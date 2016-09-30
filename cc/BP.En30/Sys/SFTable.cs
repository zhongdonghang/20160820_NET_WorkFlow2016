using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;
using System.Net;
using System.Xml;
using BP.DA;
using BP.En;
using Microsoft.CSharp;
using System.Web.Services.Description;

namespace BP.Sys
{
    /// <summary>
    /// ��������Դ����
    /// </summary>
    public enum SrcType
    {
        /// <summary>
        /// ���ر������ͼ
        /// </summary>
        TableOrView,
        /// <summary>
        /// ͨ��һ��SQLȷ����һ���ⲿ����Դ
        /// </summary>
        SQL,
        /// <summary>
        /// ͨ��WebServices��õ�һ������Դ
        /// </summary>
        WebServices
    }
    /// <summary>
    /// ���������
    /// </summary>
    public enum CodeStruct
    {
        /// <summary>
        /// ��ͨ�ı����
        /// </summary>
        NoName,
        /// <summary>
        /// �������(No,Name,ParentNo)
        /// </summary>
        Tree,
        /// <summary>
        /// �������������
        /// </summary>
        GradeNoName
    }
	/// <summary>
	/// �û��Զ����
	/// </summary>
    public class SFTableAttr : EntityNoNameAttr
    {
        /// <summary>
        /// �Ƿ����ɾ��
        /// </summary>
        public const string IsDel = "IsDel";
        /// <summary>
        /// �ֶ�
        /// </summary>
        public const string FK_Val = "FK_Val";
        /// <summary>
        /// ���ݱ�����
        /// </summary>
        public const string TableDesc = "TableDesc";
        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public const string DefVal = "DefVal";
        /// <summary>
        /// ����Դ
        /// </summary>
        public const string DBSrc = "DBSrc";
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public const string IsTree = "IsTree";
        /// <summary>
        /// ������
        /// </summary>
        public const string SrcType = "SrcType";
        /// <summary>
        /// �ֵ������
        /// </summary>
	    public const string CodeStruct = "CodeStruct";

        #region ���ӵ�����ϵͳ��ȡ���ݵ����ԡ�
        /// <summary>
        /// ����Դ
        /// </summary>
        public const string FK_SFDBSrc = "FK_SFDBSrc";
        /// <summary>
        /// ����Դ��
        /// </summary>
        public const string SrcTable = "SrcTable";
        /// <summary>
        /// ��ʾ��ֵ
        /// </summary>
        public const string ColumnValue = "ColumnValue";
        /// <summary>
        /// ��ʾ������
        /// </summary>
        public const string ColumnText = "ColumnText";
        /// <summary>
        /// �����ֵ
        /// </summary>
        public const string ParentValue = "ParentValue";
        /// <summary>
        /// ��ѯ���
        /// </summary>
	    public const string SelectStatement = "SelectStatement";
        /// <summary>
        /// ���������
        /// </summary>
        public const string CashMinute = "CashMinute";
        /// <summary>
        /// ��������ʱ��
        /// </summary>
        public const string CashDT = "CashDT";
        #endregion ���ӵ�����ϵͳ��ȡ���ݵ����ԡ�
    }
	/// <summary>
	/// �û��Զ����
	/// </summary>
    public class SFTable : EntityNoName
    {
        #region ����Դ����.
        /// <summary>
        /// ����ⲿ���ݱ�
        /// </summary>
        public System.Data.DataTable GetTableSQL
        {
            get
            {
                SFDBSrc src = new SFDBSrc(this.FK_SFDBSrc);

                #region //this.SrcType == Sys.SrcType.WebServices��by liuxc
                //��ֻ����No,Name�ṹ������Դ��2015.10.04��added by liuxc
                if (this.SrcType == Sys.SrcType.WebServices)
                {
                    var td = this.TableDesc.Split(','); //�ӿ�����,��������
                    var ps = (this.SelectStatement ?? string.Empty).Split('&');
                    var args = new ArrayList();
                    string[] pa = null;

                    foreach(var p in ps)
                    {
                        if (string.IsNullOrWhiteSpace(p)) continue;

                        pa = p.Split('=');
                        if (pa.Length != 2) continue;

                        //�˴�ҪSL����ʾ��ʱ����������
                        try
                        {
                            if (pa[1].Contains("@WebUser.No"))
                                pa[1] = pa[1].Replace("@WebUser.No", BP.Web.WebUser.No);
                            if (pa[1].Contains("@WebUser.Name"))
                                pa[1] = pa[1].Replace("@WebUser.Name", BP.Web.WebUser.Name);
                            if (pa[1].Contains("@WebUser.FK_Dept"))
                                pa[1] = pa[1].Replace("@WebUser.FK_Dept", BP.Web.WebUser.FK_Dept);
                            if (pa[1].Contains("@WebUser.FK_DeptName"))
                                pa[1] = pa[1].Replace("@WebUser.FK_DeptName", BP.Web.WebUser.FK_DeptName);
                        }
                        catch
                        {}

                        if (pa[1].Contains("@WorkID"))
                            pa[1] = pa[1].Replace("@WorkID", BP.Sys.Glo.Request["WorkID"] ?? "");
                        if (pa[1].Contains("@NodeID"))
                            pa[1] = pa[1].Replace("@NodeID", BP.Sys.Glo.Request["NodeID"] ?? "");
                        if (pa[1].Contains("@FK_Node"))
                            pa[1] = pa[1].Replace("@FK_Node", BP.Sys.Glo.Request["FK_Node"] ?? "");
                        if (pa[1].Contains("@FK_Flow"))
                            pa[1] = pa[1].Replace("@FK_Flow", BP.Sys.Glo.Request["FK_Flow"] ?? "");
                        if (pa[1].Contains("@FID"))
                            pa[1] = pa[1].Replace("@FID", BP.Sys.Glo.Request["FID"] ?? "");

                        args.Add(pa[1]);
                    }

                    var result = InvokeWebService(src.IP, td[0], args.ToArray());

                    switch(td[1])
                    {
                        case "DataSet":
                            return result == null ? new DataTable() : (result as DataSet).Tables[0];
                        case "DataTable":
                            return result as DataTable;
                        case "Json":
                            var jdata = LitJson.JsonMapper.ToObject(result as string);

                            if (!jdata.IsArray)
                                throw new Exception("@���ص�JSON��ʽ�ַ�����" + (result ?? string.Empty) + "������ȷ");

                            var dt = new DataTable();
                            dt.Columns.Add("No", typeof (string));
                            dt.Columns.Add("Name", typeof (string));

                            for(var i=0;i<jdata.Count;i++)
                            {
                                dt.Rows.Add(jdata[i]["No"].ToString(), jdata[i]["Name"].ToString());
                            }

                            return dt;
                        case "Xml":
                            if (result == null || string.IsNullOrWhiteSpace(result.ToString()))
                                throw new Exception("@���ص�XML��ʽ�ַ�������ȷ��");

                            var xml = new XmlDocument();
                            xml.LoadXml(result as string);

                            XmlNode root = null;

                            if (xml.ChildNodes.Count < 2)
                                root = xml.ChildNodes[0];
                            else
                                root = xml.ChildNodes[1];
                            
                            dt = new DataTable();
                            dt.Columns.Add("No", typeof (string));
                            dt.Columns.Add("Name", typeof (string));

                            foreach(XmlNode node in root.ChildNodes)
                            {
                                dt.Rows.Add(node.SelectSingleNode("No").InnerText,
                                            node.SelectSingleNode("Name").InnerText);
                            }

                            return dt;
                        default:
                            throw new Exception("@��֧�ֵķ�������" + td[1]);
                    }
                }
                #endregion


                string runObj = this.SelectStatement;
                if (runObj.Contains("@WebUser.No"))
                    runObj = runObj.Replace("@WebUser.No", BP.Web.WebUser.No);

                if (runObj.Contains("@WebUser.Name"))
                    runObj = runObj.Replace("@WebUser.Name", BP.Web.WebUser.Name);

                if (runObj.Contains("@WebUser.FK_Dept"))
                    runObj = runObj.Replace("@WebUser.FK_Dept", BP.Web.WebUser.FK_Dept);

                return src.RunSQLReturnTable(runObj);
            }
        }
        /// <summary>
        /// ʵ����WebServices
        /// </summary>
        /// <param name="url">WebServices��ַ</param>
        /// <param name="methodname">���õķ���</param>
        /// <param name="args">��webservices����Ҫ�Ĳ�����˳��ŵ����object[]��</param>
        public object InvokeWebService(string url, string methodname, object[] args)
        {

            //�����namespace�������õ�webservices�������ռ䣬��������д���ģ���ҿ��Լ�һ�����������洫������
            string @namespace = "BP.RefServices";
            try
            {
                if (url.EndsWith(".asmx"))
                    url += "?wsdl";
                else if (url.EndsWith(".svc"))
                    url += "?singleWsdl";

                //��ȡWSDL
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(url);
                ServiceDescription sd = ServiceDescription.Read(stream);
                string classname = sd.Services[0].Name;
                ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
                sdi.AddServiceDescription(sd, "", "");
                CodeNamespace cn = new CodeNamespace(@namespace);

                //���ɿͻ��˴��������
                CodeCompileUnit ccu = new CodeCompileUnit();
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);
                CSharpCodeProvider csc = new CSharpCodeProvider();
                ICodeCompiler icc = csc.CreateCompiler();

                //�趨�������
                CompilerParameters cplist = new CompilerParameters();
                cplist.GenerateExecutable = false;
                cplist.GenerateInMemory = true;
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");

                //���������
                CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
                if (true == cr.Errors.HasErrors)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                    {
                        sb.Append(ce.ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }

                //���ɴ���ʵ���������÷���
                System.Reflection.Assembly assembly = cr.CompiledAssembly;
                Type t = assembly.GetType(@namespace + "." + classname, true, true);
                object obj = Activator.CreateInstance(t);
                System.Reflection.MethodInfo mi = t.GetMethod(methodname);

                return mi.Invoke(obj, args);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region ���ӵ�����ϵͳ��ȡ���ݵ�����
        /// <summary>
        /// ����Դ
        /// </summary>
        public string FK_SFDBSrc
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.FK_SFDBSrc);
            }
            set
            {
                this.SetValByKey(SFTableAttr.FK_SFDBSrc, value);
            }
        }
        public string FK_SFDBSrcT
        {
            get
            {
                return this.GetValRefTextByKey(SFTableAttr.FK_SFDBSrc);
            }
        }
        /// <summary>
        /// ���ݻ���ʱ��
        /// </summary>
        public string CashDT
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.CashDT);
            }
            set
            {
                this.SetValByKey(SFTableAttr.CashDT, value);
            }
        }
        /// <summary>
        /// ͬ�����
        /// </summary>
        public int CashMinute
        {
            get
            {
                return this.GetValIntByKey(SFTableAttr.CashMinute);
            }
            set
            {
                this.SetValByKey(SFTableAttr.CashMinute, value);
            }
        }
       
        /// <summary>
        /// ���������
        /// </summary>
        public string SrcTable
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.SrcTable);
            }
            set
            {
                this.SetValByKey(SFTableAttr.SrcTable, value);
            }
        }
        /// <summary>
        /// ֵ/�����ֶ���
        /// </summary>
        public string ColumnValue
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.ColumnValue);
            }
            set
            {
                this.SetValByKey(SFTableAttr.ColumnValue, value);
            }
        }
        /// <summary>
        /// ��ʾ�ֶ�/��ʾ�ֶ���
        /// </summary>
        public string ColumnText
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.ColumnText);
            }
            set
            {
                this.SetValByKey(SFTableAttr.ColumnText, value);
            }
        }
        /// <summary>
        /// ������ֶ���
        /// </summary>
        public string ParentValue
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.ParentValue);
            }
            set
            {
                this.SetValByKey(SFTableAttr.ParentValue, value);
            }
        }

        /// <summary>
        /// ��ѯ���
        /// </summary>
        public string SelectStatement
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.SelectStatement);
            }
            set
            {
                this.SetValByKey(SFTableAttr.SelectStatement, value);
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsClass
        {
            get
            {
                if (this.No.Contains("."))
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// �Ƿ�������ʵ��?
        /// </summary>
        public bool IsTree
        {
            get
            {
                if (this.CodeStruct == Sys.CodeStruct.NoName)
                    return false;
                return true;
            }
        }
        /// <summary>
        /// ����Դ����
        /// </summary>
        public SrcType SrcType
        {
            get
            {
                return (SrcType)this.GetValIntByKey(SFTableAttr.SrcType);
            }
            set
            {
                this.SetValByKey(SFTableAttr.SrcType, (int)value);
            }
        }
        /// <summary>
        /// �ֵ������
        /// <para>0��NoName����</para>
        /// <para>1��NoNameTree����</para>
        /// <para>2��NoName������������</para>
        /// </summary>
        public CodeStruct CodeStruct
	    {
	        get
	        {
                return (CodeStruct)this.GetValIntByKey(SFTableAttr.CodeStruct);
	        }
            set
            {
                this.SetValByKey(SFTableAttr.CodeStruct, (int)value);
            }
	    }
        public string CodeStructT
        {
            get
            {
                return this.GetValRefTextByKey(SFTableAttr.CodeStruct);
            }
        }
        /// <summary>
        /// ֵ
        /// </summary>
        public string FK_Val
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.FK_Val);
            }
            set
            {
                this.SetValByKey(SFTableAttr.FK_Val, value);
            }
        }
        public string TableDesc
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.TableDesc);
            }
            set
            {
                this.SetValByKey(SFTableAttr.TableDesc, value);
            }
        }
        public string DefVal
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.DefVal);
            }
            set
            {
                this.SetValByKey(SFTableAttr.DefVal, value);
            }
        }
        public EntitiesNoName HisEns
        {
            get
            {
                if (this.IsClass)
                {
                    EntitiesNoName ens = (EntitiesNoName)BP.En.ClassFactory.GetEns(this.No);
                    ens.RetrieveAll();
                    return ens;
                }

                BP.En.GENoNames ges = new GENoNames(this.No, this.Name);
                ges.RetrieveAll();
                return ges;
            }
        }
        #endregion

        #region ���췽��
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                uac.IsInsert = false;
                return uac;
            }
        }
        /// <summary>
        /// �û��Զ����
        /// </summary>
        public SFTable()
        {
        }
        public SFTable(string mypk)
        {
            this.No = mypk;
            try
            {
                this.Retrieve();
            }
            catch (Exception ex)
            {
                switch (this.No)
                {
                    case "BP.Pub.NYs":
                        this.Name = "����";
                      //  this.HisCodeStruct = CodeStruct.ClsLab;
                        this.FK_Val = "FK_NY";
                     //   this.IsEdit = true;
                        this.Insert();
                        break;
                    case "BP.Pub.YFs":
                        this.Name = "��";
                      //  this.HisCodeStruct = CodeStruct.ClsLab;
                        this.FK_Val = "FK_YF";
                       // this.IsEdit = true;
                        this.Insert();
                        break;
                    case "BP.Pub.Days":
                        this.Name = "��";
                     //   this.HisCodeStruct = CodeStruct.ClsLab;
                        this.FK_Val = "FK_Day";
                        //this.IsEdit = true;
                        this.Insert();
                        break;
                    case "BP.Pub.NDs":
                        this.Name = "��";
                     //   this.HisCodeStruct = CodeStruct.ClsLab;
                        this.FK_Val = "FK_ND";
                       // this.IsEdit = true;
                        this.Insert();
                        break;
                    default:
                        throw new Exception(ex.Message);
                }
            }
        }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_SFTable");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "�ֵ��";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(SFTableAttr.No, null, "��Ӣ������", true, false, 1, 200, 20);
                map.AddTBString(SFTableAttr.Name, null, "����������", true, false, 0, 200, 20);

                map.AddDDLSysEnum(SFTableAttr.SrcType, 0, "���ݱ�����", true, false, SFTableAttr.SrcType, 
                    "@0=�����@1=�ⲿ��(SQL��)@2=WebService��(ͨ��WS�����)");

                map.AddDDLSysEnum(SFTableAttr.CodeStruct, 0, "�ֵ������", true, false, SFTableAttr.CodeStruct);
                
                map.AddTBString(SFTableAttr.FK_Val, null, "Ĭ�ϴ������ֶ���", true, false, 0, 200, 20);
                map.AddTBString(SFTableAttr.TableDesc, null, "������", true, false, 0, 200, 20);
                map.AddTBString(SFTableAttr.DefVal, null, "Ĭ��ֵ", true, false, 0, 200, 20);

                //��ͬ����ص�����.
                map.AddTBString(SFTableAttr.CashDT, null, "�ϴ�ͬ����ʱ��", false, false, 0, 200, 20);
                map.AddTBInt(SFTableAttr.CashMinute, 0, "���ݻ���ʱ��(0��ʾ������)", false, false);
               

                //����Դ.
                map.AddDDLEntities(SFTableAttr.FK_SFDBSrc, "local", "����Դ", new BP.Sys.SFDBSrcs(), true);

                map.AddTBString(SFTableAttr.SrcTable, null, "����Դ��", false, false, 0, 200, 20);
                map.AddTBString(SFTableAttr.ColumnValue, null, "��ʾ��ֵ(�����)", false, false, 0, 200, 20);
                map.AddTBString(SFTableAttr.ColumnText, null, "��ʾ������(������)", false, false, 0, 200, 20);
                map.AddTBString(SFTableAttr.ParentValue, null, "����ֵ(������)", false, false, 0, 200, 20);
                map.AddTBString(SFTableAttr.SelectStatement, null, "��ѯ���", false, false, 0, 1000, 600, true);

                //����.
                map.AddSearchAttr(SFTableAttr.FK_SFDBSrc);

                RefMethod rm = new RefMethod();
                rm.Title = "�鿴����"; 
                rm.ClassMethodName = this.ToString() + ".DoEdit";
                rm.RefMethodType = RefMethodType.RightFrameOpen;
                rm.IsForEns = false;
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "����Table��";
                rm.ClassMethodName = this.ToString() + ".DoGuide";
                rm.RefMethodType = RefMethodType.RightFrameOpen;
                rm.IsForEns = false;
                map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = "����Դ����";
                //rm.ClassMethodName = this.ToString() + ".DoMangDBSrc";
                //rm.RefMethodType = RefMethodType.RightFrameOpen;
                //rm.IsForEns = false;
                //map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        /// <summary>
        /// ����Դ����
        /// </summary>
        /// <returns></returns>
        public string DoMangDBSrc()
        {
            return "/WF/Comm/Search.aspx?EnsName=BP.Sys.SFDBSrcs";
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string DoGuide()
        {
            return "/WF/Comm/Sys/SFGuide.aspx";
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <returns></returns>
        public string DoEdit()
        {
            if (this.IsClass)
                return "/WF/Comm/Ens.aspx?EnsName=" + this.No;
            else
                return "/WF/MapDef/SFTableEditData.aspx?RefNo=" + this.No;
        }
        protected override bool beforeDelete()
        {
            MapAttrs attrs = new MapAttrs();
            attrs.Retrieve(MapAttrAttr.UIBindKey, this.No);
            if (attrs.Count != 0)
            {
                string err = "";
                foreach (MapAttr item in attrs)
                    err += " @ " + item.MyPK + " " + item.Name ;
                throw new Exception("@����ʵ���ֶ�������:"+err+"��������ɾ���ñ�");
            }
            return base.beforeDelete();
        }
    }
	/// <summary>
	/// �û��Զ����s
	/// </summary>
    public class SFTables : EntitiesNoName
	{		
		#region ����
        /// <summary>
        /// �û��Զ����s
        /// </summary>
		public SFTables()
		{
		}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity 
		{
			get
			{
				return new SFTable();
			}
		}
		#endregion
	}
}

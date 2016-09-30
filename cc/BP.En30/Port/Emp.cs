using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Sys;

namespace BP.Port
{
	/// <summary>
	/// ����Ա����
	/// </summary>
    public class EmpAttr : BP.En.EntityNoNameAttr
    {
        #region ��������
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// ����
        /// </summary>
        public const string Pass = "Pass";
        /// <summary>
        /// sid
        /// </summary>
        public const string SID = "SID";
        #endregion
    }
	/// <summary>
	/// Emp ��ժҪ˵����
	/// </summary>
    public class Emp : EntityNoName
    {
        #region ��չ����
        /// <summary>
        /// ��Ҫ�Ĳ��š�
        /// </summary>
        public Dept HisDept
        {
            get
            {
                try
                {
                    return new Dept(this.FK_Dept);
                }
                catch (Exception ex)
                {
                    throw new Exception("@��ȡ����Ա" + this.No + "����[" + this.FK_Dept + "]���ִ���,������ϵͳ����Աû�и���ά������.@" + ex.Message);
                }
            }
        }
        /// <summary>
        /// ������λ���ϡ�
        /// </summary>
        public Stations HisStations
        {

            get
            {
                if (BP.Sys.SystemConfig.OSDBSrc == OSDBSrc.WebServices)
                {
                    //�������.
                    var v = DataType.GetPortalInterfaceSoapClientInstance();
                    DataTable dt = v.GetEmpHisStations(this.No);

                    //���ò�ѯ.
                    Stations ens = new Stations();
                    QueryObject.InitEntitiesByDataTable(ens, dt, null);
                    return ens;
                }
                else
                {
                    EmpStations sts = new EmpStations();
                    QueryObject qo = new QueryObject(sts);
                    qo.AddWhere(EmpDeptAttr.FK_Emp, this.No);
                    qo.DoQuery();

                    Stations ens = new Stations();
                    foreach (EmpStation en in sts)
                    {
                        ens.AddEntity(new Station(en.FK_Station));
                    }
                    return ens;
                }
            }
        }
        /// <summary>
        /// ���ż��ϡ�
        /// </summary>
        public Depts HisDepts
        {
            get
            {
                if (BP.Sys.SystemConfig.OSDBSrc == OSDBSrc.WebServices)
                {
                    //�������.
                    var v = DataType.GetPortalInterfaceSoapClientInstance();
                    DataTable dt = v.GetEmpHisDepts(this.No);

                    //���ò�ѯ.
                    Depts depts = new Depts();
                    QueryObject.InitEntitiesByDataTable(depts, dt, null);
                    return depts;
                }
                else
                {
                    EmpDepts sts = new EmpDepts();
                    QueryObject qo = new QueryObject(sts);
                    qo.AddWhere(EmpDeptAttr.FK_Emp, this.No);
                    qo.DoQuery();

                    Depts ens = new Depts();
                    foreach (EmpDept en in sts)
                    {
                        ens.AddEntity(new Dept(en.FK_Dept));
                    }
                    return ens;
                }
             
            }
        }
        /// <summary>
        /// ���ű��
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStrByKey(EmpAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(EmpAttr.FK_Dept, value);
            }
        }
        /// <summary>
        /// ���ű��
        /// </summary>
        public string FK_DeptText
        {
            get
            {
                return this.GetValRefTextByKey(EmpAttr.FK_Dept);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Pass
        {
            get
            {
                return this.GetValStrByKey(EmpAttr.Pass);
            }
            set
            {
                this.SetValByKey(EmpAttr.Pass, value);
            }
        }
        public string SID
        {
            get
            {
                return this.GetValStrByKey(EmpAttr.SID);
            }
            set
            {
                this.SetValByKey(EmpAttr.SID, value);
            }
        }
        #endregion

        #region ��������
        /// <summary>
        /// Ȩ�޹���.
        /// </summary>
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForAppAdmin();
                return uac;
            }
        }
        /// <summary>
        /// �������(������д�˷���)
        /// </summary>
        /// <param name="pass">����</param>
        /// <returns>�Ƿ�ƥ��ɹ�</returns>
        public bool CheckPass(string pass)
        {
            if (SystemConfig.IsDebug)
                return true;

            string gePass = SystemConfig.AppSettings["GenerPass"];
            if (gePass == pass)
                return true;

            if (SystemConfig.OSDBSrc == OSDBSrc.WebServices)
            {
                //�����ʹ��webservicesУ��.
                var v = DataType.GetPortalInterfaceSoapClientInstance();
                int i = v.CheckUserNoPassWord(this.No, pass);
                if (i == 1)
                    return true;
                return false;
            }
            else
            {
                /*ʹ�����ݿ�У��.*/
                if (this.Pass == pass)
                    return true;
            }
            return false;
        }
        #endregion ��������

        #region ���캯��
        /// <summary>
        /// ����Ա
        /// </summary>
        public Emp()
        {
        }
        /// <summary>
        /// ����Ա
        /// </summary>
        /// <param name="no">���</param>
        public Emp(string no)
        {
            this.No = no.Trim();
            if (this.No.Length == 0)
                throw new Exception("@Ҫ��ѯ�Ĳ���Ա���Ϊ�ա�");
            try
            {
                this.Retrieve();
            }
            catch (Exception ex)
            {
                if (BP.Sys.SystemConfig.OSDBSrc == OSDBSrc.Database)
                {
                    //��½�ʺŲ�ѯ�����û���ʹ��ְ����Ų�ѯ��
                    QueryObject obj = new QueryObject(this);
                    obj.AddWhere(EmpAttr.No, no);
                    int i = obj.DoQuery();
                    if (i == 0)
                        i = this.RetrieveFromDBSources();
                    if (i == 0)
                        throw new Exception("@�û������������[" + no + "]�������ʺű�ͣ�á�@������Ϣ(���ڴ��в�ѯ���ִ���)��ex1=" + ex.Message);
                }
                else
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map();

                #region ��������
                map.EnDBUrl =
                    new DBUrl(DBUrlType.AppCenterDSN); //Ҫ���ӵ�����Դ����ʾҪ���ӵ����Ǹ�ϵͳ���ݿ⣩��
                map.PhysicsTable = "Port_Emp"; // Ҫ�����
                map.DepositaryOfMap = Depositary.Application;    //ʵ��map�Ĵ��λ��.
                map.DepositaryOfEntity = Depositary.Application; //ʵ����λ��
                map.EnDesc = "�û�"; // "�û�";
                map.EnType = EnType.App;   //ʵ�����͡�
                #endregion

                #region �ֶ�
                /* �����ֶ����Ե����� .. */
                map.IsEnableVer = true;
                map.AddTBStringPK(EmpAttr.No, null, "���", true, false, 1, 20, 30); 
                map.AddTBString(EmpAttr.Name, null, "����", true, false, 0, 200, 30);
                map.AddTBString(EmpAttr.Pass, "123", "����", false, false, 0, 20, 10);
                map.AddDDLEntities(EmpAttr.FK_Dept, null, "����", new Port.Depts(), true);
                map.AddTBString(EmpAttr.SID, null, "��ȫУ����", false, false, 0, 36, 36);
                //map.AddTBString("Tel", null, "Tel", false, false, 0, 20, 10);
                //map.AddTBString(EmpAttr.PID, null, this.ToE("PID", "UKEY��PID"), true, false, 0, 100, 30);
                //map.AddTBString(EmpAttr.PIN, null, this.ToE("PIN", "UKEY��PIN"), true, false, 0, 100, 30);
                //map.AddTBString(EmpAttr.KeyPass, null, this.ToE("KeyPass", "UKEY��KeyPass"), true, false, 0, 100, 30);
                //map.AddTBString(EmpAttr.IsUSBKEY, null, this.ToE("IsUSBKEY", "�Ƿ�ʹ��usbkey"), true, false, 0, 100, 30);
                //map.AddDDLSysEnum("Sex", 0, "�Ա�", "@0=Ů@1=��");
                #endregion �ֶ�

                map.AddSearchAttr(EmpAttr.FK_Dept);

                #region ���Ӷ�Զ�����
                //���Ĳ���Ȩ��
                map.AttrsOfOneVSM.Add(new EmpDepts(), new Depts(), EmpDeptAttr.FK_Emp, 
                    EmpDeptAttr.FK_Dept, DeptAttr.Name, DeptAttr.No, "����Ȩ��");

                map.AttrsOfOneVSM.Add(new EmpStations(), new Stations(), EmpStationAttr.FK_Emp, 
                    EmpStationAttr.FK_Station,DeptAttr.Name, DeptAttr.No, "��λȨ��");
                #endregion

                //RefMethod rm = new RefMethod();
                //rm.Title = "����";
                //rm.RefMethodType = RefMethodType.LinkeWinOpen;
                //rm.IsCanBatch =true;
                //rm.IsForEns = false;
                //rm.ClassMethodName = this.ToString() + ".DoExp";
                //map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }

        }
        /// <summary>
        /// ��ȡ����
        /// </summary>
        public override Entities GetNewEntities
        {
            get { return new Emps(); }
        }
        #endregion ���캯��

        #region ��д����
        protected override bool beforeDelete()
        {
            // ɾ������Ա�ĸ�λ���������ͼ���п����׳��쳣��
            try
            {
                EmpStations ess = new EmpStations();
                ess.Delete(EmpDeptAttr.FK_Emp, this.No);
            }
            catch
            {
            }

            // ɾ������Ա�Ĳ��ţ��������ͼ���п����׳��쳣��
            try
            {
                EmpDepts eds = new EmpDepts();
                eds.Delete(EmpDeptAttr.FK_Emp, this.No);
            }
            catch
            {
            }
            return base.beforeDelete();
        }
        #endregion ��д����

        #region ��д��ѯ.
        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <returns></returns>
        public override int Retrieve()
        {
            if (BP.Sys.SystemConfig.OSDBSrc == OSDBSrc.WebServices)
            {
                var v = DataType.GetPortalInterfaceSoapClientInstance();
                DataTable dt = v.GetEmp(this.No);
                if (dt.Rows.Count == 0)
                    throw new Exception("@���Ϊ("+this.No+")����Ա�����ڡ�");
                this.Row.LoadDataTable(dt, dt.Rows[0]);
                return 1;
            }
            else
            {
                return base.Retrieve();
            }
        }
        /// <summary>
        /// ��ѯ.
        /// </summary>
        /// <returns></returns>
        public override int RetrieveFromDBSources()
        {
            if (BP.Sys.SystemConfig.OSDBSrc == OSDBSrc.WebServices)
            {
                var v = DataType.GetPortalInterfaceSoapClientInstance();
                DataTable dt = v.GetEmp(this.No);
                if (dt.Rows.Count == 0)
                    return 0;
                this.Row.LoadDataTable(dt, dt.Rows[0]);
                return 1;
            }
            else
            {
                return base.RetrieveFromDBSources();
            }
        }
        #endregion

    }
	/// <summary>
	/// ����Ա
	// </summary>
    public class Emps : EntitiesNoName
    {
        #region ���췽��
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new Emp();
            }
        }
        /// <summary>
        /// ����Աs
        /// </summary>
        public Emps()
        {
        }
        /// <summary>
        /// ����Աs
        /// </summary>
        public Emps(string deptNo)
        {
            if (BP.Sys.SystemConfig.OSDBSrc == OSDBSrc.WebServices)
            {
                this.Clear(); //�����������.
                //�������.
                var v = DataType.GetPortalInterfaceSoapClientInstance();
                DataTable dt = v.GetEmpsByDeptNo(deptNo);
                if (dt.Rows.Count != 0)
                    //���ò�ѯ.
                    QueryObject.InitEntitiesByDataTable(this, dt, null);
            }
            else
            {
               this.Retrieve(EmpAttr.FK_Dept, deptNo);
            }
        }
        #endregion ���췽��

        #region ��д��ѯ,add by stone 2015.09.30 Ϊ����Ӧ�ܹ���webservice����Դ��ѯ����.
        /// <summary>
        /// ��д��ѯȫ����Ӧ��WSȡ������Ҫ
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {
            if (BP.Sys.SystemConfig.OSDBSrc == OSDBSrc.WebServices)
            {
                this.Clear(); //�����������.
                //�������.
                var v = DataType.GetPortalInterfaceSoapClientInstance();
                DataTable dt = v.GetEmps();
                if (dt.Rows.Count == 0)
                    return 0;

                //���ò�ѯ.
                QueryObject.InitEntitiesByDataTable(this, dt, null);
                return dt.Rows.Count;
            }
            else
            {
                return base.RetrieveAll();
            }
        }
        /// <summary>
        /// ��д������Դ��ѯȫ����Ӧ��WSȡ������Ҫ
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAllFromDBSource()
        {
            if (BP.Sys.SystemConfig.OSDBSrc == OSDBSrc.WebServices)
            {
                this.Clear(); //�����������.
                //�������.
                var v = DataType.GetPortalInterfaceSoapClientInstance();
                DataTable dt = v.GetEmps();
                if (dt.Rows.Count == 0)
                    return 0;

                //���ò�ѯ.
                QueryObject.InitEntitiesByDataTable(this, dt, null);
                return dt.Rows.Count;
            }
            else
            {
                return base.RetrieveAllFromDBSource();
            }
        }
        #endregion ��д��ѯ.

    }
}
 
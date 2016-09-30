using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Sys;
using BP.Web.Controls;
using System.Data;

namespace CCFlow.WF.Admin.Org
{
    public partial class Default : System.Web.UI.Page
    {
        #region Property
        private string[] _steps = new[]
                                     {
                                         "第1步 设置组织机构模式",
                                         "第2步 设置组织机构维护方式",
                                         "第3步 选择组织机构来源",
                                         "第4步 配置查询语句"
                                     };

        private Dictionary<string, Dictionary<string, string>> _oneones =
            new Dictionary<string, Dictionary<string, string>>
                {
                    {
                        "Port_Station",
                        new Dictionary<string, string> {{"No", "String"}, {"Name", "String"}, {"StaGrade", "Int32"}}
                        },
                    {
                        "Port_Dept",
                        new Dictionary<string, string> {{"No", "String"}, {"Name", "String"}, {"ParentNo", "String"}}
                        },
                    {
                        "Port_Emp",
                        new Dictionary<string, string>
                            {
                                {"No", "String"},
                                {"Name", "String"},
                                {"Pass", "String"},
                                {"FK_Dept", "String"},
                                {"SID", "String"}
                            }
                        },
                    {"Port_EmpStation", new Dictionary<string, string> {{"FK_Emp", "String"}, {"FK_Station", "String"}}}
                };

        private Dictionary<string, Dictionary<string, string>> _onemores =
            new Dictionary<string, Dictionary<string, string>>
                {
                    {
                        "Port_StationType",
                        new Dictionary<string, string> {{"No", "String"}, {"Name", "String"}}
                        },
                    {
                        "Port_Station",
                        new Dictionary<string, string> {{"No", "String"}, {"Name", "String"}, {"FK_StationType", "String"}}
                        },
                    {
                        "Port_Dept",
                        new Dictionary<string, string> {{"No", "String"}, {"Name", "String"}, {"ParentNo", "String"}}
                        },
                    {
                        "Port_Duty",
                        new Dictionary<string, string> {{"No", "String"}, {"Name", "String"}}
                        },
                    {
                        "Port_DeptDuty",
                        new Dictionary<string, string> {{"FK_Dept", "String"}, {"FK_Duty", "String"}}
                        },
                    {
                        "Port_DeptStation",
                        new Dictionary<string, string> {{"FK_Dept", "String"}, {"FK_Station", "String"}}
                        },
                    {
                        "Port_Emp",
                        new Dictionary<string, string>
                            {
                                {"No", "String"},
                                {"Name", "String"},
                                {"Pass", "String"},
                                {"FK_Dept", "String"},
                                {"FK_Duty", "String"},
                                {"SID", "String"}
                            }
                        },
                    {"Port_DeptEmpStation", new Dictionary<string, string> {{"FK_Emp", "String"}, {"FK_Station", "String"}, {"FK_Dept", "String"}}},
                    {"Port_DeptEmp", new Dictionary<string, string> {{"FK_Emp", "String"}, {"FK_Dept", "String"}}}
                };

        public string StepTitle
        {
            get
            {
                return _steps[Step - 1];
            }
        }

        public int Step
        {
            get
            {
                return int.Parse(Request.QueryString["step"] ?? "1");
            }
        }
        #endregion

        /// <summary>
        /// 组织结构模式变量
        /// </summary>
        GloVar gvarSModel = null;
        /// <summary>
        /// 组织结构维护方式变量
        /// </summary>
        GloVar gvarSMKind = null;
        /// <summary>
        /// 组织结构数据来源变量
        /// </summary>
        GloVar gvarSSource = null;
        /// <summary>
        /// 数据源变量
        /// </summary>
        GloVar gvarDBSrc = null;
        /// <summary>
        /// 关于组织结构设置的全局变量集合
        /// </summary>
        GloVars gvars = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            GloVar gvar = new GloVar();

            switch (Step)
            {
                case 1:

                    #region 设置组织结构模式

                    Pub1.Add("选择组织结构模式：");

                    gvar.No = "StructureModel";

                    if (gvar.RetrieveFromDBSources() == 0)
                    {
                        gvar.Name = "组织结构模式";
                        gvar.Val = "1";
                        gvar.GroupKey = "IntegrationSet";
                        gvar.Note = "1.一个用户一个部门模式;2.一个用户多个部门模式.";
                        gvar.Insert();
                    }

                    BPRadioButtonList rads = new BPRadioButtonList();
                    rads.ID = "Rads_StructureModel";
                    rads.Items.Add(new ListItem("1.一个用户一个部门模式", "1"));
                    rads.Items.Add(new ListItem("2.一个用户多个部门模式", "2"));
                    rads.RepeatDirection = RepeatDirection.Horizontal;
                    rads.RepeatLayout = RepeatLayout.Flow;
                    rads.SetSelectItem(gvar.Val);
                    Pub1.Add(rads);
                    Pub1.AddBR();
                    Pub1.AddBR();

                    AddButton(NamesOfBtn.Save, "保存并继续");
                    #endregion

                    break;
                case 2:

                    #region 设置组织结构维护方式

                    Pub1.Add("设置组织结构维护方式：");

                    gvar.No = "StructureMngKind";

                    if (gvar.RetrieveFromDBSources() == 0)
                    {
                        gvar.Name = "组织结构维护方式";
                        gvar.Val = "1";
                        gvar.GroupKey = "IntegrationSet";
                        gvar.Note = "1.由CCBPM组织结构维护;2.集成我自己现有的组织结构.";
                        gvar.Insert();
                    }

                    rads = new BPRadioButtonList();
                    rads.ID = "Rads_StructureMngKind";
                    rads.Items.Add(new ListItem("1.由CCBPM组织结构维护", "1"));
                    rads.Items.Add(new ListItem("2.集成我自己现有的组织结构", "2"));
                    rads.RepeatDirection = RepeatDirection.Horizontal;
                    rads.RepeatLayout = RepeatLayout.Flow;
                    rads.SetSelectItem(gvar.Val);
                    Pub1.Add(rads);
                    Pub1.AddBR();
                    Pub1.AddBR();

                    AddButton(NamesOfBtn.Save, "保存并继续");
                    Pub1.AddSpace(1);
                    AddButton(NamesOfBtn.Back, "上一步");
                    #endregion

                    break;
                case 3:

                    #region 选择组织结构来源

                    Pub1.Add("选择组织结构来源：");

                    gvar.No = "StructureSource";

                    if (gvar.RetrieveFromDBSources() == 0)
                    {
                        gvar.Name = "选择组织结构来源";
                        gvar.Val = "1";
                        gvar.GroupKey = "IntegrationSet";
                        gvar.Note = "1.使用数据源直接连接;2.使用WebServces模式;3.使用AD.";
                        gvar.Insert();
                    }

                    rads = new BPRadioButtonList();
                    rads.ID = "Rads_StructureSource";
                    rads.Items.Add(new ListItem("1.使用数据源直接连接", "1"));
                    rads.Items.Add(new ListItem("2.使用WebServces模式", "2"));
                    rads.Items.Add(new ListItem("3.使用AD", "3"));
                    rads.RepeatDirection = RepeatDirection.Horizontal;
                    rads.RepeatLayout = RepeatLayout.Flow;
                    rads.SetSelectItem(gvar.Val);
                    Pub1.Add(rads);
                    Pub1.AddBR();
                    Pub1.AddBR();

                    AddButton(NamesOfBtn.Save, "保存并继续");
                    Pub1.AddSpace(1);
                    AddButton(NamesOfBtn.Back, "上一步");
                    #endregion

                    break;
                case 4:

                    #region 配置查询语句

                    gvars = gvar.GetNewEntities as GloVars;
                    gvars.Retrieve(GloVarAttr.GroupKey, "IntegrationSet");

                    gvarSModel = gvars.GetEntityByKey("StructureModel") as GloVar;
                    gvarSMKind = gvars.GetEntityByKey("StructureMngKind") as GloVar;
                    gvarSSource = gvars.GetEntityByKey("StructureSource") as GloVar;
                    string msg = string.Empty;

                    if (gvarSModel == null)
                    {
                        msg = "“<a href='?step=1'>组织结构模式</a>”、";
                    }

                    if (gvarSMKind == null)
                    {
                        msg += "“<a href='?step=2'>组织结构维护方式</a>”、";
                    }

                    if (gvarSSource == null)
                    {
                        msg += "“<a href='?step=3'>组织结构来源</a>”";
                    }

                    if (!string.IsNullOrWhiteSpace(msg))
                    {
                        Pub1.AddEasyUiPanelInfo("信息", "请先配置" + msg.TrimEnd('、') + "，然后配置此项！");
                        Pub1.AddBR();
                        AddButton(NamesOfBtn.Back, "上一步");
                        return;
                    }

                    //如果组织结构来源选择的是WebService和AD，则提示用户编写提供的通用webservice，以联接这两方载体
                    if (gvarSSource.Val == "2" || gvarSSource.Val == "3")
                    {
                        Pub1.AddEasyUiPanelInfo("信息", "您选择的组织结构数据来源是“<span style='font-weight:bold;'>" + (gvarSSource.Val == "2" ? "使用WebServces模式" : "使用AD") + "</span>”，该种方式目前请自行修改位于CCFlow项目下的WebService文件：\\DataUser\\PortalInterface.asmx，此WebService用来提供组织结构的相关数据。");
                        Pub1.AddBR();
                        AddButton(NamesOfBtn.Back, "上一步");
                        return;
                    }

                    //如果组织结构维护方式选择的是由“CCBPM组织结构维护”，则下面的配置查询语句就不需要了
                    if (gvarSMKind.Val == "1")
                    {
                        msg = "您选择的组织结构数据来源是“<span style='font-weight:bold;'>使用数据源直接连接</span>”，组织结构维护方式选择的是“<span style='font-weight:bold;'>CCBPM组织结构维护</span>”，组织结构模式是“<span style='font-weight:bold;'>" + (gvarSModel.Val == "1" ? "一个用户一个部门模式" : "一个用户多个部门模式") + "</span>”，此种模式下，在CCFlow的主库中需要维护的相关表有：<br />";

                        if (gvarSModel.Val == "1")
                        {
                            //一个用户一个部门
                            msg += "1.岗位类型[Sys_Enum]。Sys_Enum枚举表中EnumKey='StaGrade'的枚举。<br />";
                            msg += "2.岗位[Port_Station]。<br />";
                            msg += "3.部门[Port_Dept]。<br />";
                            msg += "4.人员[Port_Emp]。<br />";
                            msg += "5.人员部门[Port_EmpDept]。<br />";
                            msg += "6.人员岗位[Port_EmpStation]。";
                        }
                        else
                        {
                            //一个用户多个部门
                            msg += "1.岗位类型[Port_StationType]。<br />";
                            msg += "2.岗位[Port_Station]。<br />";
                            msg += "3.部门[Port_Dept]。<br />";
                            msg += "4.职务[Port_Duty]。<br />";
                            msg += "5.部门职务[Port_DeptDuty]。<br />";
                            msg += "6.部门岗位[Port_DeptStation]。<br />";
                            msg += "7.人员[Port_Emp]。<br />";
                            msg += "8.部门人员岗位[Port_DeptEmpStation]。<br />";
                            msg += "9.部门人员[Port_DeptEmp]。";
                        }

                        Pub1.AddEasyUiPanelInfo("信息", msg);
                        Pub1.AddBR();
                        AddButton(NamesOfBtn.Back, "上一步");
                    }
                    else
                    {
                        Pub1.Add(
                            "您选择的组织结构数据来源是“<span style='font-weight:bold;'>使用数据源直接连接</span>”，组织结构维护方式选择的是“<span style='font-weight:bold;'>集成我自己现有的组织结构</span>”，组织结构模式是“<span style='font-weight:bold;'>" +
                            (gvarSModel.Val == "1" ? "一个用户一个部门模式" : "一个用户多个部门模式") +
                            "</span>”。<br />");

                        gvarDBSrc = gvars.GetEntityByKey("StructureDBSrc") as GloVar;

                        if (gvarDBSrc == null)
                        {
                            gvarDBSrc = new GloVar();
                            gvarDBSrc.No = "StructureDBSrc";
                            gvarDBSrc.Name = "数据源编号";
                            gvarDBSrc.Val = "local";
                            gvarDBSrc.Note = "数据源是Sys_SFDBSrc表";
                            gvarDBSrc.GroupKey = "IntegrationSet";
                            gvarDBSrc.Save();
                        }

                        SFDBSrcs srcs = new SFDBSrcs();
                        srcs.RetrieveAll(SFDBSrcAttr.DBSrcType);

                        //根据传来的数据源编号，变更当前的数据源
                        if (!string.IsNullOrWhiteSpace(Request.QueryString["src"]) && srcs.IsExits(SFDBSrcAttr.No, Request.QueryString["src"]))
                        {
                            gvarDBSrc.Val = Request.QueryString["src"];
                            gvarDBSrc.Update();
                        }

                        DDL ddl = new DDL();
                        ddl.CssClass = "easyui-combobox";
                        ddl.Attributes["data-options"] = "onSelect:function(rcd){location.href='Integration.aspx?step=4&src=' + rcd.value;}";
                        ddl.ID = "DDL_DBSrcs";
                        ddl.AutoPostBack = true;
                        ddl.BindEntities(srcs);
                        ddl.SetSelectItem(gvarDBSrc.Val);

                        Pub1.AddBR();
                        Pub1.Add("选择数据源：");
                        Pub1.Add(ddl);
                        Pub1.AddBR();
                        Pub1.AddBR();

                        if (gvarDBSrc.Val == "local")
                        {
                            Pub1.AddEasyUiPanelInfo("信息", string.Format("“<span style='font-weight:bold;'>{0}</span>”是CCFlow主数据源，不需要进行组织结构的SQL配置。", ddl.SelectedItem.Text));
                            Pub1.AddBR();
                            AddButton(NamesOfBtn.Back, "上一步");
                            return;
                        }

                        Pub1.Add("选择此数据源，需要配置的相关表的SQL查询语句如下：");
                        Pub1.AddBR();
                        Pub1.AddBR();

                        if (gvarSModel.Val == "1")
                        {
                            #region //一个用户一个部门
                            Pub1.Add("1.<span style='font-weight:bold;'>岗位类型数据</span>。<br />");
                            LinkBtn btn = new LinkBtn(false, NamesOfBtn.Edit, "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;编辑数据");
                            btn.PostBackUrl = "javascript:void(0)";
                            btn.OnClientClick = "OpenEasyUiDialog('../../Comm/Sys/EnumList.aspx?RefNo=StaGrade&t=' + Math.random(), 'euiframe','编辑岗位类型',600,374,'icon-edit')";
                            Pub1.Add(btn);
                            Pub1.AddBR();
                            Pub1.AddBR();

                            gvar = CheckGloVar(gvars, "Port_Station", "岗位表");
                            Pub1.Add("2.<span style='font-weight:bold;'>岗位SQL</span>[列：No(唯一编号，varchar)、Name(名称，varchar)、StaGrade(岗位类型，对应枚举表EnumKey='StaGrade'枚举项的IntKey值，int)]。<br />");
                            TB tb = new TB();
                            tb.ID = "TB_Port_Station";
                            tb.TextMode = TextBoxMode.MultiLine;
                            tb.Text = gvar.Val;
                            tb.Wrap = true;
                            tb.Width = 600;
                            tb.Height = 60;
                            Pub1.Add(tb);
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.DataCheck + "_Port_Station", "检查正确性");
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.Open + "_Port_Station", "打开数据源");
                            Pub1.AddBR();
                            Pub1.AddBR();

                            gvar = CheckGloVar(gvars, "Port_Dept", "部门表");
                            Pub1.Add("3.<span style='font-weight:bold;'>部门SQL</span>[列：No(唯一编号，varchar)、Name(名称，varchar)、ParentNo(上级部门编号，varchar)]。<br />");
                            tb = new TB();
                            tb.ID = "TB_Port_Dept";
                            tb.TextMode = TextBoxMode.MultiLine;
                            tb.Text = gvar.Val;
                            tb.Wrap = true;
                            tb.Width = 600;
                            tb.Height = 60;
                            Pub1.Add(tb);
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.DataCheck + "_Port_Dept", "检查正确性");
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.Open + "_Port_Dept", "打开数据源");
                            Pub1.AddBR();
                            Pub1.AddBR();

                            gvar = CheckGloVar(gvars, "Port_Emp", "人员表");
                            Pub1.Add("4.<span style='font-weight:bold;'>人员SQL</span>[列：No(唯一编号，varchar)、Name(姓名，varchar)、Pass(密码，varchar)、FK_Dept(部门编号，varchar)、SID(安全校验码，用于是否登录的验证码，varchar)]。<br />");
                            tb = new TB();
                            tb.ID = "TB_Port_Emp";
                            tb.TextMode = TextBoxMode.MultiLine;
                            tb.Text = gvar.Val;
                            tb.Wrap = true;
                            tb.Width = 600;
                            tb.Height = 60;
                            Pub1.Add(tb);
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.DataCheck + "_Port_Emp", "检查正确性");
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.Open + "_Port_Emp", "打开数据源");
                            Pub1.AddBR();
                            Pub1.AddBR();

                            gvar = CheckGloVar(gvars, "Port_EmpStation", "人员岗位表");
                            Pub1.Add("5.<span style='font-weight:bold;'>人员岗位SQL</span>[列：FK_Emp(人员编号，varchar)、FK_Station(岗位编号，varchar)]。<br />");
                            tb = new TB();
                            tb.ID = "TB_Port_EmpStation";
                            tb.TextMode = TextBoxMode.MultiLine;
                            tb.Text = gvar.Val;
                            tb.Wrap = true;
                            tb.Width = 600;
                            tb.Height = 60;
                            Pub1.Add(tb);
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.DataCheck + "_Port_EmpStation", "检查正确性");
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.Open + "_Port_EmpStation", "打开数据源");
                            #endregion
                        }
                        else
                        {
                            #region //一个用户多个部门
                            gvar = CheckGloVar(gvars, "Port_StationType", "岗位类型表");
                            Pub1.Add("1.<span style='font-weight:bold;'>岗位类型SQL</span>[列：No(唯一编号，varchar)、Name(名称、varchar)。<br />");
                            TB tb = new TB();
                            tb.ID = "TB_Port_StationType";
                            tb.TextMode = TextBoxMode.MultiLine;
                            tb.Text = gvar.Val;
                            tb.Wrap = true;
                            tb.Width = 600;
                            tb.Height = 60;
                            Pub1.Add(tb);
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.DataCheck + "_Port_StationType", "检查正确性");
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.Open + "_Port_StationType", "打开数据源");
                            Pub1.AddBR();
                            Pub1.AddBR();

                            gvar = CheckGloVar(gvars, "Port_Station", "岗位表");
                            Pub1.Add("2.<span style='font-weight:bold;'>岗位SQL</span>[列：No(唯一编号，varchar)、Name(名称，varchar)、FK_StationType(岗位类型，对应岗位类型的No编号，varchar)]。<br />");
                            tb = new TB();
                            tb.ID = "TB_Port_Station";
                            tb.TextMode = TextBoxMode.MultiLine;
                            tb.Text = gvar.Val;
                            tb.Wrap = true;
                            tb.Width = 600;
                            tb.Height = 60;
                            Pub1.Add(tb);
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.DataCheck + "_Port_Station", "检查正确性");
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.Open + "_Port_Station", "打开数据源");
                            Pub1.AddBR();
                            Pub1.AddBR();

                            gvar = CheckGloVar(gvars, "Port_Dept", "部门表");
                            Pub1.Add("3.<span style='font-weight:bold;'>部门SQL</span>[列：No(唯一编号，varchar)、Name(名称，varchar)、ParentNo(上级部门编号，varchar)]。<br />");
                            tb = new TB();
                            tb.ID = "TB_Port_Dept";
                            tb.TextMode = TextBoxMode.MultiLine;
                            tb.Text = gvar.Val;
                            tb.Wrap = true;
                            tb.Width = 600;
                            tb.Height = 60;
                            Pub1.Add(tb);
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.DataCheck + "_Port_Dept", "检查正确性");
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.Open + "_Port_Dept", "打开数据源");
                            Pub1.AddBR();
                            Pub1.AddBR();

                            gvar = CheckGloVar(gvars, "Port_Duty", "部门表");
                            Pub1.Add("4.<span style='font-weight:bold;'>职务SQL</span>[列：No(唯一编号，varchar)、Name(名称，varchar)]。<br />");
                            tb = new TB();
                            tb.ID = "TB_Port_Duty";
                            tb.TextMode = TextBoxMode.MultiLine;
                            tb.Text = gvar.Val;
                            tb.Wrap = true;
                            tb.Width = 600;
                            tb.Height = 60;
                            Pub1.Add(tb);
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.DataCheck + "_Port_Duty", "检查正确性");
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.Open + "_Port_Duty", "打开数据源");
                            Pub1.AddBR();
                            Pub1.AddBR();

                            gvar = CheckGloVar(gvars, "Port_DeptDuty", "部门职务表");
                            Pub1.Add("5.<span style='font-weight:bold;'>部门职务SQL</span>[列：FK_Dept(部门编号，varchar)、FK_Duty(职务编号，varchar)]。<br />");
                            tb = new TB();
                            tb.ID = "TB_Port_DeptDuty";
                            tb.TextMode = TextBoxMode.MultiLine;
                            tb.Text = gvar.Val;
                            tb.Wrap = true;
                            tb.Width = 600;
                            tb.Height = 60;
                            Pub1.Add(tb);
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.DataCheck + "_Port_DeptDuty", "检查正确性");
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.Open + "_Port_DeptDuty", "打开数据源");
                            Pub1.AddBR();
                            Pub1.AddBR();

                            gvar = CheckGloVar(gvars, "Port_DeptStation", "部门岗位表");
                            Pub1.Add("6.<span style='font-weight:bold;'>部门岗位SQL</span>[列：FK_Dept(部门编号，varchar)、FK_Station(岗位编号，varchar)]。<br />");
                            tb = new TB();
                            tb.ID = "TB_Port_DeptStation";
                            tb.TextMode = TextBoxMode.MultiLine;
                            tb.Text = gvar.Val;
                            tb.Wrap = true;
                            tb.Width = 600;
                            tb.Height = 60;
                            Pub1.Add(tb);
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.DataCheck + "_Port_DeptStation", "检查正确性");
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.Open + "_Port_DeptStation", "打开数据源");
                            Pub1.AddBR();
                            Pub1.AddBR();

                            gvar = CheckGloVar(gvars, "Port_Emp", "人员表");
                            Pub1.Add("7.<span style='font-weight:bold;'>人员SQL</span>[列：No(唯一编号，varchar)、Name(姓名，varchar)、Pass(密码，varchar)、FK_Dept(部门编号，varchar)、FK_Duty(职务编号，varchar)、SID(安全校验码，用于是否登录的验证码，varchar)]。<br />");
                            tb = new TB();
                            tb.ID = "TB_Port_Emp";
                            tb.TextMode = TextBoxMode.MultiLine;
                            tb.Text = gvar.Val;
                            tb.Wrap = true;
                            tb.Width = 600;
                            tb.Height = 60;
                            Pub1.Add(tb);
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.DataCheck + "_Port_Emp", "检查正确性");
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.Open + "_Port_Emp", "打开数据源");
                            Pub1.AddBR();
                            Pub1.AddBR();

                            gvar = CheckGloVar(gvars, "Port_DeptEmpStation", "部门人员岗位表");
                            Pub1.Add("8.<span style='font-weight:bold;'>部门人员岗位SQL</span>[列：FK_Dept(部门编号，varchar)、FK_Emp(人员编号，varchar)、FK_Station(岗位编号，varchar)]。<br />");
                            tb = new TB();
                            tb.ID = "TB_Port_DeptEmpStation";
                            tb.TextMode = TextBoxMode.MultiLine;
                            tb.Text = gvar.Val;
                            tb.Wrap = true;
                            tb.Width = 600;
                            tb.Height = 60;
                            Pub1.Add(tb);
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.DataCheck + "_Port_DeptEmpStation", "检查正确性");
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.Open + "_Port_DeptEmpStation", "打开数据源");
                            Pub1.AddBR();
                            Pub1.AddBR();

                            gvar = CheckGloVar(gvars, "Port_DeptEmp", "部门人员表");
                            Pub1.Add("9.<span style='font-weight:bold;'>部门人员SQL</span>[列：FK_Dept(部门编号，varchar)、FK_Emp(人员编号，varchar)]。<br />");
                            tb = new TB();
                            tb.ID = "TB_Port_DeptEmp";
                            tb.TextMode = TextBoxMode.MultiLine;
                            tb.Text = gvar.Val;
                            tb.Wrap = true;
                            tb.Width = 600;
                            tb.Height = 60;
                            Pub1.Add(tb);
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.DataCheck + "_Port_DeptEmp", "检查正确性");
                            Pub1.AddSpace(1);
                            AddButton(NamesOfBtn.Open + "_Port_DeptEmp", "打开数据源");
                            #endregion
                        }

                        Pub1.AddBR();
                        Pub1.AddBR();
                        AddButton(NamesOfBtn.Setting, "设置全部");
                        Pub1.AddSpace(1);
                        AddButton(NamesOfBtn.Back, "上一步");
                    }

                    #endregion

                    break;
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            LinkBtn btn = sender as LinkBtn;

            if (btn.ID.StartsWith(NamesOfBtn.DataCheck + "_"))
            {
                #region //检查正确性
                string item = btn.ID.Substring((NamesOfBtn.DataCheck + "_").Length);
                string value = Pub1.GetTBByID("TB_" + item).Text;
                string srcno = Pub1.GetDDLByID("DDL_DBSrcs").SelectedItemStringVal;
                SFDBSrc src = new SFDBSrc(srcno);
                DataTable dt = null;

                //检查连接及SQL语法正确性
                try
                {
                    dt = src.RunSQLReturnTable(value, 0, 1);
                }
                catch (Exception ex)
                {
                    Alert(ex.Message, "error");
                    return;
                }

                //检查返回的列及各列类型是否正确
                Dictionary<string, string> coldefs = gvarSModel.Val == "1" ? _oneones[item] : _onemores[item];
                DataColumn col = null;
                string errorFields = string.Empty;
                string error = string.Empty;
                int idx = 0;

                foreach (KeyValuePair<string, string> coldef in coldefs)
                {
                    error = string.Empty;

                    if (!dt.Columns.Contains(coldef.Key))
                    {
                        error += (++idx) + ". 不包含" + coldef.Key + "列，";
                    }
                    else
                    {
                        col = dt.Columns[coldef.Key];
                        if (!Equals(col.DataType.Name, coldef.Value))
                        {
                            error += (error.EndsWith("，") ? "" : (++idx) + ". ") + coldef.Key + "列类型应为" + coldef.Value + "，现在是" + col.DataType.Name + "<br />";
                        }
                        else if (error.EndsWith("，"))
                        {
                            error = error.TrimEnd('，') + "<br />";
                        }
                    }

                    errorFields += error;
                }

                if (string.IsNullOrWhiteSpace(errorFields))
                {
                    //如果配置正确，则把此次的SQL语句存储上
                    GloVar gvar = gvars.GetEntityByKey(item) as GloVar;
                    gvar.Val = value;
                    gvar.Update();

                    Alert("配置正确！");
                }
                else
                {
                    Alert("成功获取数据，但有如下错误：<br />" + errorFields.TrimEnd('，'), "error");
                }
                #endregion
            }
            else if (btn.ID.StartsWith(NamesOfBtn.Open + "_"))
            {
                #region //打开数据源
                string item = btn.ID.Substring((NamesOfBtn.Open + "_").Length);
                string value = Pub1.GetTBByID("TB_" + item).Text;
                string srcno = Pub1.GetDDLByID("DDL_DBSrcs").SelectedItemStringVal;
                SFDBSrc src = new SFDBSrc(srcno);
                DataTable dt = null;

                try
                {
                    dt = src.RunSQLReturnTable(value, 0, 100);
                }
                catch (Exception ex)
                {
                    Alert(ex.Message, "error");
                    return;
                }

                Dictionary<string, string> coldefs = gvarSModel.Val == "1" ? _oneones[item] : _onemores[item];

                Pub2.Add("<table class='easyui-datagrid' data-options='fit:true'>");

                Pub2.Add("  <thead>");
                Pub2.Add("      <tr>");

                foreach (KeyValuePair<string, string> coldef in coldefs)
                {
                    Pub2.Add(string.Format("          <th data-options=\"field:'{0}'\">{0}</th>", coldef.Key));
                }

                Pub2.Add("      </tr>");
                Pub2.Add("  </thead>");

                Pub2.Add("  <tbody>");

                foreach (DataRow row in dt.Rows)
                {
                    Pub2.Add("      <tr>");

                    foreach (KeyValuePair<string, string> coldef in coldefs)
                    {
                        if (dt.Columns.Contains(coldef.Key))
                        {
                            Pub2.Add(string.Format("          <td>{0}</td>",
                                                   row[coldef.Key] == null || row[coldef.Key] == DBNull.Value
                                                       ? ""
                                                       : row[coldef.Key]));
                        }
                        else
                        {
                            Pub2.Add("          <td></td>");
                        }
                    }

                    Pub2.Add("      </tr>");
                }

                Pub2.Add("  </tbody>");

                Pub2.Add("</table>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "showwindow", "$(function(){$('#datawin').window('open');});",
                                                       true);
                #endregion
            }
            else
            {
                switch (btn.ID)
                {
                    case NamesOfBtn.Save:
                        #region //保存并继续
                        switch (Step)
                        {
                            case 1:
                                GloVar gvar = new GloVar("StructureModel");
                                gvar.Val = Pub1.GetRadioButtonListByID("Rads_StructureModel").SelectedValue;
                                gvar.Update();
                                Response.Redirect("Integration.aspx?step=2", true);
                                break;
                            case 2:
                                gvar = new GloVar("StructureMngKind");
                                gvar.Val = Pub1.GetRadioButtonListByID("Rads_StructureMngKind").SelectedValue;
                                gvar.Update();
                                Response.Redirect("Integration.aspx?step=3", true);
                                break;
                            case 3:
                                gvar = new GloVar("StructureSource");
                                gvar.Val = Pub1.GetRadioButtonListByID("Rads_StructureSource").SelectedValue;
                                gvar.Update();
                                Response.Redirect("Integration.aspx?step=4", true);
                                break;
                        }
                        #endregion
                        break;
                    case NamesOfBtn.Back:
                        #region //上一步
                        switch (Step)
                        {
                            case 2:
                                Response.Redirect("Integration.aspx?step=1", true);
                                break;
                            case 3:
                                Response.Redirect("Integration.aspx?step=2", true);
                                break;
                            case 4:
                                Response.Redirect("Integration.aspx?step=3", true);
                                break;
                        }
                        #endregion
                        break;
                    case NamesOfBtn.Setting:
                        #region //设置全部
                        //循环文本控件，保存所有SQL
                        Dictionary<string, Dictionary<string, string>> coldefs = gvarSModel.Val == "1" ? _oneones : _onemores;
                        TB tb = null;
                        GloVar tvar = null;

                        try
                        {
                            foreach (KeyValuePair<string, Dictionary<string, string>> def in coldefs)
                            {
                                tb = Pub1.GetTBByID("TB_" + def.Key);
                                tvar = gvars.GetEntityByKey(def.Key) as GloVar;
                                tvar.Val = tb.Text;
                                tvar.Update();

                                //todo:此处建立视图逻辑，待处理
                            }

                            //todo:oneone下部门人员表SQL自动写入，待处理

                            Alert("全部设置成功！但还未创建视图，待创建！！！！");
                        }
                        catch (Exception ex)
                        {
                            Alert(ex.Message, "error");
                        }
                        #endregion
                        break;
                }
            }
        }

        /// <summary>
        /// 获取或创建全局变量
        /// <remarks>如果已经存在要检查的全局变量，则获取此变量；否则创建此变量，并获取</remarks>
        /// </summary>
        /// <param name="vars">全局变量集合</param>
        /// <param name="varno">要检查的全局变量编号</param>
        /// <param name="varname">要检查的全局变量名称</param>
        /// <returns></returns>
        private GloVar CheckGloVar(GloVars vars, string varno, string varname)
        {
            GloVar gvar = vars.GetEntityByKey(varno) as GloVar;

            if (gvar == null)
            {
                gvar = new GloVar();
                gvar.No = varno;
                gvar.Name = varname;
                gvar.GroupKey = "IntegrationSet";
                gvar.Val = "";
                gvar.Save();
            }

            return gvar;
        }

        /// <summary>
        /// Pub1中添加LinkBtn按钮
        /// </summary>
        /// <param name="btnId">按钮的id</param>
        /// <param name="btnText">按钮的文字</param>
        private void AddButton(string btnId, string btnText)
        {
            LinkBtn btn = new LinkBtn(false, btnId, "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + btnText);
            btn.Click += new EventHandler(btn_Click);
            Pub1.Add(btn);
        }

        /// <summary>
        /// 前端弹出easyui-messager
        /// </summary>
        /// <param name="msg">包含的信息</param>
        /// <param name="type">图标类型，info/error/warning/question</param>
        private void Alert(string msg, string type = "info")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "msginfo", "$.messager.alert('提示', '" + msg.Replace("\'", "\\\'") + "','" + type + "');", true);
        }
    }
}
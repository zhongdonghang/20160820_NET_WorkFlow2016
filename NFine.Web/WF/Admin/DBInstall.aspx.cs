﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Sys;
using BP.DA;

namespace CCFlow.WF.Admin
{
    public partial class WF_Admin_DBInstall : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 检查一下数据库是否链接成功.
            try
            {
                switch (BP.Sys.SystemConfig.AppCenterDBType)
                {
                    case DBType.MSSQL:
                        BP.DA.DBAccess.RunSQLReturnString("SELECT 1+2 ");
                        break;
                    case DBType.Oracle:
                        BP.DA.DBAccess.RunSQLReturnString("SELECT 1+2 FROM DUAL ");
                        break;
                    case DBType.Informix:
                        BP.DA.DBAccess.RunSQLReturnString("SELECT 1+2 FROM DUAL ");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                this.Response.Write("<h1>数据库连接错误</h1><hr> <font color=red>请参考安装说明书检查web.config数据库连接设置，错误信息:</font><br>" + ex.Message);
                return;
            }
            #endregion

            this.Pub1.AddH3("ccflow 数据库修复与安装工具");
            this.Pub1.AddHR();
            if (this.Request.QueryString["DoType"] == "OK")
            {
                this.Pub1.AddFieldSet("提示");
                this.Pub1.Add("ccflow数据库初始化成功.");

                this.Pub1.AddBR("<a href='./XAP/Designer.aspx?IsCheckUpdate=1' >进入流程设计器.</a>");
                this.Response.Redirect("./XAP/Designer.aspx?IsCheckUpdate=1", true);

                this.Pub1.AddFieldSetEnd();
                return;
            }

            if (BP.DA.DBAccess.IsExitsObject("WF_Flow") == true)
            {
                this.Pub1.AddFieldSet("提示");
                this.Pub1.Add("数据已经安装，如果您要重新安装，您需要手工的清除数据库里对象。");
                this.Pub1.AddFieldSetEnd();

                this.Pub1.AddFieldSet("修复数据表");
                this.Pub1.Add("把最新的版本的与当前的数据表结构，做一个自动修复, 修复内容：缺少列，缺少列注释，列注释不完整或者有变化。");
                this.Pub1.AddB("<a href='DBInstall.aspx?DoType=FixDB' >开始执行数据库修复</a>。");
                this.Pub1.AddFieldSetEnd();

                this.Pub1.AddFieldSet("流程设计器");
                this.Pub1.AddB("<br><a href='/' >进入流程设计器</a>,执行设计与调试流程。");
                this.Pub1.AddFieldSetEnd();


                if (this.Request.QueryString["DoType"] == "FixDB")
                {
                    string rpt = BP.Sys.PubClass.DBRpt(BP.DA.DBCheckLevel.High);
                    this.Pub1.AddMsgGreen("同步数据表结构成功, 部分错误不会影响系统运行.",
                        "执行成功，希望在系统每次升级后执行此功能，不会对你的数据库数据产生影响。<br><br> <a href='./XAP/Designer.aspx'>进入流程设计器.</a>");
                }
                return;
            }

            #region 检查是否连接上GPM
            if (BP.WF.Glo.OSModel == BP.Sys.OSModel.OneMore)
            {
                //首先检查是否安装上了GPM.
                try
                {
                    //  CCPortal.API.CheckIsConn();
                }
                catch
                {

                    string msg = "当前ccflow的工作模式为集成模式，您没有安装或者成功配制CCGPM, ccflow的BPM工作模式，必须依赖CCGPM才能运行,您可以按照如下方式处理.";
                    msg += "<ul>";
                    msg += "<li>1，使用ccflow的 workflow 模式， 把web.config 中的OSMode 修改成 0 。</li>";
                    msg += "<li>2，使用ccflow的 GPM 模式， 安装ccgpm，正确的配置ccflow连接GPM的连接。</li>";
                    msg += "</ul>";

                    this.Pub1.AddFieldSetRed("错误:",
                        msg);
                    return;
                }
            }
            #endregion 检查是否连接上GPM


            this.Pub1.AddFieldSet("选择安装语言(ccflow5仅支持中文).");
            BP.WF.XML.Langs langs = new BP.WF.XML.Langs();
            langs.RetrieveAll();

            RadioButton rb = new RadioButton();
            foreach (BP.WF.XML.Lang lang in langs)
            {
                rb = new RadioButton();
                rb.Text = lang.Name;
                rb.ID = "RB_" + lang.No;
                rb.GroupName = "ch";
                if (lang.No == "CH")
                    rb.Checked = true;
                else
                    rb.Checked = false;
                rb.Enabled = false;
                this.Pub1.Add(rb);
            }
            this.Pub1.GetRadioButtonByID("RB_CH").Checked = true;
            this.Pub1.AddFieldSetEndBR();

            #region 数据库类型.
            this.Pub1.AddFieldSet("当前数据库安装类型(如果要修改数据库类型请修改 web.config AppCenterDSNType 设置。).");
            rb = new RadioButton();
            rb.Text = "SQLServer2000,2005,2008系列版本";
            rb.ID = "RB_SQL";
            rb.GroupName = "sd";
            if (BP.Sys.SystemConfig.AppCenterDBType == DBType.MSSQL)
                rb.Checked = true;
            else
                rb.Checked = false;

            rb.Enabled = false;
            this.Pub1.Add(rb);
            this.Pub1.AddBR();

            rb = new RadioButton();
            rb.Text = "Oracle,Oracle9i,10g系列版本";
            rb.ID = "RB_Oracle";
            rb.GroupName = "sd";
            rb.Enabled = false;

            if (BP.Sys.SystemConfig.AppCenterDBType == DBType.Oracle)
                rb.Checked = true;
            else
                rb.Checked = false;

            this.Pub1.Add(rb);
            this.Pub1.AddBR();

            rb = new RadioButton();
            rb.Text = "Informix 系列版本(首先需要执行:D:\\ccflow\\trunk\\CCFlow\\WF\\Data\\Install\\Informix.sql)";
            rb.ID = "RB_DB2";
            rb.GroupName = "sd";
            rb.Enabled = false;

            if (BP.Sys.SystemConfig.AppCenterDBType == DBType.Informix)
                rb.Checked = true;
            else
                rb.Checked = false;

            this.Pub1.Add(rb);
            this.Pub1.AddBR();

            rb = new RadioButton();
            rb.Text = "MySQL系列版本";
            rb.ID = "RB_MYSQL";
            rb.GroupName = "sd";
            rb.Enabled = false;

            if (BP.Sys.SystemConfig.AppCenterDBType == DBType.MySQL)
                rb.Checked = true;
            else
                rb.Checked = false;

            this.Pub1.Add(rb);
            this.Pub1.AddBR();
            this.Pub1.AddFieldSetEnd();
            #endregion 数据库类型.

            #region 安装模式.
            this.Pub1.AddFieldSet("ccflow的运行模式,手工修改 web.config 中的 OSModel 进行配置. ");
            rb = new RadioButton();
            rb.Text = "集成模式";
            rb.ID = "RB_WorkFlow";
            rb.GroupName = "model";
            if (BP.WF.Glo.OSModel == BP.Sys.OSModel.OneOne)
                rb.Checked = true;
            rb.Enabled = false;
            this.Pub1.Add(rb);

            rb = new RadioButton();
            rb.Text = "BPM工作模式";
            rb.ID = "RB_BMP";
            rb.GroupName = "model";
            if (BP.WF.Glo.OSModel == BP.Sys.OSModel.OneMore)
                rb.Checked = true;
            rb.Enabled = false;
            this.Pub1.Add(rb);
            this.Pub1.AddBR();
            this.Pub1.AddFieldSetEnd();
            #endregion 安装模式.

            this.Pub1.AddFieldSet("是否需要安装CCIM.");
            rb = new RadioButton();
            rb.Text = "是";
            rb.ID = "RB_CCIM_Y";
            rb.Checked = true;
            rb.GroupName = "ccim";
            this.Pub1.Add(rb);
            rb = new RadioButton();
            rb.Text = "否";
            rb.ID = "RB_CCIM_N";
            rb.GroupName = "ccim";
            this.Pub1.Add(rb);
            this.Pub1.AddBR();
            this.Pub1.AddFieldSetEnd();


            this.Pub1.AddFieldSet("应用环境模拟.");
            rb = new RadioButton();
            rb.Text = "集团公司，企业单位。";
            rb.ID = "RB_Inc";
            rb.GroupName = "hj";
            rb.Checked = true;
            rb.Enabled = false;
            this.Pub1.Add(rb);
            this.Pub1.AddBR();
            rb = new RadioButton();
            rb.Text = "政府机关，事业单位。";
            rb.ID = "RB_Gov";
            rb.GroupName = "hj";
            rb.Enabled = false;
            this.Pub1.Add(rb);
            this.Pub1.AddBR();
            this.Pub1.AddFieldSetEndBR();


            this.Pub1.AddFieldSet("是否装载演示流程模板?");
            rb = new RadioButton();
            rb.Text = "是:我要安装demo组织结构体系、demo流程模板、表单模板，以方便我学习ccflow与ccform.";
            rb.ID = "RB_DemoOn";
            rb.GroupName = "hjd";
            rb.Checked = true;
            this.Pub1.Add(rb);
            this.Pub1.AddBR();
            rb = new RadioButton();
            rb.Text = "否:不安装demo。";
            rb.ID = "RB_DemoOff";
            rb.GroupName = "hjd";
            this.Pub1.Add(rb);
            this.Pub1.AddBR();
            this.Pub1.AddFieldSetEndBR();

            Button btn = new Button();
            btn.ID = "Btn_s";
            btn.Text = "接受CCFlow 6 的 GPL开源软件协议并安装";
            btn.CssClass = "Btn";
            btn.UseSubmitBehavior = false;
            btn.OnClientClick = "this.disabled=true;";
            btn.Click += new EventHandler(btn_Click);
            this.Pub1.Add(btn);

            this.Pub1.AddBR();
            this.Pub1.AddBR("1,估计在<font color=red size=10px>10分钟</font>内安装完成.");
            this.Pub1.AddBR("2,如果您是用VS打开的请不要用F5运行它,会导致安装变慢.");
            this.Pub1.AddBR("3.如果安装错误,请删除数据库表并重新安装,或者把安装遇到的问题反馈给ccflow开发团队. <a href='http://bbs.ccflow.org' target=_blank >ccflow 技术论坛</a>");
        }
        void btn_Click(object sender, EventArgs e)
        {

            string lang = "CH";
            // 首先安装GPM.
            BP.GPM.Glo.DoInstallDataBase(lang, "Inc");
             
            //是否要安装demo.
            bool isDemo = this.Pub1.GetRadioButtonByID("RB_DemoOn").Checked;

            if (BP.WF.Glo.OSModel == BP.Sys.OSModel.OneMore)
            {
                //运行GPM的安装
                BP.GPM.Glo.DoInstallDataBase(lang,"Inc");
            }

            //运行ccflow的安装
            BP.WF.Glo.DoInstallDataBase(lang, isDemo);

            //安装CCIM
            if (this.Pub1.GetRadioButtonByID("RB_CCIM_Y").Checked)
            {
                BP.WF.Glo.DoInstallCCIM();
            }
             
            //加注释.
            BP.Sys.PubClass.AddComment();

           
          
            this.Response.Redirect("DBInstall.aspx?DoType=OK", true);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF;
using BP.Port;
using BP.Web;
using BP.En;
using BP.WF.Template;
//using CCPortal.DA;
using System.Data;

namespace LBQZFBpm.WF.WorkOpt
{
    public partial class ShipLeader : BP.Web.WebPage
    {
        #region 属性
        public int ToNodes
        {
            get
            {
                return int.Parse(this.Request.QueryString["ToNodes"]);
            }
        }
        public string FK_Flow
        {
            get
            {
                return this.Request.QueryString["FK_Flow"];
            }
        }
        public int FK_Node
        {
            get
            {
                return int.Parse(this.Request.QueryString["FK_Node"]);
            }
        }

        public Int64 WorkID
        {
            get
            {
                return Int64.Parse(this.Request.QueryString["WorkID"]);
            }
        }
        public Int64 FID
        {
            get
            {
                if (this.Request.QueryString["FID"] != null)
                    return Int64.Parse(this.Request.QueryString["FID"]);
                return 0;
            }
        }
        public string CFlowNo
        {
            get
            {
                return this.Request.QueryString["CFlowNo"];
            }
        }
        public string WorkIDs
        {
            get
            {
                return this.Request.QueryString["WorkIDs"];
            }
        }
        public string DoFunc
        {
            get
            {
                return this.Request.QueryString["DoFunc"];
            }
        }
        #endregion 属性
        protected void Page_Load(object sender, EventArgs e)
        {
            //首先去找到开始节点的发起部门编号
            string str = "select * from wf_generworkflow where WORKID='"+this.WorkID.ToString()+"'";
            DataTable dtl = BP.DA.DBAccess.RunSQLReturnTable(str);
            //根据部门编号，查询所属的分管领导
            string sql = "select * from port_shipleader where FK_DEPT='"+dtl.Rows[0]["FK_DEPT"]+"'";
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                this.Pub1.AddBR(); this.Pub1.AddBR();
                BP.Web.Controls.RadioBtn rb = new BP.Web.Controls.RadioBtn();
                rb = new BP.Web.Controls.RadioBtn();
                rb.GroupName = "s";
                rb.Text = row["FK_EMPNAME"].ToString();
                rb.ID = "RB_" + row["FK_EMP"].ToString();
                rb.Attributes["onclick"] = "SetUnEable(this);";
                this.Pub1.Add(rb);
                this.Pub1.AddBR();
            }
            this.Pub1.AddBR();
            this.Pub1.AddBR();
            this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;"); this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;");
            this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;"); this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;");
            this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;"); this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;");
            this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;"); this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;");
            this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;"); this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;");
            this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;"); this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;");
            this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;"); this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;");
            this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;"); this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;");
            this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;"); this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;");
            this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;"); this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;");
            this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;"); this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;");
            Button btn = new Button();
            btn.ID = "To";
            btn.Text = "  执 行  ";
            this.Pub1.Add(btn);
            btn.Click += new EventHandler(btn_Click);
            this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;"); this.Pub1.Add("&nbsp;&nbsp;&nbsp;&nbsp;");
            btn = new Button();
            btn.ID = "Btn_Cancel";
            btn.Text = "取消/返回";
            this.Pub1.Add(btn);
            btn.Click += new EventHandler(btn_Click);
            this.Pub1.AddBR();
            this.Pub1.AddBR();
        }
        void btn_Click(object sender, EventArgs e)
        {
            Nodes nds = new Nodes();
            nds = BP.WF.Dev2Interface.WorkOpt_GetToNodes(this.FK_Flow, this.FK_Node, this.WorkID, this.FID);

            Button btn = (Button)sender;
            if (btn.ID == "Btn_Cancel")
            {
                string url = "../MyFlow.aspx?FK_Flow=" + this.FK_Flow + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID + "&FID=" + this.FID;
                this.Response.Redirect(url, true);
                return;
            }

            //首先去找到开始节点的发起部门编号
            string str = "select * from wf_generworkflow where WORKID='" + this.WorkID.ToString() + "'";
            DataTable dtl = BP.DA.DBAccess.RunSQLReturnTable(str);
            string emps = "";
            string sql = "select * from port_shipleader where FK_DEPT='" + dtl.Rows[0]["FK_DEPT"] + "'";
            DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                BP.Web.Controls.RadioBtn rb = this.Pub1.GetRadioBtnByID("RB_" + row["FK_EMP"].ToString());
                if (rb.Checked == false)
                    continue;
                emps = row["FK_EMP"].ToString();
            }

            if (emps.Length < 2)
            {
                this.Alert("您没有选择人员。");
                return;
            }
            int toNodes = this.ToNodes;
            if (this.ToNodes == 0)
            {
                foreach (Node mynd in nds)
                {
                    toNodes = mynd.NodeID;
                }
            }
            //设置人员.
            BP.WF.Dev2Interface.WorkOpt_SetAccepter(toNodes, this.WorkID, this.FID, emps, false);
            this.DoSend();
            return;
        }
        public void DoSend()
        {
            // 以下代码是从 MyFlow.aspx Send 方法copy 过来的，需要保持业务逻辑的一致性，所以代码需要保持一致.

            Nodes nds = new Nodes();
            nds = BP.WF.Dev2Interface.WorkOpt_GetToNodes(this.FK_Flow, this.FK_Node, this.WorkID, this.FID);
            int toNodes = this.ToNodes;
            if (this.ToNodes == 0)
            {
                foreach (Node mynd in nds)
                {
                    toNodes = mynd.NodeID;
                }
            }

            BP.WF.Node nd = new BP.WF.Node(this.FK_Node);
            Work wk = nd.HisWork;
            wk.OID = this.WorkID;
            wk.Retrieve();

            string msg = "";
            try
            {
                msg = BP.WF.Dev2Interface.WorkOpt_SendToNodes(this.FK_Flow,
                        this.FK_Node, this.WorkID, this.FID, toNodes.ToString()).ToMsgOfHtml();
            }
            catch (Exception exSend)
            {
                this.Pub1.AddFieldSetGreen("错误");
                this.Pub1.Add(exSend.Message.Replace("@@", "@").Replace("@", "<BR>@"));
                this.Pub1.AddFieldSetEnd();
                return;
            }

            #region 处理通用的发送成功后的业务逻辑方法，此方法可能会抛出异常.
            try
            {
                //处理通用的发送成功后的业务逻辑方法，此方法可能会抛出异常.
                BP.WF.Glo.DealBuinessAfterSendWork(this.FK_Flow, this.WorkID, this.DoFunc, WorkIDs, this.CFlowNo, 0, null);
            }
            catch (Exception ex)
            {
                this.ToMsg(msg, ex.Message);
                return;
            }
            #endregion 处理通用的发送成功后的业务逻辑方法，此方法可能会抛出异常.


            /*处理转向问题.*/
            switch (nd.HisTurnToDeal)
            {
                case TurnToDeal.SpecUrl:
                    string myurl = nd.TurnToDealDoc.Clone().ToString();
                    if (myurl.Contains("?") == false)
                        myurl += "?1=1";
                    myurl = BP.WF.Glo.DealExp(myurl, wk, null);
                    myurl += "&FromFlow=" + this.FK_Flow + "&FromNode=" + this.FK_Node + "&PWorkID=" + this.WorkID + "&UserNo=" + WebUser.No + "&SID=" + WebUser.SID;
                    this.Response.Redirect(myurl, true);
                    return;
                case TurnToDeal.TurnToByCond:
                    TurnTos tts = new TurnTos(this.FK_Flow);
                    if (tts.Count == 0)
                        throw new Exception("@您没有设置节点完成后的转向条件。");
                    foreach (TurnTo tt in tts)
                    {
                        tt.HisWork = wk;
                        if (tt.IsPassed == true)
                        {
                            string url = tt.TurnToURL.Clone().ToString();
                            if (url.Contains("?") == false)
                                url += "?1=1";
                            url = BP.WF.Glo.DealExp(url, wk, null);
                            url += "&PFlowNo=" + this.FK_Flow + "&FromNode=" + this.FK_Node + "&PWorkID=" + this.WorkID + "&UserNo=" + WebUser.No + "&SID=" + WebUser.SID;
                            this.Response.Redirect(url, true);
                            return;
                        }
                    }
#warning 为上海修改了如果找不到路径就让它按系统的信息提示。
                    this.ToMsg(msg, "info");
                    //throw new Exception("您定义的转向条件不成立，没有出口。");
                    break;
                default:
                    this.ToMsg(msg, "info");
                    break;
            }
            //附件数据的流转
            //先去附件数据表查询，当前节点有没有上传附件
            string fuSql = "select * from sys_frmattachmentdb where REFPKVAL='" + this.WorkID + "' and FK_MAPDATA='ND" + this.FK_Node + "'";
            DataTable fuDt = BP.DA.DBAccess.RunSQLReturnTable(fuSql);
            if (fuDt.Rows.Count > 0)
            {
                //如果有，就去轨迹图中查询已经到达的节点
                string fuTrack = "select NDTO from ND" + int.Parse(this.FK_Flow) + "Track where WORKID='" + this.WorkID + "' and NDFROM='" + this.FK_Node + "'";
                DataTable tk = BP.DA.DBAccess.RunSQLReturnTable(fuTrack);

                //再查询附件表中，有没有到达节点的信息
                string toSql = "select * from sys_frmattachmentdb where REFPKVAL='" + this.WorkID + "' and FK_MAPDATA='ND" + tk.Rows[0]["NDTO"] + "'";
                DataTable ndt = BP.DA.DBAccess.RunSQLReturnTable(toSql);

                //如果存在，就将附件二进制copy到到达节点
                if (ndt.Rows.Count > 0)
                {
                    for (int i = 0; i < fuDt.Rows.Count; i++)
                    {
                        for (int j = 0; j < ndt.Rows.Count; j++)
                        {
                            //单附件
                            if (ndt.Rows[j]["MyPK"].ToString().Contains("DanFuJian") && fuDt.Rows[i]["MyPK"].ToString().Contains("DanFuJian"))
                            {
                                string sql = "select * from Sys_FrmAttachmentDB where MyPK='" + ndt.Rows[j]["MyPK"] + "'";
                                //DataSet ds = LbqOA.DBAccess.GetSingleDataSet("file", sql);
                                //ds.Tables["file"].Rows[0]["FDB"] = fuDt.Rows[i]["FDB"];
                                //LbqOA.DBAccess.SaveSingleDataSet(ds, "file", sql);
                            }
                            //多附件
                            if (ndt.Rows[j]["MyPK"].ToString().Contains("DanFuJian") == false && fuDt.Rows[i]["MyPK"].ToString().Contains("DanFuJian") == false)
                            {
                                if (fuDt.Rows[i]["FILENAME"].ToString() == ndt.Rows[j]["FILENAME"].ToString())
                                {
                                    string sql = "select * from Sys_FrmAttachmentDB where MyPK='" + ndt.Rows[j]["MyPK"] + "'";
                                    //DataSet ds = LbqOA.DBAccess.GetSingleDataSet("file", sql);
                                    //ds.Tables["file"].Rows[0]["FDB"] = fuDt.Rows[i]["FDB"];
                                    //LbqOA.DBAccess.SaveSingleDataSet(ds, "file", sql);
                                }
                            }
                        }
                    }
                }
            }
            return;
        }

        public void ToMsg(string msg, string type)
        {
            this.Session["info"] = msg;
            this.Application["info" + WebUser.No] = msg;

            BP.WF.Glo.SessionMsg = msg;
            this.Response.Redirect("./../MyFlowInfo.aspx?FK_Flow=" + this.FK_Flow + "&FK_Type=" + type + "&FK_Node=" + this.FK_Node + "&WorkID=" + this.WorkID, false);
        }
    }
}
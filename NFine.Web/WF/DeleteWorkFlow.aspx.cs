using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP;
using BP.Web;
using BP.En;
using BP.DA;
using BP.Web.Controls;
using BP.WF;
using BP.Sys;

namespace CCFlow.WF
{
    public partial class DeleteWorkFlow : PageBase
    {
        public string FK_Flow
        {
            get
            {
                string s = this.Request.QueryString["FK_Flow"];
                if (string.IsNullOrEmpty(s))
                    s = "200";
                return s;
            }
        }

        public int WorkID
        {
            get
            {
                string s = this.Request.QueryString["WorkID"];
                if (string.IsNullOrEmpty(s))
                    s = "0";
                return int.Parse(s);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                WorkFlow wf = new WorkFlow(this.FK_Flow, this.WorkID);
                string delInfo = wf.DoDeleteWorkFlowByReal(true);
                this.Pub1.Add(delInfo);
            }
            catch (Exception ex)
            {
                this.Response.Write(ex.Message);
                this.Alert(ex.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CCFlow.WF.App.Amaz
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userNo = this.Request.QueryString["UserNo"];
                string fk_flow = this.Request.QueryString["FK_Flow"];
                string fk_node = this.Request.QueryString["FK_Node"];
                string DoWhat = this.Request.QueryString["DoWhat"];
                if (fk_flow != null)
                {
                    string url = "../../MyFlow.aspx?FK_Flow=" + fk_flow + "&FK_Node=" + fk_node + "&DoWhat=" + DoWhat;
                    BP.Sys.PubClass.WinOpen(url, 800, 600);

                }
            }
        }
    }
}
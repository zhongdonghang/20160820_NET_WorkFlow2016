using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CCFlow.WF.Admin.BPMN
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(BP.Web.WebUser.No) || BP.Web.WebUser.No != "admin")
                Response.Redirect("Login.aspx?DoType=Logout");
        }
    }
}
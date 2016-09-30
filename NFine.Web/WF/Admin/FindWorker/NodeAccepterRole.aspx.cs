using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.WF.Template;
using BP.WF;

namespace CCFlow.WF.Admin.FlowNodeAttr
{
    public partial class NodeAccepterRole :BP.Web.WebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btn_Save_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        protected void Btn_SaveAndClose_Click(object sender, EventArgs e)
        {
            this.Save();
            this.WinClose();
        }
        public void Save()
        {

        }
    }
}
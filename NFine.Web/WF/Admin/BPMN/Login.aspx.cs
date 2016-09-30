using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BP.Web;

namespace CCFlow.WF.Admin.BPMN
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString["DoType"] == "Logout")
                BP.Web.WebUser.Exit();

            if (this.Request.QueryString["DoType"] == "Login")
            {
                string user = TB_UserName.Value.Trim();
                string pass = TB_Password.Value.Trim();
                try
                {
                    if (WebUser.No != null)
                        WebUser.Exit();

                    BP.Port.Emp em = new BP.Port.Emp(user);
                    if (em.CheckPass(pass))
                    {
                        WebUser.SignInOfGenerLang(em, WebUser.SysLang, false);
                        WebUser.Token = this.Session.SessionID;
                        this.Response.Redirect("Default.aspx?DDD=" + em.No, false);
                        return;
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "kesy", "<script language=JavaScript>alert('用户名密码错误，注意密码区分大小写，请检查是否按下了CapsLock.。');</script>");
                    }
                }
                catch (System.Exception ex)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "kesy", "<script language=JavaScript>alert('@用户名密码错误!@检查是否按下了CapsLock.@更详细的信息:" + ex.Message + "');</script>");
                }
            }
        }
    }
}
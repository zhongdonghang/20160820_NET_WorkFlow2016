using System.Web.Mvc;

namespace NFine.Web.Areas.CC
{
    public class CCAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CC";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CC_default",
                "CC/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

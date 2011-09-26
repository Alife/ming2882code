using System.Web.Mvc;

namespace Web.Areas.Sys
{
    public class SysAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Sys"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Sys_default",
                "Sys/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Web.Controllers" } 
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            if ((Response.ContentType == "text/html" || Response.ContentType == "application/json" || Response.ContentType == "application/x-javascript")
                && (
                    Request.Url.Segments.Length == 1
                    || Request.Url.Segments.Length > 1 && !Common.Utils.gl(Request.Url.Segments[1].TrimEnd('/'), "ext|css|images")
                    && Request.Url.AbsolutePath.ToLower().TrimStart('/').TrimEnd('/') != "validatecode")
                )
                Response.Filter = new Common.CG2BFilter(Response.Filter);
        }
    }
}
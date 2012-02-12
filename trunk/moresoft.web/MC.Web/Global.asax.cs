using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Unity.Mvc3;

namespace Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Pages", 
                "{id}.html", 
                new { controller = "Pages", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}/{id}", // 带有参数的 URL
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // 参数默认值
            );
        }
        protected void Application_PostRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpCookie lang = Request.Cookies["Lang"];
            if (lang != null)
            {
                if (Response.ContentType == "text/html" || Response.ContentType == "application/json")
                    Response.Filter = new LocalizationHandler(Response.Filter, lang.Value);
                return;
            }
            string langFromBrowser = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
            if (Response.ContentType == "text/html" || Response.ContentType == "application/json")
                Response.Filter = new LocalizationHandler(Response.Filter, langFromBrowser);
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            Bootstrapper.Initialise();
        }
    }
}
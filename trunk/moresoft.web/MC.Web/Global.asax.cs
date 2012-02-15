using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Unity.Mvc3;
using MC.DAO;
using MC.Service;
using MC.IBLL;
using Microsoft.Practices.Unity;

namespace MC.Web
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
            /* 
              <appSettings>
                <add key="csses" value="/extjs/resources/css/ext-all.css,/extjs/resources/css/ext-all-notheme.css,/css/icon.css"/>
                <add key="jses" value="/extjs/adapter/ext/ext-base-min.js,/extjs/ext-all-min.js,/extjs/src/locale/ext-lang-zh_CN.js,/extjs/ux/RowExpander.js,&#xD;&#xA;      /js/util/mc.util.js,/js/TabCloseMenu.js,/js/CookieTheme.js,/js/MenuTreePanel.js,/js/login.js,/js/app.js"/>
                <add key="blue" value="/ExtJS/resources/css/xtheme-blue.css"/>
                <add key="gray" value="/ExtJS/resources/css/xtheme-gray.css"/>
                <add key="indigo" value="/ExtJS/resources/css/xtheme-indigo.css"/>
                <add key="pink" value="/ExtJS/resources/css/xtheme-pink.css"/>
                <add key="tp" value="/ExtJS/resources/css/xtheme-tp.css"/>
              </appSettings>
              <link rel="stylesheet" type="text/css" href="/compress/cachecontent/csses/css?version=1.0"/>
		      <script type="text/javascript" src="/compress/cachecontent/jses/javascript?version=1.0"></script>
            */
            routes.Add(new Route("compress/{action}/{key}/{type}", new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(new { controller = "Compress", action = "CacheContent", key = "", type = "" }),
            });
            routes.MapRoute(
                "Pages",
                "{id}.html",
                new { controller = "Pages", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                "Industry",
                "industry/cat-{id}",
                new { controller = "Industry", action = "Index", id = UrlParameter.Optional }
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
            if (string.Compare(langFromBrowser, "zh-HK", true) == 0 || string.Compare(langFromBrowser, "zh-MO", true) == 0
                || string.Compare(langFromBrowser, "zh-SG", true) == 0 || string.Compare(langFromBrowser, "zh-TW", true) == 0)
                langFromBrowser = "zh-Hant";
            else if (string.Compare(langFromBrowser, "zh-Hans", true) == 0)
                langFromBrowser = "zh-CN";
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
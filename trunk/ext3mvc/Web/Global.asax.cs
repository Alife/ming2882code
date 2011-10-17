using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using System.IO.Compression;

namespace Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
        private void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            HttpRequest request = application.Request;
            HttpResponse response = application.Response;
            //if (request.RawUrl.IndexOf(".jsgz") != -1)
            //{
            //    string acceptEncoding = request.Headers["Accept-Encoding"];
            //    if (string.IsNullOrEmpty(acceptEncoding)) return;
            //    acceptEncoding = acceptEncoding.ToUpperInvariant();
            //    if (acceptEncoding.Contains("GZIP"))
            //    {
            //        response.AppendHeader("Content-encoding", "gzip");
            //        response.Filter = new GZipStream(response.Filter, CompressionMode.Decompress);
            //    }
            //    else if (acceptEncoding.Contains("DEFLATE"))
            //    {
            //        response.AppendHeader("Content-encoding", "deflate");
            //        response.Filter = new DeflateStream(response.Filter, CompressionMode.Decompress);
            //    }
            //    response.AppendHeader("Content-encoding", "gzip");
            //}
        }
    }
}
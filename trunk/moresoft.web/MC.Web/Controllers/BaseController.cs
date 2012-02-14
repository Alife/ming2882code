using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Reflection;
using Microsoft.Practices.Unity;
using MC.Model;
using MC.IBLL;

namespace MC.Web.Controllers
{
    [HandleError]
    public class BaseController : Controller
    {
        [Dependency]
        public ISetting_set _Setting_setServer { get; set; }
        protected readonly log4net.ILog _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");
            var ex = filterContext.Exception ?? new Exception("No further infomation exists.");
            _logger.Error("Error general OnException", ex);
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult { Data = new { message = filterContext.Exception.Message, success = false } };
            }
            else
            {
                HandleErrorInfo data = new HandleErrorInfo(ex, (string)filterContext.RouteData.Values["controller"], (string)filterContext.RouteData.Values["action"]);
                filterContext.Controller.ViewData.Model = data;
                filterContext.Result = new ViewResult
                {
                    ViewName = "Error",
                    ViewData = filterContext.Controller.ViewData
                };
            }
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Setting = _Setting_setServer.GetItem(1);
            ViewBag.Lang = Request.Cookies["Lang"] != null ? Request.Cookies["Lang"].Value : System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
            base.OnActionExecuting(filterContext);
        }
    }
}
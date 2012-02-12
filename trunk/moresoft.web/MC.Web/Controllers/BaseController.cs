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
            _logger.Error(string.Format("\r\n Unhandled exception: {0}.\r\n Stack trace: {1}\r\n{2}----------------"
                , filterContext.Exception.Message, filterContext.Exception.StackTrace, User.Identity.Name));
            base.OnException(filterContext);
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Setting = _Setting_setServer.GetItem(1);
            ViewBag.Lang = Request.Cookies["Lang"] != null ? Request.Cookies["Lang"].Value : System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
            base.OnActionExecuting(filterContext);
        }
    }
}
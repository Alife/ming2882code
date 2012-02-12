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
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(filterContext.Exception.Message
                + (filterContext.Exception.InnerException != null ? filterContext.Exception.InnerException.Message : ""));
            sb.AppendLine("1.错误：" + filterContext.Exception.HelpLink);
            sb.AppendLine("2.错误：" + filterContext.Exception.Source);
            sb.AppendLine("3.错误：" + filterContext.Exception.StackTrace);
            sb.AppendLine("4.错误：" + filterContext.Exception.TargetSite);
            string user = System.Web.HttpContext.Current.User.Identity.Name;
            if (string.IsNullOrEmpty(user)) user = "游客";
            sb.Append("\r\n" + user + "----------------");
            _logger.Error(sb.ToString());
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
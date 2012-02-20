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
    public class BaseController : Controller
    {
        [Dependency]
        public ISetting_set _Setting_setServer { get; set; }
        [Dependency]
        public IInfoType_ift _InfoType_iftServer { get; set; }
        [Dependency]
        public IPage_pag _Page_pagServer { get; set; }
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
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Setting = _Setting_setServer.GetItem(1);
            ViewBag.Lang = Request.Cookies["Lang"] != null ? Request.Cookies["Lang"].Value : System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
            ViewBag.InfoType = LoadInfoTypesChild(0);
            base.OnActionExecuting(filterContext);
        }
        public IList<InfoType_ift> LoadInfoTypesChild(int parentID)
        {
            QueryInfo info = new QueryInfo();
            info.Parameters.Add("Parent_ift", parentID);
            info.Orderby.Add("Sort_ift", null);
            var list = _InfoType_iftServer.GetList(info);
            foreach (var item in list)
                item.children = item.IsHasChild_ift.Value ? LoadInfoTypesChild(item.ID_ift.Value) : null;
            return list;
        }
        public IList<Page_pag> LoadPagesChild(int parentID)
        {
            QueryInfo info = new QueryInfo();
            info.Parameters.Add("Parent_pag", parentID);
            info.Orderby.Add("Sort_pag", null);
            var list = _Page_pagServer.GetList(info);
            foreach (var item in list)
                item.children = item.IsHasChild_pag.Value ? LoadPagesChild(item.ID_pag.Value) : null;
            return list;
        }
    }
}
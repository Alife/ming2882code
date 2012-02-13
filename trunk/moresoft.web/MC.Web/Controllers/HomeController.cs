using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MC.Model;
using MC.IBLL;

namespace MC.Web.Controllers
{
    public class HomeController : BaseController
    {
        [Dependency]
        public Imc_User _mc_UserService { get; set; }
        [Dependency]
        public ILink_lnk _Link_lnkService { get; set; }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewBag.MetaTitle = ViewBag.Setting.Title_set;
            ViewBag.MetaKeywords = ViewBag.Setting.Keywords_set;
            ViewBag.MetaAuthor = ViewBag.Setting.Author_set;
            base.OnActionExecuted(filterContext);
        }
        [Unity.Mvc3.Filter.CompressFilter]
        public ActionResult Index()
        {
            var linkqi = new MC.Model.QueryInfo(); 
            linkqi.Parameters.Add("IsHide", true);
            linkqi.Parameters.Add("top", 20);
            linkqi.Orderby.Add("Sort_lnk","desc");
            ViewBag.Link = _Link_lnkService.GetList(linkqi);
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
    }
}

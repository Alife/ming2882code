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
        #region properties
        [Dependency]
        public IUser_usr _User_usrService { get; set; }
        [Dependency]
        public ILink_lnk _Link_lnkService { get; set; }
        [Dependency]
        public IPage_pag _Page_pagService { get; set; }
        #endregion
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
            var linkqi = new QueryInfo();
            linkqi.Parameters.Add("IsHide_lnk", false);
            linkqi.Parameters.Add("top", 20);
            linkqi.Orderby.Add("Sort_lnk","desc");
            ViewBag.Link = _Link_lnkService.GetList(linkqi);

            var caseqi = new QueryInfo();
            caseqi.Parameters.Add("Parent_pag", _Page_pagService.GetItem("case").ID_pag);
            caseqi.Parameters.Add("top", 4);
            caseqi.Orderby.Add("Sort_pag", "desc");
            ViewBag.CasePages = _Page_pagService.GetList(caseqi);
            return View();
        }
        public ActionResult error()
        {
            return View();
        }
    }
}

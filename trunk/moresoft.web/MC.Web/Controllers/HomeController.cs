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
            linkqi.Orderby.Add("Sort_lnk", null);
            ViewBag.Link = _Link_lnkService.GetList(linkqi);
            //成功案例
            var caseqi = new QueryInfo();
            caseqi.Parameters.Add("Parent_pag", _Page_pagService.GetItem("case").ID_pag);
            caseqi.Parameters.Add("top", 4);
            caseqi.Orderby.Add("Sort_pag", null);
            ViewBag.CasePages = _Page_pagService.GetList(caseqi);
            //主页上几个单独模块:为什么选择制造执行系统MS-MES,MS-MES给制造业客户带来的应用体验,摩尔制造执行系统MS-MES,覆盖行业,解决方案
            var homePageQuery = new QueryInfo();
            homePageQuery.Parameters.Add("Code_pags", new List<string> { "WhyMes", "Experience", "MS-MES", "Industry", "Solution" });
            ViewBag.HomePages = _Page_pagService.GetList(homePageQuery);
            //产品体系
            var productsqi = new QueryInfo();
            productsqi.Parameters.Add("Parent_pag", _Page_pagService.GetItem("product").ID_pag);
            productsqi.Parameters.Add("top", 3);
            productsqi.Orderby.Add("Sort_pag", null);
            ViewBag.Products = _Page_pagService.GetList(productsqi).Where(p => !p.Code_pag.Contains("MS-MES"));
            return View();
        }
        public ActionResult error()
        {
            return View();
        }
    }
}

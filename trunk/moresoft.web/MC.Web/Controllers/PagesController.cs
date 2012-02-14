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
    [HandleError]
    public class PagesController : BaseController
    {
        [Dependency]
        public IPage_pag _Page_pagService { get; set; }
        public ActionResult Index(string id)
        {
            ViewBag.MetaTitle = ViewBag.Setting.Title_set;
            ViewBag.MetaKeywords = ViewBag.Setting.Keywords_set;
            ViewBag.MetaAuthor = ViewBag.Setting.Author_set;
            ViewBag.Pages = _Page_pagService.GetItem(id);
            ViewBag.ParentPage = ViewBag.Pages.Parent_pag == 0 ? ViewBag.Pages : _Page_pagService.GetItem(ViewBag.Pages.Parent_pag);
            var qi = new QueryInfo();
            qi.Parameters.Add("Parent_pag", ViewBag.Pages.Parent_pag == 0 ? ViewBag.Pages.ID_pag : ViewBag.Pages.Parent_pag);
            qi.Orderby.Add("Sort_pag", null);
            ViewBag.ChilePages = _Page_pagService.GetList(qi);
            return View();
        }
    }
}

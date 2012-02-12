using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MC.Model;
using MC.IBLL;

namespace Web.Controllers
{
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
            return View();
        }
    }
}

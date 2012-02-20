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
        public ActionResult Index(string id)
        {
            ViewBag.MetaTitle = ViewBag.Setting.Title_set;
            ViewBag.MetaKeywords = ViewBag.Setting.Keywords_set;
            ViewBag.MetaAuthor = ViewBag.Setting.Author_set;
            ViewBag.CurrentPage = _Page_pagServer.GetItem(id);
            IList<Page_pag> ParentPages = ViewBag.CurrentPage.Parent_pag == 0 ? new List<Page_pag>() : LoadPagesParent((int)ViewBag.CurrentPage.Parent_pag).Reverse().ToList();
            ViewBag.TopPage = ViewBag.CurrentPage.Parent_pag == 0 ? ViewBag.CurrentPage : ParentPages.First();
            ViewBag.ChilePages = LoadPagesChild((int)ViewBag.TopPage.ID_pag);
            if (ViewBag.CurrentPage.Parent_pag == 0)
            {
                ViewBag.Title = ViewBag.TopPage.Name_pag + "-" + ViewBag.Setting.WebName_set;
                ViewBag.Title_pag = ViewBag.TopPage.Name_pag;
            }
            else
            {
                string str = string.Empty;
                foreach (var item in ParentPages) str += string.Format("<a href=\"{0}\">{1}</a> >> ", Url.Content("~/" + item.Code_pag.ToLower() + ".html"), item.Name_pag);
                ViewBag.Title_pag = str + ViewBag.CurrentPage.Name_pag;
                ParentPages = ParentPages.Reverse().ToList(); str = string.Empty;
                foreach (var item in ParentPages) str += item.Name_pag + "-";
                ViewBag.Title = ViewBag.CurrentPage.Name_pag + "-" + str + ViewBag.Setting.WebName_set;
            }
            QueryInfo qi = new QueryInfo();
            qi.Parameters.Add("Parent_pag", ViewBag.CurrentPage.ID_pag);
            qi.Orderby.Add("Sort_pag", null);
            ViewBag.Content_pag = string.IsNullOrEmpty(ViewBag.CurrentPage.Content_pag) && ViewBag.ChilePages.Count > 0 ? _Page_pagServer.GetList(qi).First().Content_pag : ViewBag.CurrentPage.Content_pag;
            return View();
        }
        private IList<Page_pag> LoadPagesParent(int parentID)
        {
            List<Page_pag> lst = new List<Page_pag>();
            var item = _Page_pagServer.GetItem(parentID);
            lst.Add(item);
            if (item.Parent_pag > 0)
                lst.AddRange(LoadPagesParent(item.Parent_pag.Value));
            return lst;
        }
    }
}

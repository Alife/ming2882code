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
    public class NewsController : BaseController
    {
        [Dependency]
        public IInfo_inf _Info_infServer { get; set; }
        public ActionResult Index(int? id, int rows = 20, int page = 1)
        {
            QueryInfo queryInfo = new QueryInfo();
            queryInfo.Parameters.Add("rows", rows);
            queryInfo.Parameters.Add("page", page);
            queryInfo.Parameters.Add("Type_inf", (int)InfoType.News);
            ViewBag.Infos = _Info_infServer.GetPageList(queryInfo);
            ViewBag.MetaTitle = "<=News>-" + ViewBag.Setting.Title_set;
            ViewBag.MetaKeywords = "<=News>-" + ViewBag.Setting.Title_set;
            ViewBag.MetaAuthor = ViewBag.Setting.Author_set;
            ViewBag.Title = "<=News>-" + ViewBag.Setting.Title_set;
            return View();
        }
        public ActionResult Detail(int? id)
        {
            var Info = _Info_infServer.GetItem(id);
            ViewBag.MetaTitle = string.Format("{0}-{1}-{2}", Info.Title_inf, "<=News>", ViewBag.Setting.Title_set);
            ViewBag.MetaKeywords = Info.Keywords_inf;
            ViewBag.MetaAuthor = Info.Author_inf;
            ViewBag.Title = string.Format("{0}-{1}-{2}", Info.Title_inf, "<=News>", ViewBag.Setting.Title_set);
            return View(Info);
        }
    }
}

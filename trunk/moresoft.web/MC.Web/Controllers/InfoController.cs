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
    public class InfoController : BaseController
    {
        [Dependency]
        public IInfo_inf _Info_infServer { get; set; }
        public ActionResult Index(int? id, int rows = 20, int page = 1)
        {
            QueryInfo queryInfo = new QueryInfo();
            if (id.HasValue)
            {
                ViewBag.CurrentInfoType = _InfoType_iftServer.GetItem(id);
                int? parentID = ViewBag.CurrentInfoType.Parent_ift;
                if (parentID == 0)
                {
                    var qi = new QueryInfo();
                    qi.Parameters.Add("Parent_ift", ViewBag.CurrentInfoType.ID_ift);
                    qi.Orderby.Add("Sort_ift", null);
                    var childList = _InfoType_iftServer.GetList(qi);
                    List<int?> childIDs = childList.Select(p => p.ID_ift).ToList();
                    queryInfo.Parameters.Add("InfoTypeIDs", childIDs);
                }
                else
                    queryInfo.Parameters.Add("InfoTypeIDs", new List<int?> { id });
            }
            queryInfo.Parameters.Add("rows", rows);
            queryInfo.Parameters.Add("page", page);
            queryInfo.Parameters.Add("Type_inf", (int)InfoType.Info);
            ViewBag.Infos = _Info_infServer.GetPageList(queryInfo);
            ViewBag.MetaTitle = ViewBag.CurrentInfoType == null ? "<=Info1>-" + ViewBag.Setting.Title_set : ViewBag.CurrentInfoType.Name_ift + "-<=Info1>-" + ViewBag.Setting.Title_set;
            ViewBag.MetaKeywords = ViewBag.CurrentInfoType == null ? "<=Info1>-" + ViewBag.Setting.Title_set : ViewBag.CurrentInfoType.Keywords_ift + "-<=Info1>-" + ViewBag.Setting.Title_set;
            ViewBag.MetaAuthor = ViewBag.Setting.Author_set;
            ViewBag.Title = ViewBag.CurrentInfoType == null ? "<=Info1>-" + ViewBag.Setting.Title_set : ViewBag.CurrentInfoType.Name_ift + "-<=Info1>-" + ViewBag.Setting.Title_set;
            return View();
        }
        public ActionResult Detail(int? id)
        {
            dynamic viewModel = new System.Dynamic.ExpandoObject();
            viewModel.Info = _Info_infServer.GetItem(id);
            viewModel.Info.Hits_inf += 1;
            _Info_infServer.Update(viewModel.Info);
            viewModel.InfoType = _InfoType_iftServer.GetItem(viewModel.Info.InfoTypeID_inf);
            ViewBag.MetaTitle = string.Format("{0}-{1}-{2}", viewModel.Info.Title_inf, viewModel.InfoType.Name_ift, ViewBag.Setting.Title_set);
            ViewBag.MetaKeywords = viewModel.Info.Keywords_inf;
            ViewBag.MetaAuthor = viewModel.Info.Author_inf;
            ViewBag.Title = string.Format("{0}-{1}-{2}", viewModel.Info.Title_inf, viewModel.InfoType.Name_ift, ViewBag.Setting.Title_set);
            return View(viewModel);
        }
    }
}

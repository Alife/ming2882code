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
    public class IndustryController : BaseController
    {
        [Dependency]
        public IInfo_inf _Info_infServer { get; set; }
        public ActionResult Index(int? id, int rows = 1, int page = 20)
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
            ViewBag.Infos = _Info_infServer.GetPageList(queryInfo);
            return View();
        }
    }
}

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
        public ActionResult Index(int? id, int rows = 15, int page = 1)
        {
            ViewBag.CurrentInfoType = _InfoType_iftServer.GetItem(id);
            QueryInfo queryInfo = new QueryInfo();
            if (id.HasValue)
            {
                int? parentID = ViewBag.CurrentInfoType.Parent_ift;
                if (parentID > 0)
                    queryInfo.Parameters.Add("InfoTypeID_inf", parentID);
                else
                    queryInfo.Parameters.Add("InfoTypeID_inf", id);
            }
            queryInfo.Parameters.Add("rows", rows);
            queryInfo.Parameters.Add("page", page);
            ViewBag.Infos = _Info_infServer.GetPageList(Funs.GetQueryInfo());
            return View();
        }
    }
}

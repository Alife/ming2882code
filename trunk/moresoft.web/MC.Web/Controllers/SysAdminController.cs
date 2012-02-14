using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using Microsoft.Practices.Unity;
using MC.Model;
using MC.IBLL;

namespace MC.Web.Controllers
{
    [Authorize]
    [HandleError]
    public class SysAdminController : BaseController
    {
        [Dependency]
        public IPage_pag _Page_pagServer { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        #region 页面信息管理
        public ActionResult Pages()
        {
            return View();
        }
        public JsonResult PagesList()
        {
            return Json(_Page_pagServer.GetPageList(Funs.GetQueryInfo()), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}

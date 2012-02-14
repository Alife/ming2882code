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
        #region UI
        public ActionResult Pages()
        {
            return View();
        }
        #endregion
        #region 页面信息列表
        public JsonResult PagesList()
        {
            return Json(LoadPagesChild(0), JsonRequestBehavior.AllowGet);
        }
        public JsonResult PagesTree(int? id)
        {
            var tree = LoadPagesTree(0);
            if (id.HasValue)
                tree.Insert(0, new TreeEntity { id = 0, text = "请选择" });
            return Json(tree, JsonRequestBehavior.AllowGet);
        }
        private IList<Page_pag> LoadPagesChild(int parentID)
        {
            QueryInfo info = new QueryInfo();
            info.Parameters.Add("Parent_pag", parentID);
            info.Orderby.Add("Sort_pag", null);
            var list = _Page_pagServer.GetList(info);
            foreach (var item in list)
                item.children = LoadPagesChild(item.ID_pag.Value);
            return list;
        }
        private List<TreeEntity> LoadPagesTree(int parentID)
        {
            List<TreeEntity> tree = new List<TreeEntity>();
            QueryInfo info = new QueryInfo();
            info.Parameters.Add("Parent_pag", parentID);
            info.Orderby.Add("Sort_pag", null);
            var list = _Page_pagServer.GetList(info);
            foreach (var item in list)
                tree.Add(new TreeEntity { id = item.ID_pag.Value, text = item.Name_pag, children = LoadPagesTree(item.ID_pag.Value) });
            return tree;
        }
        #endregion
        #region 页面信息增加修改删除 
        public ActionResult PagesDetail(int id)
        {
            if (id > 0)
                return View(_Page_pagServer.GetItem(id));
            return View();
        }
        [ValidateInput(false)]
        public JsonResult SavePagesDetail(string a, Page_pag model)
        {
            int v = 0;
            if (model.Parent_pag > 0) model.Path_pag = _Page_pagServer.GetItem(model.Parent_pag.Value).Path_pag + 1;
            else { model.Parent_pag = 0; model.Path_pag = 1; }
            if (a == "add")
                v = _Page_pagServer.Insert(model);
            else
                v = _Page_pagServer.Update(model);
            if (v > 0)
                return Json(new { success = true, msg = "保存成功" }, JsonRequestBehavior.AllowGet);
            return Json(new { success = false, msg = "保存失败" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PagesDelete(string id)
        {
            if (_Page_pagServer.Delete(id.Split(',').ToList()) > 0)
                return Json(new { success = true, msg = "删除成功" }, JsonRequestBehavior.AllowGet);
            return Json(new { success = false, msg = "删除失败" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
    }
}

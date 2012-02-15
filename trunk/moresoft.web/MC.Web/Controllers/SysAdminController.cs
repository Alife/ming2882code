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
    [Authorize(Users = "admin")]
    [HandleError]
    public class SysAdminController : BaseController
    {
        #region properties
        [Dependency]
        public IPage_pag _Page_pagServer { get; set; }
        [Dependency]
        public IKeywords_key _Keywords_keyServer { get; set; }
        [Dependency]
        public ILink_lnk _Link_lnkServer { get; set; }
        [Dependency]
        public IUser_usr _User_usrServer { get; set; }
        #endregion
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
                return Json(new { success = true, msg = "保存成功" }, "text/plain");
            return Json(new { success = false, msg = "保存失败" }, "text/plain");
        }
        public JsonResult PagesDelete(string id)
        {
            if (_Page_pagServer.Delete(id.Split(',').ToList()) > 0)
                return Json(new { success = true, msg = "删除成功" }, "text/plain");
            return Json(new { success = false, msg = "删除失败" }, "text/plain");
        }
        #endregion
        #endregion
        #region 关键字管理
        #region UI
        public ActionResult Keywords()
        {
            return View();
        }
        #endregion
        #region 关键字管理列表
        public JsonResult KeywordsList()
        {
            return Json(_Keywords_keyServer.GetPageList(Funs.GetQueryInfo()), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 关键字管理增加修改删除
        public JsonResult KeywordsSave(string a, Keywords_key model)
        {
            int v = 0;
            if (a == "add")
                v = _Keywords_keyServer.Insert(model);
            else
                v = _Keywords_keyServer.Update(model);
            if (v > 0)
                return Json(new { success = true, msg = "保存成功" }, "text/plain");
            return Json(new { success = false, msg = "保存失败" }, "text/plain");
        }
        public JsonResult KeywordsDelete(string id)
        {
            if (_Keywords_keyServer.Delete(id.Split(',').ToList()) > 0)
                return Json(new { success = true, msg = "删除成功" }, "text/plain");
            return Json(new { success = false, msg = "删除失败" }, "text/plain");
        }
        #endregion
        #endregion
        #region 网站配置
        public ActionResult Settings(int id)
        {
            return View(ViewBag.Setting);
        }
        public JsonResult SettingsSave(string a, Setting_set model)
        {
            int v = 0;
            if (a == "add")
                v = _Setting_setServer.Insert(model);
            else
                v = _Setting_setServer.Update(model);
            if (v > 0)
                return Json(new { success = true, msg = "保存成功" }, "text/plain");
            return Json(new { success = false, msg = "保存失败" }, "text/plain");
        }
        #endregion
        #region 友情连接管理
        #region UI
        public ActionResult Links()
        {
            return View();
        }
        #endregion
        #region 友情连接列表
        public JsonResult LinksList()
        {
            return Json(_Link_lnkServer.GetPageList(Funs.GetQueryInfo()), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 友情连接增加修改删除
        public JsonResult LinksSave(string a, Link_lnk model)
        {
            int v = 0;
            model.IsHide_lnk = model.IsHide_lnk ?? false;
            if (a == "add")
                v = _Link_lnkServer.Insert(model);
            else
                v = _Link_lnkServer.Update(model);
            if (v > 0)
                return Json(new { success = true, msg = "保存成功" }, "text/plain");
            return Json(new { success = false, msg = "保存失败" }, "text/plain");
        }
        public JsonResult LinksDelete(string id)
        {
            if (_Link_lnkServer.Delete(id.Split(',').ToList()) > 0)
                return Json(new { success = true, msg = "删除成功" }, "text/plain");
            return Json(new { success = false, msg = "删除失败" }, "text/plain");
        }
        #endregion
        #endregion
        #region 用户管理
        #region UI
        public ActionResult Users()
        {
            return View();
        }
        #endregion
        #region 用户列表
        public JsonResult UsersList()
        {
            return Json(_User_usrServer.GetPageList(Funs.GetQueryInfo()), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 用户增加修改删除
        public JsonResult UsersSave(string a, User_usr model, string UserName_usr_Old)
        {
            if (string.IsNullOrEmpty(UserName_usr_Old) || UserName_usr_Old != model.UserName_usr)
            {
                if (_User_usrServer.IsUserExists(model.UserName_usr))
                    return Json(new { success = false, msg = "保存失败，已经有相同的用户名" }, "text/plain");
            }
            int v = 0;
            model.Password_usr = !string.IsNullOrEmpty(model.Password_usr) ? Unity.Mvc3.Helpers.Encoders.MD5.Encode(model.Password_usr) : null;
            if (a == "add")
                v = _User_usrServer.Insert(model);
            else
                v = _User_usrServer.Update(model);
            if (v > 0)
                return Json(new { success = true, msg = "保存成功" }, "text/plain");
            return Json(new { success = false, msg = "保存失败" }, "text/plain");
        }
        public JsonResult UsersDelete(string id)
        {
            if (_User_usrServer.Delete(id.Split(',').ToList()) > 0)
                return Json(new { success = true, msg = "删除成功" }, "text/plain");
            return Json(new { success = false, msg = "删除失败" }, "text/plain");
        }
        #endregion
        #endregion
    }
}

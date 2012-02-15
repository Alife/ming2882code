using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
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
                item.children = item.IsHasChild_pag.Value ? LoadPagesChild(item.ID_pag.Value) : null;
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
                tree.Add(new TreeEntity
                {
                    id = item.ID_pag.Value,
                    text = item.Name_pag,
                    children = item.IsHasChild_pag.Value ? LoadPagesTree(item.ID_pag.Value) : null
                });
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
            {
                model.IsHasChild_pag = false;
                v = _Page_pagServer.Insert(model);
            }
            else
            {
                var qi = new QueryInfo();
                qi.Parameters.Add("Parent_pag", model.ID_pag);
                model.IsHasChild_pag = _Page_pagServer.GetList(qi).Count > 0;
                v = _Page_pagServer.Update(model);
            }
            if (v > 0)
                return Json(new { success = true, msg = "保存成功" }, "text/plain");
            return Json(new { success = false, msg = "保存失败" }, "text/plain");
        }
        public JsonResult PagesDelete(string id)
        {
            if (_Page_pagServer.Delete(id.Split(',').ToList()) > 0)
                return Json(new { success = true, msg = "删除成功" }, JsonRequestBehavior.AllowGet);
            return Json(new { success = false, msg = "删除失败" }, JsonRequestBehavior.AllowGet);
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
                return Json(new { success = true, msg = "删除成功" }, JsonRequestBehavior.AllowGet);
            return Json(new { success = false, msg = "删除失败" }, JsonRequestBehavior.AllowGet);
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
                return Json(new { success = true, msg = "删除成功" }, JsonRequestBehavior.AllowGet);
            return Json(new { success = false, msg = "删除失败" }, JsonRequestBehavior.AllowGet);
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
                return Json(new { success = true, msg = "删除成功" }, JsonRequestBehavior.AllowGet);
            return Json(new { success = false, msg = "删除失败" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 日志
        #region UI
        public ActionResult Logs()
        {
            return View();
        }
        #endregion
        #region 日志列表
        public JsonResult LogsList()
        {
            string[] arrFiles = Directory.GetFiles(Server.MapPath("/log/"), "*.log", SearchOption.AllDirectories);
            List<object> files = new List<object>();
            foreach (string item in arrFiles)
            {
                FileInfo fileinfo = new FileInfo(item);
                files.Add(new { FileName = fileinfo.Name, FileSize = fileinfo.Length });
            }
            return Json(files, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 日志明细
        public ActionResult LogsDetail(string id)
        {
            string path = Server.MapPath("/log/" + id);
            StringBuilder content = new StringBuilder();
            FileStream fs = null; StreamReader sr = null;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));
                //byte[] data = new byte[fs.Length];
                //fs.Read(data, 0, data.Length);
                sr.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
                string s1;
                while ((s1 = sr.ReadLine()) != null)
                    content.AppendLine("<tr bgcolor=\"#f9fbf0\"><td style=\"height: 24px\">" + s1 + "</td></tr>");
            }
            finally
            {
                fs.Close();
                sr.Close();
            }
            ViewBag.LogContent = content.ToString();
            return View();
        }
        #endregion
        #region 删除日志
        public JsonResult LogsDelete(string id)
        {
            string[] ids = id.Split(',');
            int delCount = 0;
            foreach (string fileName in ids)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(Server.MapPath("/log/" + fileName));
                    fileInfo.Delete();
                    delCount += 1;
                }
                catch
                {
                }
            }
            if (ids.Length == delCount)
                return Json(new { success = true, msg = "删除成功" }, JsonRequestBehavior.AllowGet);
            return Json(new { success = false, msg = "删除失败,今日日志正在使用中无法删除" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 行业分类管理
        #region UI
        public ActionResult InfoTypes()
        {
            return View();
        }
        #endregion
        #region 行业分类列表
        public JsonResult InfoTypesList()
        {
            return Json(LoadInfoTypesChild(0), JsonRequestBehavior.AllowGet);
        }
        public JsonResult InfoTypesTree(int? id)
        {
            var tree = LoadInfoTypesTree(0);
            if (id.HasValue)
                tree.Insert(0, new TreeEntity { id = 0, text = "请选择" });
            return Json(tree, JsonRequestBehavior.AllowGet);
        }
        private List<TreeEntity> LoadInfoTypesTree(int parentID)
        {
            List<TreeEntity> tree = new List<TreeEntity>();
            QueryInfo info = new QueryInfo();
            info.Parameters.Add("Parent_ift", parentID);
            info.Orderby.Add("Sort_ift", null);
            var list = _InfoType_iftServer.GetList(info);
            foreach (var item in list)
                tree.Add(new TreeEntity
                {
                    id = item.ID_ift.Value,
                    text = item.Name_ift,
                    children = item.IsHasChild_ift.Value ? LoadInfoTypesTree(item.ID_ift.Value) : null
                });
            return tree;
        }
        #endregion
        #region 行业分类增加修改删除
        public ActionResult InfoTypesDetail(int id)
        {
            if (id > 0)
                return View(_InfoType_iftServer.GetItem(id));
            return View();
        }
        [ValidateInput(false)]
        public JsonResult InfoTypesSave(string a, InfoType_ift model)
        {
            int v = 0;
            if (model.Parent_ift > 0) model.Path_ift = _InfoType_iftServer.GetItem(model.Parent_ift.Value).Path_ift + 1;
            else { model.Parent_ift = 0; model.Path_ift = 1; }
            if (a == "add")
            {
                model.IsHasChild_ift = false;
                v = _InfoType_iftServer.Insert(model);
            }
            else
            {
                var qi = new QueryInfo();
                qi.Parameters.Add("Parent_ift", model.ID_ift);
                model.IsHasChild_ift = _InfoType_iftServer.GetList(qi).Count > 0;
                v = _InfoType_iftServer.Update(model);
            }
            if (v > 0)
                return Json(new { success = true, msg = "保存成功" }, "text/plain");
            return Json(new { success = false, msg = "保存失败" }, "text/plain");
        }
        public JsonResult InfoTypesDelete(int id)
        {
            if (_InfoType_iftServer.Delete(id) > 0)
                return Json(new { success = true, msg = "删除成功" }, JsonRequestBehavior.AllowGet);
            return Json(new { success = false, msg = "删除失败，分类下有子类无法删除" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
    }
}

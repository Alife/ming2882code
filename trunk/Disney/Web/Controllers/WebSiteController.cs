using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Enums;
using BLL;
using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data;
using System.IO;

namespace Web.Controllers
{
    public class WebSiteController : Controller
    {
        #region 网站导航分类
        #region 查询全部
        [AdminAuthorize("phototype", "select")]
        public JsonResult phototype()
        {
            var list = web_PhotoTypeBLL.GetList();
            ArrayList al = new ArrayList();
            foreach (web_PhotoType item in list)
                al.Add(new { ID = item.ID, Name = item.ParentID == 0 ? item.Name : "　" + item.Name, Code = item.Code, ParentID = item.ParentID, OrderID = item.OrderID });
            return Json(al, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 查询一条
        [AdminAuthorize("phototype", "select")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult getphototype(int? id)
        {
            web_PhotoType item = null;
            if (id.HasValue) item = web_PhotoTypeBLL.GetItem(id.Value);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 增加
        [AdminAuthorize("phototype", "add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult phototypeadd()
        {
            web_PhotoType item = new web_PhotoType();
            TryUpdateModel(item, Request.Form.AllKeys);
            item.Name = item.Name.Trim().Replace("　", "");
            if (web_PhotoTypeBLL.Insert(item) > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("phototype", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult phototypeedit(int id)
        {
            web_PhotoType item = new web_PhotoType();
            TryUpdateModel(item, Request.Form.AllKeys);
            item.Name = item.Name.Trim().Replace("　", "");
            if (web_PhotoTypeBLL.Update(item) > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("phototype", "del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult phototypedelete()
        {
            List<string> ids = Request["id"].Split(',').ToList();
            if (web_PhotoTypeBLL.Delete(ids) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
    }
}

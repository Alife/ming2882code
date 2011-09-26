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
        #region 删除
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
        #region 图片明细管理
        public JsonResult Uploads(int photoType)
        {
            string saveFileName = string.Empty;
            HttpPostedFileBase fileUpload = Request.Files[0];
            if (fileUpload != null && fileUpload.ContentLength > 0 && FileExtension.IsImages(fileUpload.InputStream))
                saveFileName = string.Format("{0:yyyyMMddmmhhssffff}{1}", DateTime.Now, System.IO.Path.GetExtension(fileUpload.FileName));
            if (saveFileName != string.Empty)
            {
                Stream stream = (Stream)fileUpload.InputStream;
                stream.Position = 0;
                byte[] buffer = new byte[stream.Length + 1];
                stream.Read(buffer, 0, buffer.Length);  
                DpUploads.Uploads uploads = new DpUploads.Uploads();
                if (uploads.UploadFile(buffer, fileUpload.FileName.Split('.')[0], saveFileName, photoType))
                {
                    stream.Close();
                    stream.Dispose();
                    return Json(new MessageBox(true, "上传成功"), "text/html");
                }
            }
            return Json(new MessageBox(false, "上传失败"), "text/html");
        }
        [AdminAuthorize("phototype", "select")]
        public JsonResult photodetail(int id)
        {
            return Json(web_PhotoBLL.GetList(id), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("phototype", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult photodetailedit(int id)
        {
            web_Photo item = web_PhotoBLL.GetItem(id);
            TryUpdateModel(item, Request.Form.AllKeys);
            item.Name = item.Name.Trim().Replace("　", "");
            if (web_PhotoBLL.Update(item) > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("phototype", "del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult photodetaildelete()
        {
            List<string> ids = Request["id"].Split(',').ToList();
            if (web_PhotoBLL.Delete(ids) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}

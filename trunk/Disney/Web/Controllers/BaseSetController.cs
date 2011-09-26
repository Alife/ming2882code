using System;
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
    public class BaseSetController : BaseController
    {
        #region 部门机构管理
        #region 查询全部
        [AdminAuthorize("department", "select")]
        public JsonResult dept(int? type)
        {
            int id = 0;
            List<Department> list = new List<Department>();
            var depts = sys_DataPermissionBLL.GetLists(UserBase.ID, ResourceType.Dept);
            if (depts.Count > 0)
            {
                foreach (var dept in depts)
                {
                    if (dept != null && dept.ResourceID.HasValue) id = dept.ResourceID.Value;
                    var temp = d_DepartmentBLL.GetList(id, type ?? 0);
                    int path = 0;
                    if (temp.Count > 0 && dept != null && dept.ResourceID.HasValue)
                        path = temp.FirstOrDefault(p => p.ID == id).Path - 1;
                    foreach (var item in temp)
                    {
                        Department t = new Department();
                        t.ID = item.ID;
                        t.Code = item.Code;
                        int tmppath = 0;
                        if (path > 0) tmppath = item.Path - path; else tmppath = item.Path;
                        t.Name = Funs.GetCategroyPath(tmppath, "　") + item.Name;
                        t.ParentID = item.ParentID;
                        t.Lft = item.Lft;
                        t.Rgt = item.Rgt;
                        t.IsLeaf = item.IsLeaf;
                        t.Path = item.Path;
                        t.Children = item.Children;
                        list.Add(t);
                    }
                }
            }
            else
            {
                var temp = d_DepartmentBLL.GetList(0, type ?? 0);
                foreach (var item in temp)
                {
                    Department t = new Department();
                    t.ID = item.ID;
                    t.Code = item.Code;
                    t.Name = Funs.GetCategroyPath(item.Path, "　") + item.Name;
                    t.ParentID = item.ParentID;
                    t.Lft = item.Lft;
                    t.Rgt = item.Rgt;
                    t.IsLeaf = item.IsLeaf;
                    t.Path = item.Path;
                    t.Children = item.Children;
                    list.Add(t);
                }
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("department", "select")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult depts()
        {
            int id = 0;
            var dept = sys_DataPermissionBLL.GetItem(UserBase.ID, ResourceType.Dept);
            if (dept != null && dept.ResourceID.HasValue) id = dept.ResourceID.Value;
            return Json(d_DepartmentBLL.GetList(id, 0), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 查询一条
        [AdminAuthorize("department", "select")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult getdept(int id)
        {
            return Json(d_DepartmentBLL.GetItem(id), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 增加
        [AdminAuthorize("department", "add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult deptadd()
        {
            d_Department item = new d_Department();
            TryUpdateModel(item, Request.Form.AllKeys);
            var user = UserBase;
            if (user.ID != 1 && item.ParentID == 0)
                item.ParentID = user.DepartmentID ?? 0;
            if (d_DepartmentBLL.Insert(item) > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("department", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult deptedit(int id, int parentid)
        {
            d_Department item = d_DepartmentBLL.GetItem(id);
            if (item.ID == parentid)
                return Json(new MessageBox(false, "父类不能是自身"), JsonRequestBehavior.AllowGet);
            TryUpdateModel(item, Request.Form.AllKeys);
            if (d_DepartmentBLL.Update(item) > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 删除
        [AdminAuthorize("department", "del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult deptdelete()
        {
            List<string> ids = Request["id"].Split(',').ToList();
            if (d_DepartmentBLL.Delete(ids) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 用户类型
        #region 查询全部
        [AdminAuthorize("usertype", "select")]
        public ContentResult usertype(string id)
        {
            GridJSONHelper jsonObject = new GridJSONHelper();
            jsonObject.success = true;

            var userType = t_UserTypeBLL.GetList(id);
            var dataPer = sys_DataPermissionBLL.GetItem(UserBase.ID, ResourceType.Dept);
            List<d_Department> depts = new List<d_Department>();
            if (dataPer != null && dataPer.ResourceID.HasValue)
                depts = d_DepartmentBLL.GetList(dataPer.ResourceID.Value, 1);
            foreach (var item in userType)
            {
                var role = sys_RoleBLL.GetItem(item.RoleID);
                var dataPerDept = sys_DataPermissionBLL.GetList(item.RoleID).FirstOrDefault(p => p.ResourceType == (int)ResourceType.Dept);
                if (dataPer != null && dataPerDept != null)
                {
                    if (depts.FirstOrDefault(p => p.ID == dataPerDept.ResourceID) != null)
                    {
                        jsonObject.AddItem("ID", item.ID);
                        jsonObject.AddItem("Name", item.Name);
                        jsonObject.AddItem("Type", item.Type);
                        jsonObject.AddItem("TypeName", GetEnumBLL.GetEnumDescription(Enum.Parse(typeof(UserType), item.Type.ToString())));
                        jsonObject.AddItem("RoleID", item.RoleID);
                        jsonObject.AddItem("RoleName", role.RoleName);
                        jsonObject.ItemOk();
                    }
                }
                else
                {
                    jsonObject.AddItem("ID", item.ID);
                    jsonObject.AddItem("Name", item.Name);
                    jsonObject.AddItem("Type", item.Type);
                    jsonObject.AddItem("TypeName", GetEnumBLL.GetEnumDescription(Enum.Parse(typeof(UserType), item.Type.ToString())));
                    jsonObject.AddItem("RoleID", item.RoleID);
                    jsonObject.AddItem("RoleName", role.RoleName);
                    jsonObject.ItemOk();
                }
            }
            return Content(jsonObject.ToString());
            //return Json(t_UserTypeBLL.GetList(id ?? 0), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 查询一条
        [AdminAuthorize("usertype", "select")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult getusertype(int? id)
        {
            t_UserType item = null;
            if (id.HasValue) item = t_UserTypeBLL.GetItem(id.Value);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 增加
        [AdminAuthorize("usertype", "add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult usertypeadd()
        {
            t_UserType item = new t_UserType();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (t_UserTypeBLL.Insert(item) > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("usertype", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult usertypeedit(int id)
        {
            t_UserType item = new t_UserType();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (t_UserTypeBLL.Update(item) > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("usertype", "del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult usertypedelete()
        {
            List<string> ids = Request["id"].Split(',').ToList();
            if (t_UserTypeBLL.Delete(ids) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 封面尺寸
        #region 查询全部
        [AdminAuthorize("covertype", "select")]
        public JsonResult covertype()
        {
            return Json(d_CoverTypeBLL.GetList(), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 查询一条
        [AdminAuthorize("covertype", "select")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult getcovertype(int? id)
        {
            d_CoverType item = null;
            if (id.HasValue) item = d_CoverTypeBLL.GetItem(id.Value);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 增加
        [AdminAuthorize("covertype", "add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult covertypeadd()
        {
            d_CoverType item = new d_CoverType();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (d_CoverTypeBLL.Insert(item) > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("covertype", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult covertypeedit(int id)
        {
            d_CoverType item = new d_CoverType();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (d_CoverTypeBLL.Update(item) > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("covertype", "del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult covertypedelete()
        {
            List<string> ids = Request["id"].Split(',').ToList();
            if (d_CoverTypeBLL.Delete(ids) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 套图模板
        #region 查询全部
        [AdminAuthorize("kittemplate", "select")]
        public JsonResult kittemplate()
        {
            return Json(d_KitTemplateBLL.GetList(), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 查询一条
        [AdminAuthorize("kittemplate", "select")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult getkittemplate(int? id)
        {
            d_KitTemplate item = null;
            if (id.HasValue) item = d_KitTemplateBLL.GetItem(id.Value);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 增加
        [AdminAuthorize("kittemplate", "add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kittemplateadd()
        {
            d_KitTemplate item = new d_KitTemplate();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (d_KitTemplateBLL.Insert(item) > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("kittemplate", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kittemplateedit(int id)
        {
            d_KitTemplate item = new d_KitTemplate();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (d_KitTemplateBLL.Update(item) > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("kittemplate", "del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kittemplatedelete()
        {
            List<string> ids = Request["id"].Split(',').ToList();
            if (d_KitTemplateBLL.Delete(ids) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 内页尺寸
        #region 查询全部
        [AdminAuthorize("insidetype", "select")]
        public JsonResult insidetype()
        {
            return Json(d_InsideTypeBLL.GetList(), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 查询一条
        [AdminAuthorize("insidetype", "select")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult getinsidetype(int? id)
        {
            d_InsideType item = null;
            if (id.HasValue) item = d_InsideTypeBLL.GetItem(id.Value);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 增加
        [AdminAuthorize("insidetype", "add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult insidetypeadd()
        {
            d_InsideType item = new d_InsideType();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (d_InsideTypeBLL.Insert(item) > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("insidetype", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult insidetypeedit(int id)
        {
            d_InsideType item = new d_InsideType();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (d_InsideTypeBLL.Update(item) > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("insidetype", "del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult insidetypedelete()
        {
            List<string> ids = Request["id"].Split(',').ToList();
            if (d_InsideTypeBLL.Delete(ids) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 同学录套图
        #region 查询全部
        [AdminAuthorize("classtype", "select")]
        public JsonResult classtype()
        {
            return Json(d_ClassTypeBLL.GetList(), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 查询一条
        [AdminAuthorize("classtype", "select")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult getclasstype(int? id)
        {
            d_ClassType item = null;
            if (id.HasValue) item = d_ClassTypeBLL.GetItem(id.Value);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 增加
        [AdminAuthorize("classtype", "add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult classtypeadd()
        {
            d_ClassType item = new d_ClassType();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (d_ClassTypeBLL.Insert(item) > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("classtype", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult classtypeedit(int id)
        {
            d_ClassType item = new d_ClassType();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (d_ClassTypeBLL.Update(item) > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("classtype", "del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult classtypedelete()
        {
            List<string> ids = Request["id"].Split(',').ToList();
            if (d_ClassTypeBLL.Delete(ids) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 封面材料
        #region 查询全部
        [AdminAuthorize("insidematerial", "select")]
        public JsonResult insidematerial()
        {
            return Json(d_InsideMaterialBLL.GetList(), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 查询一条
        [AdminAuthorize("insidematerial", "select")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult getinsidematerial(int? id)
        {
            d_InsideMaterial item = null;
            if (id.HasValue) item = d_InsideMaterialBLL.GetItem(id.Value);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 增加
        [AdminAuthorize("insidematerial", "add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult insidematerialadd()
        {
            d_InsideMaterial item = new d_InsideMaterial();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (d_InsideMaterialBLL.Insert(item) > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("insidematerial", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult insidematerialedit(int id)
        {
            d_InsideMaterial item = new d_InsideMaterial();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (d_InsideMaterialBLL.Update(item) > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("insidematerial", "del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult insidematerialdelete()
        {
            List<string> ids = Request["id"].Split(',').ToList();
            if (d_InsideMaterialBLL.Delete(ids) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 服装管理
        #region 查询全部
        [AdminAuthorize("costume", "select")]
        public JsonResult costume()
        {
            return Json(d_CostumeBLL.GetList(), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 查询一条
        [AdminAuthorize("costume", "select")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult getcostume(int? id)
        {
            d_Costume item = null;
            if (id.HasValue) item = d_CostumeBLL.GetItem(id.Value);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 增加
        [AdminAuthorize("costume", "add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult costumeadd()
        {
            d_Costume item = new d_Costume();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (d_CostumeBLL.Insert(item) > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("costume", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult costumeedit(int id)
        {
            d_Costume item = new d_Costume();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (d_CostumeBLL.Update(item) > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("costume", "del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult costumedelete()
        {
            List<string> ids = Request["id"].Split(',').ToList();
            if (d_CostumeBLL.Delete(ids) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 套系类型
        #region 查询全部
        [AdminAuthorize("kittype", "select")]
        public ContentResult kittype()
        {
            string json = JsonConvert.SerializeObject(d_KitTypeBLL.GetDataTable(), new DataTableConverter());
            return Content(json);
        }
        #endregion
        #region 查询全部
        [AdminAuthorize("kittype", "select")]
        public JsonResult getkittype()
        {
            return Json(d_KitTypeBLL.GetList(), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 增加
        [AdminAuthorize("kittype", "add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kittypeadd()
        {
            d_KitType item = new d_KitType();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (d_KitTypeBLL.Insert(item) > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("kittype", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kittypeedit(int id)
        {
            d_KitType item = new d_KitType();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (d_KitTypeBLL.Update(item) > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("kittype", "del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kittypedelete()
        {
            List<string> ids = Request["id"].Split(',').ToList();
            if (d_KitTypeBLL.Delete(ids) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 档图类型
        #region 查询全部
        [AdminAuthorize("kitphototype", "select")]
        public ContentResult kitphototype()
        {
            var list = d_KitPhotoTypeBLL.GetList();
            JsonSerializerSettings jsonSs = new JsonSerializerSettings();
            string json = JsonConvert.SerializeObject(list, Formatting.Indented, jsonSs);
            string[] arrJson = json.Split('}');
            for (int i = 0; i < arrJson.Length - 1; i++)
            {
                arrJson[i] += string.Format(",\"KitPhotoTypeName\":\"{0}\"",
                    GetEnumBLL.GetEnumDescription(Enum.Parse(typeof(KitPhotoType), list[i].Category.ToString()))) + " }";
            }
            json = String.Concat(arrJson);
            return Content(json);
        }
        [AdminAuthorize("kitphototype", "select")]
        public ContentResult kitphototypes()
        {
            var list = d_KitPhotoTypeBLL.GetList();
            GridJSONHelper jsonObject = new GridJSONHelper();
            jsonObject.success = true;
            foreach (var item in list)
            {
                jsonObject.AddItem("ID", item.ID);
                jsonObject.AddItem("Name", item.Name);
                jsonObject.AddItem("ArtPrice", item.ArtPrice);
                jsonObject.ItemOk();
            }
            return Content(jsonObject.ToString());
        }
        #endregion
        #region 增加
        [AdminAuthorize("kitphototype", "add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kitphototypeadd()
        {
            d_KitPhotoType item = new d_KitPhotoType();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (d_KitPhotoTypeBLL.Insert(item) > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("kitphototype", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kitphototypeedit(int id)
        {
            d_KitPhotoType item = new d_KitPhotoType();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (d_KitPhotoTypeBLL.Update(item) > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("kitphototype", "del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kitphototypedelete()
        {
            List<string> ids = Request["id"].Split(',').ToList();
            if (d_KitPhotoTypeBLL.Delete(ids) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 美工档图类型价格
        #region 查询全部
        [AdminAuthorize("artistprice", "select")]
        public ContentResult artistprice()
        {
            string json = JsonConvert.SerializeObject(d_ArtistPriceBLL.GetList(), Formatting.None, new DataTableConverter());
            return Content(json);
        }
        #endregion
        #region 查询
        [AdminAuthorize("artistprice", "select")]
        public ContentResult getartistprice(int id)
        {
            var list = d_KitPhotoTypeBLL.GetList();
            GridJSONHelper jsonObject = new GridJSONHelper();
            jsonObject.success = true;
            foreach (var item in list)
            {
                jsonObject.AddItem("ID", item.ID);
                jsonObject.AddItem("Name", item.Name);
                jsonObject.AddItem("Category", item.Category);
                var artprice = d_ArtistPriceBLL.GetItem(id, item.ID);
                jsonObject.AddItem("ArtPrice", artprice == null ? item.ArtPrice : artprice.Price);
                jsonObject.ItemOk();
            }
            return Content(jsonObject.ToString());
        }
        #endregion
        #region 增加
        [AdminAuthorize("artistprice", "add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult artistpriceadd()
        {
            d_ArtistPrice item = new d_ArtistPrice();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (d_ArtistPriceBLL.Insert(item) > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("artistprice", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult artistpriceedit(int id)
        {
            d_ArtistPrice item = new d_ArtistPrice();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (d_ArtistPriceBLL.Update(item) > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改
        [AdminAuthorize("artistprice", "del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult artistpricedelete()
        {
            List<string> ids = Request["id"].Split(',').ToList();
            if (d_ArtistPriceBLL.Delete(ids) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 枚举
        public ContentResult getKitPhotoType()
        {
            List<string[]> arrStr = GetEnumBLL.GetEnumOpt(typeof(KitPhotoType));
            GridJSONHelper jsonObject = new GridJSONHelper();
            jsonObject.success = true;
            foreach (string[] str in arrStr)
            {
                jsonObject.AddItem("displayText", str[0]);
                jsonObject.AddItem("displayValue", str[1]);
                jsonObject.ItemOk();
            }
            return Content(jsonObject.ToString());
        }
        public ContentResult education()
        {
            List<string[]> arrStr = GetEnumBLL.GetEnumOpt(typeof(Education));
            GridJSONHelper jsonObject = new GridJSONHelper();
            jsonObject.success = true;
            foreach (string[] str in arrStr)
            {
                jsonObject.AddItem("displayText", str[0]);
                jsonObject.AddItem("displayValue", str[1]);
                jsonObject.ItemOk();
            }
            return Content(jsonObject.ToString());
        }
        public ContentResult nation()
        {
            List<string[]> arrStr = GetEnumBLL.GetEnumOpt(typeof(Nation));
            GridJSONHelper jsonObject = new GridJSONHelper();
            jsonObject.success = true;
            foreach (string[] str in arrStr)
            {
                jsonObject.AddItem("displayText", str[0]);
                jsonObject.AddItem("displayValue", str[1]);
                jsonObject.ItemOk();
            }
            return Content(jsonObject.ToString());
        }
        public ContentResult PoliticsStatus()
        {
            List<string[]> arrStr = GetEnumBLL.GetEnumOpt(typeof(PoliticsStatus));
            GridJSONHelper jsonObject = new GridJSONHelper();
            jsonObject.success = true;
            foreach (string[] str in arrStr)
            {
                jsonObject.AddItem("displayText", str[0]);
                jsonObject.AddItem("displayValue", str[1]);
                jsonObject.ItemOk();
            }
            return Content(jsonObject.ToString());
        }
        public ContentResult usertypes()
        {
            List<string[]> arrStr = GetEnumBLL.GetEnumOpt(typeof(UserType));
            GridJSONHelper jsonObject = new GridJSONHelper();
            jsonObject.success = true;
            foreach (string[] str in arrStr)
            {
                jsonObject.AddItem("displayText", str[0]);
                jsonObject.AddItem("displayValue", str[1]);
                jsonObject.ItemOk();
            }
            return Content(jsonObject.ToString());
        }
        #endregion
    }
}

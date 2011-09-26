using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Models;
using Models.Enums;
using BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Web.Areas.Sys.Controllers
{
    public class HomeController : BaseController
    {
        #region 功能模块
        [AdminAuthorize("application", "select")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult loadapps(int? id, int? type)
        {
            return Json(sys_ApplicationBLL.GetList(id ?? 0, type ?? 0), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("application", "select")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ExistsApplication(string code, string oldcode)
        {
            JsonResult result = new JsonResult();
            if (!string.IsNullOrEmpty(oldcode) && code.ToLower() == oldcode.ToLower())
                result.Data = true;
            else
            {
                if (sys_ApplicationBLL.Exists(code) > 0)
                    result.Data = false;
                else
                    result.Data = true;
            }
            return result;
        }
        [AdminAuthorize("application", "add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApplicationAdd()
        {
            sys_Application item = new sys_Application();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (item.ParentID == 0)
                item.Path = 1;
            else
                item.Path = sys_ApplicationBLL.GetItem(item.ParentID).Path + 1;
            int revalue = sys_ApplicationBLL.Insert(item);
            if (revalue > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("application", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ApplicationEdit(int id)
        {
            sys_Application olditem = sys_ApplicationBLL.GetItem(id);
            sys_Application item = new sys_Application();
            TryUpdateModel(item, Request.Form.AllKeys);
            if (item.ID == item.ParentID)
                return Json(new MessageBox(false, "修改失败,父类节点不能是自身"), JsonRequestBehavior.AllowGet);
            if (item.ParentID == olditem.ParentID)
                item.ParentID = -1;
            int revalue = sys_ApplicationBLL.Update(item);
            if (revalue > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("application", "del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult ApplicationDelete(string id)
        {
            string[] arrid = id.TrimEnd(',').Split(',');
            if (sys_ApplicationBLL.Delete(arrid.ToList()) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult userapp(int id)
        {
            t_User user = t_UserBLL.BaseUser;
            if (user != null)
                return Json(sys_ApplicationBLL.GetUserList(user.ID, id), JsonRequestBehavior.AllowGet);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 功能操作
        [AdminAuthorize]
        public JsonResult Operation(int id)
        {
            return Json(sys_OperationBLL.GetList(id), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult useroperation(string appCode, string opCode)
        {
            int uid = UserBase.ID;
            var item = sys_OperationBLL.GetList(uid, appCode).FirstOrDefault(p => p.Code == opCode);
            if (item != null)
                return Json(new MessageBox(true, "有" + item.Operation + "权限"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "没有此权限"), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("application", "setop")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult OperationAdd(int appid)
        {
            sys_Operation item = new sys_Operation();
            TryUpdateModel(item, Request.Form.AllKeys);
            item.ApplicationID = appid;
            int revalue = sys_OperationBLL.Insert(item);
            if (revalue > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("application", "setop")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult OperationEdit(int id)
        {
            sys_Operation item = sys_OperationBLL.GetItem(id);
            TryUpdateModel(item, Request.Form.AllKeys);
            int revalue = sys_OperationBLL.Update(item);
            if (revalue > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("application", "setop")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperationDelete()
        {
            string[] arrid = Request.Form["id"].Split(',');
            if (sys_OperationBLL.Delete(arrid.ToList()) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("application", "setop")]
        public JsonResult OperationOrder(string id, string orderid)
        {
            string[] arrid = Request.Form["id"].Split(',');
            string[] arrorderid = Request.Form["orderid"].Split(',');
            if (sys_OperationBLL.Update(arrid.ToList(), arrorderid.ToList()) > 0)
                return new JsonResult { Data = new MessageBox(true, "更新成功"), ContentType = "application/json" };
            return new JsonResult { Data = new MessageBox(false, "更新失败"), ContentType = "application/json" };
        }
        #endregion
        #region 字段设置
        [AdminAuthorize]
        public JsonResult Field(int id)
        {
            return Json(sys_FieldBLL.GetList(id), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("application", "setop")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult FieldAdd(int opid)
        {
            sys_Field item = new sys_Field();
            TryUpdateModel(item, Request.Form.AllKeys);
            item.OperationID = opid;
            int revalue = sys_FieldBLL.Insert(item);
            if (revalue > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("application", "setop")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult FieldEdit(int id)
        {
            sys_Field item = sys_FieldBLL.GetItem(id);
            UpdateModel(item, Request.Form.AllKeys);
            item.ID = id;
            int revalue = sys_FieldBLL.Update(item);
            if (revalue > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("application", "setop")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult FieldDelete()
        {
            string[] arrid = Request.Form["id"].Split(',');
            if (sys_FieldBLL.Delete(arrid.ToList()) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 角色管理
        [AdminAuthorize("role", "select")]
        public JsonResult Role()
        {
            return Json(sys_RoleBLL.GetList(), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("role", "select")]
        public JsonResult UserRole(int id)
        {
            return Json(sys_UserRoleBLL.GetList(id), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("role", "add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult RoleAdd()
        {
            sys_Role item = new sys_Role();
            TryUpdateModel(item, Request.Form.AllKeys);
            int revalue = sys_RoleBLL.Insert(item);
            if (revalue > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("role", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult RoleEdit(int id)
        {
            sys_Role item = sys_RoleBLL.GetItem(id);
            TryUpdateModel(item, Request.Form.AllKeys);
            int revalue = sys_RoleBLL.Update(item);
            if (revalue > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("role", "del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult RoleDelete()
        {
            string[] arrid = Request.Form["id"].Split(',');
            if (sys_RoleBLL.Delete(arrid.ToList()) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("role", "setper")]
        public JsonResult Permission(int id)
        {
            return Json(sys_PermissionBLL.GetList(id, 0), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("role", "setper")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult PermissionSave(int id)
        {
            List<sys_Permission> pers = sys_PermissionBLL.GetList(id, 0);
            List<sys_Permission> list = new List<sys_Permission>();
            List<sys_Permission> temps = new List<sys_Permission>();
            string cbopitem = Request["cbopitem"];
            if (!string.IsNullOrEmpty(cbopitem))
            {
                foreach (string opitem in cbopitem.Split(','))
                {
                    var temp = pers.FirstOrDefault(p => p.OperationID == int.Parse(opitem));
                    if (temp == null)
                    {
                        sys_Permission per = new sys_Permission();
                        per.RoleID = id;
                        per.OperationID = int.Parse(opitem);
                        list.Add(per);
                    }
                    else
                        temps.Add(temp);
                }
            }
            foreach (var item in pers)
            {
                var temp = temps.FirstOrDefault(p => p.OperationID == item.OperationID);
                if (temp == null)
                    list.Add(item);
            }
            int revalue = sys_PermissionBLL.Save(list);
            if (revalue > 0 || revalue == -1)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("role", "setper")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult PermissionField(int opid, int rid)
        {
            string cbitem = Request["fields"];
            List<sys_Permission> pers = sys_PermissionBLL.GetList(rid, opid);
            if (pers.Count == 0)
                return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
            sys_Permission per = pers[0];
            List<sys_PermissionField> perfields = sys_PermissionFieldBLL.GetList(per.ID);
            List<sys_PermissionField> list = new List<sys_PermissionField>();
            List<sys_Field> fields = sys_FieldBLL.GetList(opid);
            foreach (sys_Field item in fields)
            {
                var temp = cbitem.Split(',').FirstOrDefault(p => p == item.ID.ToString());
                if (temp == null)
                {
                    sys_PermissionField perfield = new sys_PermissionField();
                    perfield.FieldID = item.ID;
                    perfield.PermissionID = per.ID;
                    list.Add(perfield);
                }
                var temp1 = perfields.FirstOrDefault(p => p.FieldID == item.ID);
                if (temp1 != null)
                    list.Add(temp1);
            }
            int revalue = sys_PermissionFieldBLL.Save(list);
            if (revalue > 0 || revalue == -1)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("role", "setper")]
        public JsonResult LoadPermissionField(int id)
        {
            return Json(sys_PermissionFieldBLL.GetList(id), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("alluser", "setrole")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UserRoleSave(int uid)
        {
            List<sys_UserRole> pers = sys_UserRoleBLL.GetList(uid);
            List<sys_UserRole> list = new List<sys_UserRole>();
            List<sys_UserRole> temps = new List<sys_UserRole>();
            string roleids = Request["roleids"];
            if (!string.IsNullOrEmpty(roleids))
            {
                foreach (string roleid in roleids.Split(','))
                {
                    var temp = pers.FirstOrDefault(p => p.RoleID == int.Parse(roleid));
                    if (temp == null)
                    {
                        sys_UserRole per = new sys_UserRole();
                        per.UserID = uid;
                        per.RoleID = int.Parse(roleid);
                        list.Add(per);
                    }
                    else
                        temps.Add(temp);
                }
            }
            foreach (var item in pers)
            {
                var temp = temps.FirstOrDefault(p => p.RoleID == item.RoleID);
                if (temp == null)
                    list.Add(item);
            }
            int revalue = sys_UserRoleBLL.Save(list);
            if (revalue > 0 || revalue == -1)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 地区管理
        [AdminAuthorize("arealist", "select")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult arealist()
        {
            List<sys_Area> old = sys_AreaBLL.GetList(0, 1);
            List<TreeArea> list = new List<TreeArea>();
            foreach (sys_Area item in old)
            {
                TreeArea t = new TreeArea();
                t.Children = item.Children;
                t.Code = item.Code;
                t.ID = item.ID;
                t.IsLeaf = item.IsLeaf;
                t.Lft = item.Lft;
                t.Name = Funs.GetCategroyPath(item.Path, "　") + item.Name;
                t.ParentID = item.ParentID;
                t.Path = item.Path;
                t.Pinyin = item.Pinyin;
                t.Rgt = item.Rgt;
                list.Add(t);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("member", "select")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult prearealist()
        {
            List<sys_Area> old = sys_AreaBLL.GetList(0, 5);
            List<TreeArea> list = new List<TreeArea>();
            foreach (sys_Area item in old)
            {
                TreeArea t = new TreeArea();
                t.Children = item.Children;
                t.Code = item.Code;
                t.ID = item.ID;
                t.IsLeaf = item.IsLeaf;
                t.Lft = item.Lft;
                t.Name = Funs.GetCategroyPath(item.Path, "　") + item.Name;
                t.ParentID = item.ParentID;
                t.Path = item.Path;
                t.Pinyin = item.Pinyin;
                t.Rgt = item.Rgt;
                list.Add(t);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 联动用
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [AdminAuthorize("arealist", "select")]
        public JsonResult areachildlist(int? id)
        {
            return Json(sys_AreaBLL.GetList(id ?? 0), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询parentid
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [AdminAuthorize("member", "select")]
        public JsonResult areaparentid(int? id)
        {
            sys_Area amodel = sys_AreaBLL.GetItem(id ?? 0);
            if (amodel != null)
            {
                return Json(new MessageBox(true,amodel.ParentID.ToString()), JsonRequestBehavior.AllowGet);
            }
            return Json(new MessageBox(false, "生成成功"), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询父类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AdminAuthorize("arealist", "select")]
        public JsonResult areaparentlist(int id, int type)
        {
            return Json(sys_AreaBLL.GetList(id, type), JsonRequestBehavior.AllowGet);
        }
        #region 生成树
        [AdminAuthorize("arealist", "select")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult arealists(int? anode, int? start, int? limit)
        {
            List<sys_Area> old = sys_AreaBLL.GetList(0, 1);
            anode = anode ?? 0;
            List<TreeArea> list = getAreaChildren(old, anode.Value);
            start = start ?? 0;
            limit = anode > 0 ? list.Count : limit;
            return Json(new TreeAreaList() { data = list.Skip(start.Value).Take(limit.Value).ToList(), records = getAreaChildren(old, 0).Count }, JsonRequestBehavior.AllowGet);
        }
        private List<TreeArea> getAreaChildren(List<sys_Area> temp, int pid)
        {
            List<TreeArea> list = new List<TreeArea>();
            List<sys_Area> tempone = temp.Where(p => p.ParentID == pid).ToList();
            foreach (sys_Area item in tempone)
            {
                TreeArea t = new TreeArea();
                t.Children = item.Children;
                t.Code = item.Code;
                t.ID = item.ID;
                t.IsLeaf = item.IsLeaf;
                t.Lft = item.Lft;
                t.Name = item.Name;
                t.ParentID = item.ParentID;
                t.Path = item.Path;
                t.Pinyin = item.Pinyin;
                t.Rgt = item.Rgt;
                list.Add(t);
            }
            return list;
        }
        //[AdminAuthorizeAttribute]
        //[AcceptVerbs(HttpVerbs.Post)]
        //public JsonResult arealists()
        //{
        //    List<TreeNodeItem> list = new List<TreeNodeItem>();
        //    List<sys_Area> temp = AreaBLL.GetList(0, 1);
        //    var tempone = temp.Where(p => p.ParentID == 0).ToList();
        //    foreach (sys_Area item in tempone)
        //        list.Add(new TreeNodeItem() { id = item.ID, text = item.Name, leaf = item.IsLeaf, children = GetAreaTree(temp, item.ID) });
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}
        //private List<TreeNodeItem> GetAreaTree(List<sys_Area> temp, int pid)
        //{
        //    List<TreeNodeItem> list = new List<TreeNodeItem>();
        //    var tempone = temp.Where(p => p.ParentID == pid).ToList();
        //    foreach (sys_Area item in tempone)
        //        list.Add(new TreeNodeItem() { id = item.ID, text = item.Name, leaf = item.IsLeaf, children = GetAreaTree(temp, item.ID) });
        //    return list;
        //}
        #endregion
        [AdminAuthorize("arealist", "add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AreaAdd()
        {
            sys_Area item = new sys_Area();
            TryUpdateModel(item, Request.Form.AllKeys);
            int revalue = sys_AreaBLL.Insert(item);
            if (revalue > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("arealist", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AreaEdit(int id, int? parentid, string name, string code, string pinyin)
        {
            int pid = parentid ?? 0;
            sys_Area item = sys_AreaBLL.GetItem(id);
            if (item.ID == parentid)
                return Json(new MessageBox(false, "修改失败,父类节点不能是自身"), JsonRequestBehavior.AllowGet);
            if (item.ParentID == pid)
                item.ParentID = -1;
            else
                item.ParentID = pid;
            item.Name = name;
            item.Code = code;
            item.Pinyin = pinyin;
            int revalue = sys_AreaBLL.Update(item);
            if (revalue > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("arealist", "del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult AreaDelete(string id)
        {
            string[] arrid = Request.Form["id"].Split(',');
            if (sys_AreaBLL.Delete(arrid.ToList()) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 系统日志
        [AdminAuthorize("syslog", "select")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult syslog(int start, int limit, int? categoryid, int? opid, string usercode, string objcode, DateTime? startDate, DateTime? endDate)
        {
            sys_LogList list = sys_LogBLL.GetList(start, limit, categoryid ?? 0, opid ?? 0, usercode, objcode, startDate, endDate);
            JsonNetResult jsonNetResult = new JsonNetResult();
            jsonNetResult.Formatting = Newtonsoft.Json.Formatting.Indented;
            jsonNetResult.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            jsonNetResult.Data = list;
            return jsonNetResult;
        }
        [AdminAuthorize("syslog", "select")]
        public JsonResult getlogcategory()
        {
            return Json(sys_LogCategoryBLL.GetList(), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("syslog", "select")]
        public JsonResult getlogop(int id)
        {
            return Json(sys_LogOpBLL.GetList(id), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 数据权限管理
        [AdminAuthorize("role", "select")]
        public ContentResult permissiondata(int id)
        {
            List<sys_DataPermission> list = sys_DataPermissionBLL.GetList(id);
            string json = JsonConvert.SerializeObject(list, Formatting.None);
            string[] arrJson = json.Split('}');
            for (int i = 0; i < arrJson.Length - 1; i++)
            {
                arrJson[i] += string.Format(",\"ConfineText\":\"{0}\"",
                    GetEnumBLL.GetEnumDescription(Enum.Parse(typeof(Confine), list[i].Confine.ToString())));
                if (list[i].ResourceID == 0)
                    arrJson[i] += string.Format(",\"ResourceText\":\"{0}\"", "");
                else
                {
                    if (list[i].Confine == (int)Confine.Company || list[i].Confine == (int)Confine.Dept)
                        arrJson[i] += string.Format(",\"ResourceText\":\"{0}\"", list[i].ResourceID.HasValue ? d_DepartmentBLL.GetItem(list[i].ResourceID.Value).Name : "");
                    else if (list[i].Confine == (int)Confine.Own)
                        arrJson[i] += string.Format(",\"ResourceText\":\"{0}\"", list[i].ResourceID.HasValue ? t_UserBLL.GetItem(list[i].ResourceID.Value).TrueName : "");
                }
                arrJson[i] += string.Format(",\"ResourceTypeText\":\"{0}\"",
                    GetEnumBLL.GetEnumDescription(Enum.Parse(typeof(ResourceType), list[i].ResourceType.ToString())))+ " }";
            }
            json = String.Concat(arrJson);
            return Content(json);
        }
        [AdminAuthorize("role", "add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult permissiondataadd()
        {
            sys_DataPermission item = new sys_DataPermission();
            TryUpdateModel(item, Request.Form.AllKeys);
            int revalue = sys_DataPermissionBLL.Insert(item);
            if (revalue > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("role", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult permissiondataedit(int id)
        {
            sys_DataPermission item = sys_DataPermissionBLL.GetItem(id);
            TryUpdateModel(item, Request.Form.AllKeys);
            int revalue = sys_DataPermissionBLL.Update(item);
            if (revalue > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("role", "del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult permissiondatadelete()
        {
            string[] arrid = Request.Form["id"].Split(',');
            if (sys_DataPermissionBLL.Delete(arrid.ToList()) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 枚举
        public ContentResult confine()
        {
            List<string[]> arrStr = GetEnumBLL.GetEnumOpt(typeof(Confine));
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
        public ContentResult resourceType()
        {
            List<string[]> arrStr = GetEnumBLL.GetEnumOpt(typeof(ResourceType));
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
        [AdminAuthorize("role", "setper")]
        public ActionResult loadpermission(int id)
        {
            ViewData["id"] = id;
            return View();
        }
    }
}

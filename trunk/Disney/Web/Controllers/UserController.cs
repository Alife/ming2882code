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
    public class UserController : BaseController
    {
        #region 登录/退出/加载/注销/修改密码/删除
        #region 登录
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult login(string loginId, string password, bool? rememberMe, string validateCode)
        {
            if (string.IsNullOrEmpty(validateCode) || Session["CheckCode"] == null || string.Compare(Session["CheckCode"].ToString(), validateCode.Trim().ToLower(), true) != 0)
                return Json(new MessageBox(false, "验证码错误，请重新填写"), JsonRequestBehavior.AllowGet);
            else Session["CheckCode"] = string.Empty;
            t_User item = null;
            password = DESEncrypt.MD5Encrypt(password);
            item = t_UserBLL.GetUserLogin(loginId, 2, password);
            if (item != null && !item.IsClose && item.TypeID.HasValue)
            {
                string json = SerializeDeserialize.SerializeObject(item);
                int expires = 0;
                if (rememberMe ?? false)
                    expires = 1440 * 365 * 10;
                HttpCookie cookie = Request.Cookies["user"];
                if (cookie == null)
                    cookie = new HttpCookie("user");
                cookie.Value = DESEncrypt.Encrypt(json);
                if (expires != 0)
                    cookie.Expires = DateTime.Now.AddMinutes(expires);
                Response.Cookies.Add(cookie);
                return Json(new MessageBox(true, "登录成功"), JsonRequestBehavior.AllowGet);
            }
            return Json(new MessageBox(false, "登录失败,用户不存在,请先注册"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 退出
        public ContentResult logout()
        {
            Request.Cookies.Clear();
            HttpCookie cookie = new HttpCookie("user", string.Empty);
            cookie.Expires = DateTime.Now.AddMinutes(-1);
            Response.Cookies.Add(cookie);
            string json = JsonConvert.SerializeObject(new MessageBox(true, "退出登录"));
            return Content(json);
            //return Json(new MessageBox(true, "退出登录"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 加载用户信息
        [AdminAuthorize("personal", "browse")]
        public ContentResult getuser(int? id)
        {
            JsonSerializerSettings jsonSs = new JsonSerializerSettings();
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.DataTableConverter());
            t_User item = null;
            if (id.HasValue)
                item = t_UserBLL.GetItem(id.Value);
            else
                item = t_UserBLL.BaseUser;
            t_UserInfo userinfo = t_UserInfoBLL.GetItem(item.ID);
            string json = JsonConvert.SerializeObject(item, Formatting.None, jsonSs);
            json = json.TrimEnd('}');
            json += ",\"userinfo\":" + JsonConvert.SerializeObject(userinfo, Formatting.None, jsonSs) + " }";
            return Content(json);
        }
        [AdminAuthorize("personal", "browse")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ContentResult GetUserByCode(string id)
        {
            JsonSerializerSettings jsonSs = new JsonSerializerSettings();
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.DataTableConverter());
            t_User item = t_UserBLL.GetItem(id);
            if (item != null)
            {
                string json = JsonConvert.SerializeObject(item, Formatting.None, jsonSs);
                json = json.TrimEnd('}');
                t_UserInfo userinfo = t_UserInfoBLL.GetItem(item.ID);
                json += ",\"userinfo\":" + JsonConvert.SerializeObject(userinfo, Formatting.None, jsonSs) + " }";
                return Content(json);
            }
            return Content("");
        }
        [AdminAuthorize("personal", "browse")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetUserByMobile(string id)
        {
            return Json(t_UserBLL.GetByMobile(id), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("personal", "browse")]
        public JsonResult usersearch(string type, string query)
        {
            int deptID = 0;
            //var dept = sys_DataPermissionBLL.GetItem(UserBase.ID, ResourceType.Dept);
            //if (dept != null && dept.ResourceID.HasValue) deptID = dept.ResourceID.Value;
            return Json(t_UserBLL.GetList(type, query, deptID), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改密码
        [AdminAuthorize("userpwd", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult userpwd(string oldpassword, string password, string repassword)
        {
            if (password != repassword)
                return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
            oldpassword = DESEncrypt.MD5Encrypt(oldpassword);
            password = DESEncrypt.MD5Encrypt(password);
            if (t_UserBLL.UpdatePass(UserBase.ID, oldpassword, password) > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 注销
        [AdminAuthorize("member", "logout")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult userlogout(int id)
        {
            if (id == 1 || id == 2)
                return Json(new MessageBox(false, "不可以注销"), JsonRequestBehavior.AllowGet);
            t_User user = t_UserBLL.GetItem(id);
            user.IsClose = true;
            #region 日志
            //var makeuser = UserBase;
            //string logopcode = "logout" + user.Type.ToString().ToLower();
            //sys_LogOp logop = sys_LogOpBLL.GetItem(logopcode);
            //sys_LogBLL.Insert(new sys_Log
            //{
            //    IP = Request.UserHostAddress,
            //    LogTime = DateTime.Now,
            //    ObjCode = user.UserCode,
            //    OpID = logop.ID,
            //    UserID = makeuser.ID,
            //    Content = string.Format(logop.FormatLog, makeuser.TrueName, makeuser.UserCode, user.UserCode)
            //});
            #endregion
            if (t_UserBLL.Update(user) > 0)
                return Json(new MessageBox(true, "注销成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "注销失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 删除用户
        public JsonResult deleteUser()
        {
            string[] arrid = Request.Form["id"].Split(',');
            arrid = arrid.Where(p => p != "1" && p != "2").ToArray();
            var makeuser = UserBase;
            if (arrid.Length > 0)
            {
                #region 日志
                //foreach (string id in arrid)
                //{
                //    var user = t_UserBLL.GetItem(int.Parse(id));
                //    string logopcode = "del" + user.Type.ToString().ToLower();
                //    sys_LogOp logop = sys_LogOpBLL.GetItem(logopcode);
                //    sys_LogBLL.Insert(new sys_Log
                //    {
                //        IP = Request.UserHostAddress,
                //        LogTime = DateTime.Now,
                //        ObjCode = user.UserCode,
                //        OpID = logop.ID,
                //        UserID = makeuser.ID,
                //        Content = string.Format(logop.FormatLog, makeuser.TrueName, makeuser.UserCode, user.UserCode)
                //    });
                //}
                #endregion
                if (t_UserBLL.Delete(arrid.ToList()) > 0)
                    return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
                return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
            }
            return Json(new MessageBox(false, "安徽公司与上海分公司不可以删除"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 用户是否属于机构
        /// <summary>
        /// 用户是否属于机构
        /// </summary>
        /// <param name="usercode"></param>
        /// <param name="insID"></param>
        /// <returns></returns>
        [AdminAuthorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult isinsuser(string usercode, int depID)
        {
            t_User userModel = t_UserBLL.GetItem(usercode);
            if (userModel != null && userModel.DepartmentID == depID)
                return Json(new MessageBox(true, "是所属用户"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "是所属用户"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 用户-手机/编号是否存在
        public JsonResult existsusercode(string code, string oldcode)
        {
            JsonResult result = new JsonResult();
            if (!string.IsNullOrEmpty(oldcode) && code.ToLower() == oldcode.ToLower())
                result.Data = true;
            else
            {
                if (t_UserBLL.GetItem(code) != null)
                    result.Data = false;
                else
                    result.Data = true;
            }
            return result;
        }
        public JsonResult existsmobile(string code, string oldcode)
        {
            JsonResult result = new JsonResult();
            if (!string.IsNullOrEmpty(oldcode) && code.ToLower() == oldcode.ToLower())
                result.Data = true;
            else
            {
                if (t_UserBLL.IsMobileExists(code))
                    result.Data = false;
                else
                    result.Data = true;
            }
            return result;
        }
        public JsonResult existsusername(string code, string oldcode)
        {
            JsonResult result = new JsonResult();
            if (!string.IsNullOrEmpty(oldcode) && code.ToLower() == oldcode.ToLower())
                result.Data = true;
            else
            {
                if (t_UserBLL.IsUserNameExists(code))
                    result.Data = false;
                else
                    result.Data = true;
            }
            return result;
        }
        public JsonResult existsemail(string code, string oldcode)
        {
            JsonResult result = new JsonResult();
            if (!string.IsNullOrEmpty(oldcode) && code.ToLower() == oldcode.ToLower())
                result.Data = true;
            else
            {
                if (t_UserBLL.IsEmailExists(code))
                    result.Data = false;
                else
                    result.Data = true;
            }
            return result;
        }
        #endregion
        #region 用户列表
        [AdminAuthorize("member,arter,staff,jober", "select,select,select,select")]
        public ContentResult userlist(int start, int limit, string type, string keyword, int? provinceID,
            int? countryid, int? deptid, string mobile)
        {
            deptid = deptid.HasValue ? deptid : UserBase.DepartmentID ?? 0;
            return Content(getuserlist(start, limit, type, keyword, provinceID ?? 0, countryid ?? 0, deptid, mobile, 0));
        }
        #endregion
        #region 美工列表
        [AdminAuthorize("arter", "select")]
        public ContentResult arterlist(int start, int limit, string type, string keyword, int? provinceID,
            int? countryid, int? deptid, string mobile)
        {
            deptid = deptid.HasValue ? deptid : UserBase.DepartmentID ?? 0;
            return Content(getuserlist(start, limit, type, keyword, provinceID ?? 0, countryid ?? 0, deptid, mobile, -1));
        }
        #endregion
        #region 客戶列表
        [AdminAuthorize("custom", "select")]
        public ContentResult customlist(int start, int limit, string type, string keyword, int? provinceID,
            int? countryid, string mobile)
        {
            return Content(getuserlist(start, limit, type, keyword, provinceID ?? 0, countryid ?? 0, 0, mobile, -1));
        }
        #endregion
        #region 员工列表
        [AdminAuthorize("staff", "select")]
        public ContentResult stafflist(int start, int limit, string type, string keyword, int? provinceID,
            int? countryid, string mobile)
        {
            var item = sys_DataPermissionBLL.GetItem(UserBase.ID, ResourceType.CrankOrderManQuery);
            int deptid = 0;
            if (item != null)
                deptid = item.ResourceID.HasValue ? item.ResourceID.Value : UserBase.DepartmentID ?? 0;
            return Content(getuserlist(start, limit, type, keyword, provinceID ?? 0, countryid ?? 0, deptid, mobile, -1));
        }
        #endregion
        #region 用戶通用列表方法
        private string getuserlist(int start, int limit, string type, string keyword, int? provinceID,
            int? countryid, int? deptid, string mobile, int? close)
        {
            int records = 0;
            DataTable list = t_UserBLL.GetList(start, limit, ref records, type, keyword, provinceID ?? 0, countryid ?? 0, deptid ?? 0, mobile, close ?? -1);
            JsonSerializerSettings jsonSs = new JsonSerializerSettings();
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.DataTableConverter());
            string json = JsonConvert.SerializeObject(list, Formatting.None, jsonSs);
            MessageBox msg = new MessageBox();
            msg.data = json;
            msg.records = records;
            msg.success = true;
            json = JsonConvert.SerializeObject(msg, Formatting.None);
            json = json.Replace("\\", "");
            json = json.Replace("\"[", "[");
            json = json.Replace("]\"", "]");
            return json;
        }
        #endregion
        #region 增加用户
        [AdminAuthorize("member,arter,staff,custom,jober", "add,add,add,add,add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult useradd(int typeid, string password)
        {
            t_User user = new t_User();
            TryUpdateModel(user, Request.Form.AllKeys);
            t_UserType userTypeItem = t_UserTypeBLL.GetItem(typeid);
            int type = userTypeItem.Type;
            UserType userType = (UserType)type;
            if (userType == UserType.Custom)
            {
                var serialModel = sys_SerialNumberBLL.GetItem((int)SerialNember.Custom);
                user.UserCode = string.Format("c{0}", (serialModel.SerialNumber += 1).ToString().PadLeft(4, '0'));
                sys_SerialNumberBLL.Update(serialModel);
            }
            else if (userType == UserType.Artist || userType == UserType.Cameraman || userType == UserType.Admin || userType == UserType.Staff)
            {
                var serialModel = sys_SerialNumberBLL.GetItem((int)SerialNember.Staff);
                user.UserCode = string.Format("s{0}", (serialModel.SerialNumber += 1).ToString().PadLeft(4, '0'));
                sys_SerialNumberBLL.Update(serialModel);
            }
            user.RegTime = user.LoginTime = DateTime.Now;
            if (string.IsNullOrEmpty(password))
                user.Password = DESEncrypt.MD5Encrypt("123456");
            else
                user.Password = DESEncrypt.MD5Encrypt(password);
            if (string.IsNullOrEmpty(user.UserName)) user.UserName = user.UserCode;
            int revalue = t_UserBLL.Insert(user);
            #region 权限
            List<sys_UserRole> rolelist = new List<sys_UserRole>();
            rolelist.Add(new sys_UserRole { UserID = revalue, RoleID = userTypeItem.RoleID });
            sys_UserRoleBLL.Save(rolelist);
            #endregion
            t_UserInfo userinfo = new t_UserInfo();
            TryUpdateModel(userinfo, Request.Form.AllKeys);
            userinfo.UserID = revalue;
            int v = t_UserInfoBLL.Insert(userinfo);
            if (v > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改用户
        [AdminAuthorize("member,arter,staff,custom,jober,useredit", "edit,edit,edit,edit,edit,edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult useredit(int id, string password, bool? isMarry)
        {
            t_User user = t_UserBLL.GetItem(id);
            TryUpdateModel(user, Request.Form.AllKeys);
            if (!string.IsNullOrEmpty(password))
                user.Password = DESEncrypt.MD5Encrypt(password);
            if (string.IsNullOrEmpty(user.UserName)) user.UserName = user.UserCode;
            t_UserBLL.Update(user);
            t_UserInfo userinfo = t_UserInfoBLL.GetItem(user.ID);
            TryUpdateModel(userinfo, Request.Form.AllKeys);
            if (isMarry.HasValue) userinfo.IsMarry = isMarry.Value;
            else userinfo.IsMarry = false;
            int v = t_UserInfoBLL.Update(userinfo);
            if (v > 0)
            {
                #region 权限
                if (user.TypeID.HasValue)
                {
                    t_UserType userTypeItem = t_UserTypeBLL.GetItem(user.TypeID.Value);
                    List<sys_UserRole> pers = sys_UserRoleBLL.GetList(user.ID);
                    List<sys_UserRole> rolelist = new List<sys_UserRole>();
                    List<sys_UserRole> temps = new List<sys_UserRole>();
                    int roleid = userTypeItem.RoleID;
                    var temp = pers.FirstOrDefault(p => p.RoleID == roleid);
                    if (temp == null)
                        rolelist.Add(new sys_UserRole { UserID = user.ID, RoleID = roleid });
                    else
                        temps.Add(temp);
                    foreach (var peritem in pers)
                    {
                        if (temps.FirstOrDefault(p => p.RoleID == peritem.RoleID) == null)
                            rolelist.Add(peritem);
                    }
                    sys_UserRoleBLL.Save(rolelist);
                }
                #endregion
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            }
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Security.Cryptography;
using MC.Web.Models;
using MC.Model;
using MC.IBLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MC.Web.Controllers
{
    public class AccountController : BaseController
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewBag.MetaTitle = ViewBag.Setting.Title_set;
            ViewBag.MetaKeywords = ViewBag.Setting.Keywords_set;
            ViewBag.MetaAuthor = ViewBag.Setting.Author_set;
            base.OnActionExecuted(filterContext);
        }
        private readonly IUser_usr _User_usrService;
        public AccountController(IUser_usr User_usrService)
        {
            _User_usrService = User_usrService;
        }
        #region 设置语言
        public JsonResult SetLang(string id)
        {
            Response.Cookies["lang"].Value = id;
            Response.Cookies["lang"].Expires = DateTime.Now.AddDays(365);
            return Json(new { msg = "" });
        }
        #endregion
        #region 用户登录
        public ActionResult LogOn()
        {
            return View();
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var userInfo = _User_usrService.GetUserLogin(model.UserName, Unity.Mvc3.Helpers.Encoders.MD5.Encode(model.Password), Request.UserHostAddress);
                if (userInfo != null)
                {
                    SetCookie(userInfo, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        return Redirect(returnUrl);
                    return Redirect("/");
                }
                else
                    ModelState.AddModelError("", "<=LoginErrorMessage>");
            }
            return View(model);
        }
        #endregion
        #region 写入用户Cookie
        private void SetCookie(User_usr userInfo, bool rememberMe)
        {
            FormsAuthentication.SetAuthCookie(userInfo.UserName, rememberMe);
            //bool isPersistent = rememberMe;
            //int expires = rememberMe ? 24 * 60 * 14 : 30;
            //JsonSerializerSettings jsonSs = new JsonSerializerSettings();
            //jsonSs.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            //string userData = JsonConvert.SerializeObject(userInfo, Newtonsoft.Json.Formatting.None, jsonSs);
            //FormsAuthentication.SetAuthCookie(userInfo.UserName, isPersistent, FormsAuthentication.FormsCookiePath);
            //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userInfo.Password, DateTime.Now, DateTime.Now.AddMinutes(expires),
            //    isPersistent, userData, FormsAuthentication.FormsCookiePath);
            //FormsIdentity identity = new FormsIdentity(ticket);
            //string encTicket = FormsAuthentication.Encrypt(ticket);
            //HttpCookie userCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
            //{
            //    HttpOnly = true,
            //    Path = ticket.CookiePath,
            //    Expires = ticket.IsPersistent ? ticket.Expiration : DateTime.MinValue,
            //    Domain = FormsAuthentication.CookieDomain,
            //};
            //Response.Cookies.Add(userCookie);
        }
        #endregion
        #region 注销登录
        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
        #endregion
        #region 用户注册
        /// <summary>
        /// 注册UI
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User_usr userInfo = new User_usr();
                userInfo.Email = model.Email;
                userInfo.Password = Unity.Mvc3.Helpers.Encoders.MD5.Encode(model.Password);
                userInfo.UserName = model.UserName;
                userInfo.DeptID = 1;
                if (_User_usrService.Insert(userInfo) > 0)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return Redirect("/");
                }
                else
                    ModelState.AddModelError("", "<=RegisterErrorMessage>");
            }
            return View(model);
        }
        #endregion
        #region 修改密码
        /// <summary>
        /// 修改密码UI
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }
        /// <summary>
        /// 修改密码事件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // 在某些出错情况下，ChangePassword 将引发异常，
                // 而不是返回 false。
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "当前密码不正确或新密码无效。");
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }
        /// <summary>
        /// 密码修改成功UI
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
        #endregion
        #region 用户是否存在
        public JsonResult CheckUserNameExists(string userName)
        {
            return Json(!_User_usrService.IsUserExists(userName), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 邮件是否存在
        public JsonResult CheckEmailExists(string email)
        {
            return Json(!_User_usrService.IsEmailExists(email), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}

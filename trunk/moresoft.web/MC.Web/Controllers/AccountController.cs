﻿using System;
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
        private readonly Imc_User _mc_UserService;
        public AccountController(Imc_User mc_UserService)
        {
            _mc_UserService = mc_UserService;
        }
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
                var userInfo = _mc_UserService.GetUserLogin(model.UserName, Unity.Mvc3.Helpers.Encoders.MD5.Encode(model.Password), Request.UserHostAddress);
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
        private void SetCookie(mc_User userInfo, bool rememberMe)
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
                mc_User userInfo = new mc_User();
                userInfo.Email = model.Email;
                userInfo.Password = Unity.Mvc3.Helpers.Encoders.MD5.Encode(model.Password);
                userInfo.UserName = model.UserName;
                userInfo.DeptID = 1;
                if (_mc_UserService.Insert(userInfo) > 0)
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
        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // 请参见 http://go.microsoft.com/fwlink/?LinkID=177550 以查看
            // 状态代码的完整列表。
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "用户名已存在。请输入不同的用户名。";

                case MembershipCreateStatus.DuplicateEmail:
                    return "该电子邮件地址的用户名已存在。请输入不同的电子邮件地址。";

                case MembershipCreateStatus.InvalidPassword:
                    return "提供的密码无效。请输入有效的密码值。";

                case MembershipCreateStatus.InvalidEmail:
                    return "提供的电子邮件地址无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidAnswer:
                    return "提供的密码取回答案无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidQuestion:
                    return "提供的密码取回问题无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidUserName:
                    return "提供的用户名无效。请检查该值并重试。";

                case MembershipCreateStatus.ProviderError:
                    return "身份验证提供程序返回了错误。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";

                case MembershipCreateStatus.UserRejected:
                    return "已取消用户创建请求。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";

                default:
                    return "发生未知错误。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Web.SysAdmin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && Request.Form.Count > 0)
            {
                string userName = Request.Form["userName"];
                string password = Request.Form["password"];
                bool rememberMe = !string.IsNullOrEmpty(Request.Form["rememberMe"]);
                var userInfo = MC.BLL.mc_UserBLL.GetUserLogin(userName, password, Request.UserHostAddress);
                string json = string.Empty;
                if (userInfo != null)
                {
                    bool isPersistent = rememberMe;
                    int expires = rememberMe ? 24 * 60 * 14 : 30;
                    JsonSerializerSettings jsonSs = new JsonSerializerSettings();
                    jsonSs.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
                    string userData = JsonConvert.SerializeObject(userInfo, Newtonsoft.Json.Formatting.None, jsonSs);
                    FormsAuthentication.SetAuthCookie(userName, isPersistent, FormsAuthentication.FormsCookiePath);
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(expires),
                        isPersistent, userData, FormsAuthentication.FormsCookiePath);
                    FormsIdentity identity = new FormsIdentity(ticket);
                    string encTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie userCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
                    {
                        HttpOnly = true,
                        Path = ticket.CookiePath,
                        Expires = ticket.IsPersistent ? ticket.Expiration : DateTime.MinValue,
                        Domain = FormsAuthentication.CookieDomain,
                    };
                    Response.Cookies.Add(userCookie);
                    json = JsonConvert.SerializeObject(new { success = true, msg = "登录成功" }, Formatting.None);
                }
                else
                    json = JsonConvert.SerializeObject(new { success = false, msg = "用户名或者密码错误" }, Formatting.None);
                Response.ContentType = "text/plain;charset=utf-8";
                Response.Write(json);
                Response.End();
            }
        }
    }
}

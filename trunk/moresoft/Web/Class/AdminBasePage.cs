using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Web
{
    public class AdminBasePage : Page
    {
        public MC.Model.mc_User userInfo { get; set; }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[cookieName];
            FormsAuthenticationTicket authTicket = null;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                JsonSerializerSettings jsonSs = new JsonSerializerSettings();
                userInfo = (MC.Model.mc_User)JsonConvert.DeserializeObject(authTicket.UserData, typeof(MC.Model.mc_User), jsonSs);
                if (userInfo == null)
                {
                    Response.ContentType = "application/json;charset=utf-8";
                    HttpContext.Current.Response.Write(JsonConvert.SerializeObject(new { success = false, msg = "用户不存在" }, Formatting.None));
                    HttpContext.Current.Response.End();
                }
            }
            catch
            {
                Response.ContentType = "application/json;charset=utf-8";
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(new { success = false, msg = "没有权限" }, Formatting.None));
                HttpContext.Current.Response.End();
            }
        }
    }
}

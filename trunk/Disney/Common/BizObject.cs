using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.Caching;
using System.Security.Principal;
using System.Web.SessionState;

namespace Common
{
    public abstract class BizObject
    {
        protected static HttpRequest Request
        {
            get { return HttpContext.Current.Request; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public static int UserID
        {
            get
            {
                if (Request.Cookies["uid"] == null || string.IsNullOrEmpty(Request.Cookies["uid"].Value))
                    return 0;
                int userid = 0;
                try
                {
                    int.TryParse(DESEncrypt.Decrypt(Request.Cookies["uid"].Value), out userid);
                }
                catch
                {
                }
                return userid;
            }
        }
        /// <summary>
        /// 机构ID
        /// </summary>
        public static int InsID
        {
            get
            {
                //if (Request.Cookies["uid"] == null || string.IsNullOrEmpty(Request.Cookies["uid"].Value))
                //    return 0;
                //int userid = 0;
                //try
                //{
                //    if (int.TryParse(Request.Cookies["uid"].Value, out userid))
                //        return 0;
                //    int.TryParse(DESEncrypt.Decrypt(Request.Cookies["uid"].Value), out userid);
                //}
                //catch
                //{
                //}
                return 1;
            }
        }
        public static int PageIndex
        {
            get
            {
                if (string.IsNullOrEmpty(Request.Params["page"]))
                    return 1;
                if (!Utils.IsInt(Request.Params["page"], true))
                    HttpContext.Current.Response.Redirect("/error?msg=3");
                return int.Parse(Request.Params["page"]);
            }
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        public static string ValidateCode
        {
            get
            {
                if (HttpContext.Current.Session["CheckCode"] == null)
                    return "";
                return HttpContext.Current.Session["CheckCode"].ToString();

            }
        }
    }
}

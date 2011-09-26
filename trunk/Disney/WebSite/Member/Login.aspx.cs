using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using Common;
using BLL;
using Models;
using Newtonsoft.Json;

public partial class Member_Login : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.Form.Count > 0)
            {
                string returnUrl = Request.Form["returnUrl"];
                string username = Request.Form["username"];
                bool rememberMe = !string.IsNullOrEmpty(Request.Form["rememberMe"]);
                string email = Request.Form["email"];
                string password = Request.Form["password"];
                //string validateCode = Request.Form["validateCode"];
                #region 验证
                if (string.IsNullOrEmpty(username))
                {
                    Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = false, msg = "請填寫會員帳號" }));
                    Response.End();
                }
                if (string.IsNullOrEmpty(password) || password.Length < 5)
                {
                    Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = false, msg = "密碼長試不可以小于5位" }));
                    Response.End();
                }
                //if (string.IsNullOrEmpty(validateCode) || Session["CheckCode"] == null || string.Compare(Session["CheckCode"].ToString(), validateCode.Trim().ToLower(), true) != 0)
                //{
                //    Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = false, msg = "驗證碼錯誤，請重新填寫" }));
                //    Response.End();
                //}
                //else Session["CheckCode"] = string.Empty;
                #endregion
                t_User item = null;
                password = DESEncrypt.Encrypt(password);
                item = t_UserBLL.GetUserLogin(username, 1, password);
                if (item != null && !item.IsClose)
                {
                    int expires = 0;
                    if (rememberMe)
                        expires = 1440 * 365 * 10;
                    HttpCookie cookie = Request.Cookies["uid"];
                    if (cookie == null)
                        cookie = new HttpCookie("uid");
                    cookie.Value = DESEncrypt.Encrypt(item.ID.ToString());
                    if (expires != 0)
                        cookie.Expires = DateTime.Now.AddMinutes(expires);
                    Response.Cookies.Add(cookie);
                    if (string.IsNullOrEmpty(returnUrl))
                        returnUrl = "/member";
                    Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = true, msg = "登錄完成", data = returnUrl }));
                    Response.End();
                }
                Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = false, msg = "登錄失敗，會員不存在，請先註冊" }));
                Response.End();
            }
        }
    }
}

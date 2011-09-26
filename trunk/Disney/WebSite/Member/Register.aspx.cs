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

public partial class Member_Register : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.Form.Count > 0)
            {
                string returnUrl = Request.Form["returnUrl"];
                string username = Request.Form["username"];
                string truename = Request.Form["truename"];
                string email = Request.Form["email"];
                string password = Request.Form["password"];
                string confirmPassword = Request.Form["confirmPassword"];
                string sex = Request.Form["sex"];
                string countryid = Request.Form["countryid"];
                #region 验证
                if (string.IsNullOrEmpty(truename))
                {
                    Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = false, msg = "請填寫姓名" }));
                    Response.End();
                }
                if (string.IsNullOrEmpty(username))
                {
                    Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = false, msg = "請填寫會員帳號" }));
                    Response.End();
                }
                else
                {
                    if (t_UserBLL.IsUserNameExists(username))
                    {
                        Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = false, msg = "會員帳號已經存在" }));
                        Response.End();
                    }
                }
                if (String.IsNullOrEmpty(email))
                {
                    Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = false, msg = "請填寫郵箱" }));
                    Response.End();
                }
                else
                {
                    if (Utils.IsEmail(email))
                    {
                        if (t_UserBLL.IsEmailExists(email))
                        {
                            Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = false, msg = "郵箱已經存在" }));
                            Response.End();
                        }
                    }
                    else
                    {
                        Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = false, msg = "郵箱格式不正確" }));
                        Response.End();
                    }
                }
                if (string.IsNullOrEmpty(password) || password.Length < 5)
                {
                    Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = false, msg = "密碼長試不可以小于5位" }));
                    Response.End();
                }
                if (!String.Equals(password, confirmPassword, StringComparison.Ordinal))
                {
                    Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = false, msg = "兩次密碼不一致" }));
                    Response.End();
                }
                if (string.IsNullOrEmpty(sex))
                {
                    Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = false, msg = "性別沒有選擇" }));
                    Response.End();
                }
                else
                {
                    if (!Utils.isNumber(sex, false))
                    {
                        Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = false, msg = "性別格式不正確" }));
                        Response.End();
                    }
                }
                if (string.IsNullOrEmpty(countryid))
                {
                    Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = false, msg = "縣市沒有選擇" }));
                    Response.End();
                }
                else
                {
                    if (!Utils.isNumber(countryid, false))
                    {
                        Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = false, msg = "縣市格式不正確" }));
                        Response.End();
                    }
                }
                #endregion
                t_User item = new t_User();
                item.UserName = username;
                item.TrueName = truename;
                item.Email = email;
                item.Password = DESEncrypt.MD5Encrypt(password);
                item.Avatar = string.Empty;
                item.RegTime = DateTime.Now;
                item.Sex = int.Parse(sex);
                item.CountryID = int.Parse(countryid);
                item.LoginTime = item.RegTime;
                item.LoginIP = Request.UserHostAddress;
                item.Birthday = Request.Form["birthday"];
                item.Mobile = Request.Form["mobile"];
                item.Tel = Request.Form["tel"];
                int revalue = t_UserBLL.Insert(item);
                if (revalue > 0)
                {
                    t_UserInfo mst = new t_UserInfo();
                    mst.UserID = revalue;
                    mst.IsEmail = !string.IsNullOrEmpty(Request["isemail"]);
                    mst.Address = Request.Form["address"];
                    mst.Zip = Request.Form["zip"];
                    t_UserInfoBLL.Insert(mst);
                    HttpCookie cookie = Request.Cookies["uid"];
                    if (cookie == null)
                        cookie = new HttpCookie("uid");
                    cookie.Value = DESEncrypt.Encrypt(revalue.ToString());
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);
                    if (string.IsNullOrEmpty(returnUrl))
                        returnUrl = "/member";
                    Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = true, msg = "註冊完成", data = returnUrl }));
                    Response.End();
                }
            }
        }
    }
}

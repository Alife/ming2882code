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

public partial class Member_Profile : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.Form.Count > 0)
            {
                string truename = Request.Form["truename"];
                string email = Request.Form["email"];
                string oldemail = Request.Form["oldemail"];
                string sex = Request.Form["sex"];
                string countryid = Request.Form["countryid"];
                #region 验证
                if (string.IsNullOrEmpty(truename))
                {
                    Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = false, msg = "請填寫姓名" }));
                    Response.End();
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
                        if (!string.IsNullOrEmpty(oldemail) && email.ToLower() != oldemail.ToLower())
                        {
                            if (t_UserBLL.IsEmailExists(email))
                            {
                                Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = false, msg = "郵箱已經存在" }));
                                Response.End();
                            }
                        }
                    }
                    else
                    {
                        Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = false, msg = "郵箱格式不正確" }));
                        Response.End();
                    }
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
                t_User item = t_UserBLL.GetItem(BizObject.UserID);
                item.TrueName = truename;
                item.Email = email;
                item.Avatar = string.Empty;
                item.RegTime = DateTime.Now;
                item.Sex = int.Parse(sex);
                item.CountryID = int.Parse(countryid);
                item.Birthday = Request.Form["birthday"];
                item.Mobile = Request.Form["mobile"];
                item.Tel = Request.Form["tel"];
                int revalue = t_UserBLL.Update(item);
                if (revalue > 0)
                {
                    t_UserInfo mst = t_UserInfoBLL.GetItem(item.ID);
                    mst.UserID = item.ID;
                    mst.IsEmail = !string.IsNullOrEmpty(Request["isemail"]);
                    mst.Address = Request.Form["address"];
                    mst.Zip = Request.Form["zip"];
                    t_UserInfoBLL.Update(mst);
                    Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = true, msg = "修改完成" }));
                    Response.End();
                }
                Response.Write(JsonConvert.SerializeObject(new MessageBox() { success = false, msg = "修改失敗" }));
                Response.End();
            }
        }
    }
}

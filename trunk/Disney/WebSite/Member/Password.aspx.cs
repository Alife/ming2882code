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

public partial class Member_Password : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.Form.Count > 0)
            {
                string currentPassword = Request.Form["currentPassword"];
                string newPassword = Request.Form["newPassword"];
                string confirmPassword = Request.Form["confirmPassword"];
                MessageBox r;
                if (string.IsNullOrEmpty(currentPassword))
                    r = new MessageBox(false, "旧密码没有填写");
                if (string.IsNullOrEmpty(newPassword))
                    r = new MessageBox(false, "新密码没有填写");
                if (string.IsNullOrEmpty(confirmPassword))
                    r = new MessageBox(false, "确认密码没有填写");
                if (!newPassword.Equals(confirmPassword))
                    r = new MessageBox(false, "新密码和确认密码不一致");
                if (t_UserBLL.UpdatePass(BizObject.UserID, DESEncrypt.MD5Encrypt(currentPassword), DESEncrypt.MD5Encrypt(newPassword)) > 0)
                    r = new MessageBox(true, "密码修改完成");
                else
                    r = new MessageBox(false, "旧密码不正确");
                Response.Write(JsonConvert.SerializeObject(r));
                Response.End();
            }
        }
    }
}

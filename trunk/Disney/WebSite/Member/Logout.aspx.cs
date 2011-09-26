using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Request.Cookies.Clear();
        HttpCookie cookie = new HttpCookie("uid", string.Empty);
        cookie.Expires = DateTime.Now.AddMinutes(-1);
        Response.Cookies.Add(cookie);
        Response.Redirect("/member/login.aspx");
    }
}

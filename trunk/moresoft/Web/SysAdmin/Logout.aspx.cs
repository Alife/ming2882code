using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Web.SysAdmin
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Request.Cookies.Clear();
            Response.Write("<script>top.location.href='login.aspx';</script>");
            Response.End();
        }
    }
}

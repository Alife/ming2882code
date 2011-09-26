using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Orders_Shipping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Count > 0)
        {
            List<string> ids = Request["id"].Split(',').ToList();
            BLL.ShippingBLL.Delete(ids);
            Response.Redirect(Request.RawUrl);
        }
    }
}

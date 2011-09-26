using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Models;

public partial class Admin_Orders_ShippingAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Count > 0)
        {
            Shipping item = new Shipping();
            item.Price = decimal.Parse(Request.Form["price"]);
            item.Name = Request.Form["name"];
            item.OrderID = int.Parse(Request.Form["orderid"]);
            ShippingBLL.Insert(item);
            Response.Redirect("shipping.aspx");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Models;

public partial class Admin_Info_PhotoCategoryEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Count > 0)
        {
            w_PhotoCategory item = w_PhotoCategoryBLL.GetItem(int.Parse(Request.Form["id"]));
            item.Intro = Request.Form["intro"];
            item.Name = Request.Form["name"];
            item.OrderID = int.Parse(Request.Form["orderid"]);
            item.ShootingTime = DateTime.Parse(Request.Form["ShootingTime"]);
            w_PhotoCategoryBLL.Update(item);
            Response.Redirect("photocategory.aspx");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using MC.BLL;
using MC.Model;

namespace Web.SysAdmin
{
    public partial class InfoType1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && Request.QueryString.Count > 0)
            {
                string type = Request.QueryString["type"];
                if (type == "load")
                {
                    var list = LoadChild(0);
                    string json = JsonConvert.SerializeObject(list, Formatting.None);
                    Response.ContentType = "application/json;charset=utf-8";
                    Response.Write(json);
                    Response.End();
                }
            }
        }
        private IList<InfoType_ift> LoadChild(int parentID)
        {
            QueryInfo info = new QueryInfo();
            info.Parameters.Add("Parent_ift", parentID);
            info.Orderby.Add("Sort_ift", null);
            var list = InfoType_iftBLL.GetList(info);
            foreach (var item in list)
                item.children = LoadChild(item.ID_ift.Value);
            return list;
        }
    }
}

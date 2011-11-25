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
using CoolCode.Web;

namespace Web.SysAdmin
{
    public partial class InfoType1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && Request.QueryString.Count > 0)
            {
                string type = ReqHelper.Get<string>("type");
                string json = string.Empty;
                switch (type)
                {
                    case "load":
                        json = JsonConvert.SerializeObject(LoadChild(0), Formatting.None);
                        break;
                    case "loadtree":
                        var tree = LoadTree(0);
                        tree.Insert(0, new TreeEntity { id = 0, text = "" });
                        json = JsonConvert.SerializeObject(tree, Formatting.None);
                        break;
                    case "form":
                        InfoType_ift ift = new InfoType_ift();
                        this.TryUpdateModel(ift);
                        InfoType_iftBLL.Insert(ift);
                        break;
                }
                Response.ContentType = "application/json;charset=utf-8";
                Response.Write(json);
                Response.End();
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
        private List<TreeEntity> LoadTree(int parentID)
        {
            List<TreeEntity> tree = new List<TreeEntity>();
            QueryInfo info = new QueryInfo();
            info.Parameters.Add("Parent_ift", parentID);
            info.Orderby.Add("Sort_ift", null);
            var list = InfoType_iftBLL.GetList(info);
            foreach (var item in list)
                tree.Add(new TreeEntity { id = item.ID_ift.Value, text = item.Name_ift, children = LoadTree(item.ID_ift.Value) });
            return tree;
        }
    }
}

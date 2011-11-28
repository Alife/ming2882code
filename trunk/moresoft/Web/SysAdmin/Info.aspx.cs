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
    public partial class Info : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && Request.QueryString.Count > 0 && string.IsNullOrEmpty(Request.QueryString["_"]))
            {
                string type = ReqHelper.Get<string>("type");
                string json = string.Empty;
                int v = 0;
                switch (type)
                {
                    case "load":
                        json = JsonConvert.SerializeObject(Info_infBLL.GetPageList(Funs.GetQueryInfo()), Formatting.None);
                        break;
                    case "form":
                        Info_inf ift = new Info_inf();
                        this.TryUpdateModel(ift);
                        if (ReqHelper.Get<string>("action") == "add")
                            v = Info_infBLL.Insert(ift);
                        else
                            v = Info_infBLL.Update(ift);
                        if (v > 0)
                            json = JsonConvert.SerializeObject(new { success = true, msg = "保存成功" }, Formatting.None);
                        else
                            json = JsonConvert.SerializeObject(new { success = false, msg = "保存失败" }, Formatting.None);
                        break;
                    case "del":
                        v = Info_infBLL.Delete(ReqHelper.Get<string>("id").Split(',').ToList());
                        if (v > 0)
                            json = JsonConvert.SerializeObject(new { success = true, msg = "删除成功" }, Formatting.None);
                        else
                            json = JsonConvert.SerializeObject(new { success = false, msg = "删除失败，分类下有子类无法删除" }, Formatting.None);
                        break;
                }
                Response.ContentType = "application/json;charset=utf-8";
                Response.Write(json);
                Response.End();
            }
        }
    }
}
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
    public partial class InfoDetail : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = ReqHelper.Get<string>("type");
            if (!Page.IsPostBack && !string.IsNullOrEmpty(type) && string.IsNullOrEmpty(Request.QueryString["_"]))
            {
                string json = string.Empty; int v = 0;
                switch (type)
                {
                    case "load":
                        int id = ReqHelper.Get<int>("id");
                        JsonSerializerSettings jsonSs = new JsonSerializerSettings();
                        jsonSs.Converters.Add(new Newtonsoft.Json.Converters.IsoDateTimeConverter());
                        //jsonSs.Converters.Add(new Newtonsoft.Json.Converters.DataTableConverter());
                        json = JsonConvert.SerializeObject(Info_infBLL.GetItem(id), Formatting.None, jsonSs);
                        break;
                    case "form":
                        Info_inf ift = new Info_inf();
                        this.TryUpdateModel(ift);
                        if (ift.IndexTagID_inf.HasValue && ift.IndexTagID_inf.Value == 0) ift.IndexTagID_inf = null;
                        ift.TopType_inf = ReqHelper.Get<string>("TopType_inf");
                        if (ReqHelper.Get<string>("action") == "add")
                            v = Info_infBLL.Insert(ift);
                        else
                            v = Info_infBLL.Update(ift);
                        if (v > 0)
                            json = JsonConvert.SerializeObject(new { success = true, msg = "保存成功" }, Formatting.None);
                        else
                            json = JsonConvert.SerializeObject(new { success = false, msg = "保存失败" }, Formatting.None);
                        break;
                }
                Response.ContentType = "application/json;charset=utf-8";
                Response.Write(json);
                Response.End();
            }
        }
    }
}

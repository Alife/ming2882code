using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
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
                        Info_inf inf = new Info_inf();
                        this.TryUpdateModel(inf);
                        if (inf.IndexTagID_inf.HasValue && inf.IndexTagID_inf.Value == 0) inf.IndexTagID_inf = null;
                        if (inf.InfoTypeID_inf.HasValue && inf.InfoTypeID_inf.Value == 0) inf.InfoTypeID_inf = null;
                        inf.TopType_inf = ReqHelper.Get<string>("TopType_inf");
                        string resultContent = inf.Content_inf;
                        IList<Keywords_key> keys = Keywords_keyBLL.GetList(new QueryInfo());
                        foreach (var key in keys)
                        {
                            int replace_time = 0;
                            resultContent = Regex.Replace(resultContent, @"(?i)(?<=^|>[^<>]*?)(?<!<a[^>]*>((?!</a).)*)" + key.Name_key, delegate(Match m)
                            {
                                return replace_time++ < key.Num_key.Value ? string.Format("<a href=\"{1}\" title=\"{0}\">{0}</a>", key.Name_key, key.Url_key) : m.Value;
                            }, RegexOptions.IgnoreCase);
                        }
                        inf.Content_inf = resultContent;
                        if (ReqHelper.Get<string>("action") == "add")
                            v = Info_infBLL.Insert(inf);
                        else
                            v = Info_infBLL.Update(inf);
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

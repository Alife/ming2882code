using System;
using System.Collections;
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

    public partial class Keywords : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = ReqHelper.Get<string>("type");
            if (!Page.IsPostBack && !string.IsNullOrEmpty(type) && string.IsNullOrEmpty(Request.QueryString["_"]))
            {
                string json = string.Empty;
                int v = 0;
                switch (type)
                {
                    case "load":
                        json = JsonConvert.SerializeObject(Keywords_keyBLL.GetPageList(Funs.GetQueryInfo()), Formatting.None);
                        break;
                    case "form":
                        Keywords_key key = new Keywords_key();
                        this.TryUpdateModel(key);
                        string Name_key_Old = ReqHelper.Get<string>("Name_key_Old");
                        if (string.IsNullOrEmpty(Name_key_Old) || Name_key_Old != key.Name_key)
                        {
                            if (Keywords_keyBLL.IsHasName(key.Name_key))
                                json = JsonConvert.SerializeObject(new { success = false, msg = "保存失败，已经有相同的关键字" }, Formatting.None);
                        }
                        if (json == string.Empty)
                        {
                            if (!key.Sort_key.HasValue) key.Sort_key = 5;
                            if (ReqHelper.Get<string>("action") == "add")
                                v = Keywords_keyBLL.Insert(key);
                            else
                                v = Keywords_keyBLL.Update(key);
                            if (v > 0)
                                json = JsonConvert.SerializeObject(new { success = true, msg = "保存成功" }, Formatting.None);
                            else
                                json = JsonConvert.SerializeObject(new { success = false, msg = "保存失败" }, Formatting.None);
                        }
                        break;
                    case "del":
                        v = Keywords_keyBLL.Delete(ReqHelper.Get<string>("id").Split(',').ToList());
                        if (v > 0)
                            json = JsonConvert.SerializeObject(new { success = true, msg = "删除成功" }, Formatting.None);
                        else
                            json = JsonConvert.SerializeObject(new { success = false, msg = "删除失败" }, Formatting.None);
                        break;
                }
                Response.ContentType = "application/json;charset=utf-8";
                Response.Write(json);
                Response.End();
            }
        }
    }
}
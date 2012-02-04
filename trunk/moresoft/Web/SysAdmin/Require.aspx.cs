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

    public partial class Require : AdminBasePage
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
                        json = JsonConvert.SerializeObject(Require_reqBLL.GetPageList(Funs.GetQueryInfo()), Formatting.None);
                        break;
                    case "form":
                        Require_req key = new Require_req();
                        this.TryUpdateModel(key);
                        if (ReqHelper.Get<string>("action") == "add")
                            v = Require_reqBLL.Insert(key);
                        else
                            v = Require_reqBLL.Update(key);
                        if (v > 0)
                            json = JsonConvert.SerializeObject(new { success = true, msg = "保存成功" }, Formatting.None);
                        else
                            json = JsonConvert.SerializeObject(new { success = false, msg = "保存失败" }, Formatting.None);
                        break;
                    case "del":
                        v = Require_reqBLL.Delete(ReqHelper.Get<string>("id").Split(',').ToList());
                        if (v > 0)
                            json = JsonConvert.SerializeObject(new { success = true, msg = "删除成功" }, Formatting.None);
                        else
                            json = JsonConvert.SerializeObject(new { success = false, msg = "删除失败" }, Formatting.None);
                        break;
                }
                Response.ContentType = "text/plain;charset=utf-8";
                Response.Write(json);
                Response.End();
            }
        }
    }
}
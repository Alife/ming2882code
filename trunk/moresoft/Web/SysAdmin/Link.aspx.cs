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

    public partial class Link : AdminBasePage
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
                        json = JsonConvert.SerializeObject(Link_lnkBLL.GetPageList(Funs.GetQueryInfo()), Formatting.None);
                        break;
                    case "loadall":
                        var list = Link_lnkBLL.GetList(Funs.GetQueryInfo());
                        ArrayList lst = new ArrayList();
                        foreach(var item in list)
                            lst.Add(new { id = item.ID_lnk, text = item.Name_lnk });
                        lst.Insert(0, new { id = 0, text = "请选择" });
                        json = JsonConvert.SerializeObject(lst, Formatting.None);
                        break;
                    case "form":
                        Link_lnk ift = new Link_lnk();
                        this.TryUpdateModel(ift);
                        if (!ift.Sort_lnk.HasValue) ift.Sort_lnk = 99;
                        if (ReqHelper.Get<string>("action") == "add")
                            v = Link_lnkBLL.Insert(ift);
                        else
                            v = Link_lnkBLL.Update(ift);
                        if (v > 0)
                            json = JsonConvert.SerializeObject(new { success = true, msg = "保存成功" }, Formatting.None);
                        else
                            json = JsonConvert.SerializeObject(new { success = false, msg = "保存失败" }, Formatting.None);
                        break;
                    case "del":
                        v = Link_lnkBLL.Delete(ReqHelper.Get<string>("id").Split(',').ToList());
                        if (v > 0)
                            json = JsonConvert.SerializeObject(new { success = true, msg = "删除成功" }, Formatting.None);
                        else
                            json = JsonConvert.SerializeObject(new { success = false, msg = "删除失败，分类下有子类无法删除" }, Formatting.None);
                        break;
                }
                Response.ContentType = "text/plain;charset=utf-8";
                Response.Write(json);
                Response.End();
            }
        }
    }
}
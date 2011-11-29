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

    public partial class IndexTag : AdminBasePage
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
                        json = JsonConvert.SerializeObject(IndexTag_itgBLL.GetPageList(Funs.GetQueryInfo()), Formatting.None);
                        break;
                    case "loadall":
                        var list = IndexTag_itgBLL.GetList(Funs.GetQueryInfo());
                        ArrayList lst = new ArrayList();
                        foreach(var item in list)
                            lst.Add(new { id = item.ID_itg, text = item.Name_itg });
                        lst.Insert(0, new { id = 0, text = "请选择" });
                        json = JsonConvert.SerializeObject(lst, Formatting.None);
                        break;
                    case "form":
                        IndexTag_itg ift = new IndexTag_itg();
                        this.TryUpdateModel(ift);
                        if (!ift.Sort_itg.HasValue) ift.Sort_itg = 99;
                        if (ReqHelper.Get<string>("action") == "add")
                            v = IndexTag_itgBLL.Insert(ift);
                        else
                            v = IndexTag_itgBLL.Update(ift);
                        if (v > 0)
                            json = JsonConvert.SerializeObject(new { success = true, msg = "保存成功" }, Formatting.None);
                        else
                            json = JsonConvert.SerializeObject(new { success = false, msg = "保存失败" }, Formatting.None);
                        break;
                    case "del":
                        v = IndexTag_itgBLL.Delete(ReqHelper.Get<string>("id").Split(',').ToList());
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
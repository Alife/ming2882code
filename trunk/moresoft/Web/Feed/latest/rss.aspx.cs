using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.RssCode;

namespace Web.Feed.latest
{
    public partial class rss : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Rss rss = new Rss();
            rss.OutputStream = Response.OutputStream;//输出流设置
            Channel channel = new Channel();
            List<Item> items = new List<Item>();
            channel.Copyright = "摩尔社区";
            channel.Description = "18个MES新问题";
            channel.Link = "http://www.moresoft.cn";
            channel.RssTitle = "摩尔社区-国内最专业的MES服务商 - 18个MES新问题:";
            channel.Generator = "";

            var qi = new MC.Model.QueryInfo();
            qi.Parameters.Add("TopType_inf", "news");
            qi.Parameters.Add("top", "18");
            qi.Orderby.Add("CreateTime_inf", "desc");
            var infos = MC.BLL.Info_infBLL.GetList(qi);
            foreach (var item in infos)
            {
                items.Add(new Item()
                {
                    Title = item.Title_inf,
                    Author = item.Author_inf,
                    Description = item.Content_inf,
                    Link = "http://www.moresoft.cn/" + item.InfoTypeID_inf + "_" + item.ID_inf + "_zh.html",
                    PubDate = Rss.GetRssDate(item.CreateTime_inf_Str)
                });
            }
            //绑定数据
            rss.Channel = channel;
            rss.Items = items;

            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "text/xml";
            Rss.PublishRss(rss);//发布RSS
            Response.End();
        }
    }
}

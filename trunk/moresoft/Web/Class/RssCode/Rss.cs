using System;
using System.Data;
using System.Configuration;
using System.Xml;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Web.RssCode
{
    /// <summary>
    /// Rss 的摘要说明
    /// </summary>
    public class Rss
    {

        #region 数据成员
        private Stream outputStream;
        private Channel channel;
        private List<Item> items;
        #endregion

        #region 属性
        public Stream OutputStream
        {
            get { return outputStream; }
            set { outputStream = value; }
        }
        public Channel Channel
        {
            get { return channel; }
            set { channel = value; }
        }
        public List<Item> Items
        {
            get { return items; }
            set { items = value; }
        }
        #endregion

        #region 构造函数
        public Rss()
        {

        }

        public Rss(Channel channel, List<Item> items)
        {
            this.channel = channel;
            this.items = items;
        }
        #endregion

        #region 静态方法
        public static void PublishRss(Rss r)
        {
            XmlTextWriter writer = new XmlTextWriter(r.OutputStream, Encoding.UTF8);
            writer.WriteStartDocument();
            writer.WriteStartElement("rss");
            writer.WriteAttributeString("version", "2.0");
            writer.WriteStartElement("channel");
            writer.WriteElementString("title", r.channel.RssTitle);
            writer.WriteElementString("link", r.channel.Link);
            writer.WriteElementString("Description", r.channel.Description);
            writer.WriteElementString("copyright", r.channel.Copyright);
            writer.WriteElementString("generator", r.channel.Generator);

            foreach (Item item in r.items)
            {
                writer.WriteStartElement("item");
                writer.WriteElementString("author", item.Author);
                writer.WriteElementString("title", item.Title);
                writer.WriteElementString("link", item.Link);
                writer.WriteElementString("description", item.Description);
                writer.WriteElementString("pubDate", item.PubDate);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();

        }

        public static string GetRssDate(Object date)
        {
            DateTime rssDate = Convert.ToDateTime(date);
            string[] monthName = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            StringBuilder sb = new StringBuilder();
            sb.Append(rssDate.Day);
            sb.Append(" ");
            sb.Append(monthName[Convert.ToInt32(rssDate.Month) - 1]);
            sb.Append(" ");
            sb.Append(rssDate.Year);
            sb.Append(" ");
            sb.Append(rssDate.ToLongTimeString());

            return sb.ToString();
        }

        public static DataSet ReadRSS(string path)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(path);
            return ds;
        }

        public static Channel GetChannel(string path)
        {
            DataSet ds = new DataSet();
            ds = ReadRSS(path);
            DataTable dt = ds.Tables["Channel"];

            Channel channel = new Channel();
            channel.RssTitle = (string)dt.Rows[0]["title"];
            channel.Link = (string)dt.Rows[0]["link"];
            channel.Generator = (string)dt.Rows[0]["generator"];
            channel.Description = (string)dt.Rows[0]["description"];
            channel.Copyright = (string)dt.Rows[0]["copyright"];
            return channel;
        }

        public static List<Item> GetFeeds(string path)
        {
            DataSet ds = new DataSet();
            ds = ReadRSS(path);
            DataTable dt = ds.Tables["item"];
            int rows = dt.Rows.Count;
            Item item;
            List<Item> itemList = new List<Item>();
            if (rows > 0)
            {
                for (int i = 0; i < rows; i++)
                {
                    item = new Item();
                    item.Author = (string)dt.Rows[i]["author"];
                    item.Description = (string)dt.Rows[i]["description"];
                    item.Link = (string)dt.Rows[i]["link"];
                    item.PubDate = (string)dt.Rows[i]["pubDate"];
                    item.Title = (string)dt.Rows[i]["title"];
                    itemList.Add(item);
                }
                return itemList;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
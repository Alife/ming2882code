using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.RssCode
{
    /// <summary>
    ///Feed 的摘要说明
    /// </summary>
    public class Item
    {
        #region 数据成员
        /// <summary>
        /// Item类
        /// </summary>
        private string title;//标题
        private string link;//链接
        private string author;//作者
        private string pubDate;//发布日期
        private string description;//内容
        #endregion

        #region 属性
        //属性封装
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Link
        {
            get { return link; }
            set { link = value; }
        }
        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        public string PubDate
        {
            get { return pubDate; }
            set { pubDate = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        #endregion
    }
}

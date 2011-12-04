using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.RssCode
{
    /// <summary>
    ///Channel 的摘要说明
    /// </summary>
    public class Channel
    {
        #region 数据成员
        private string rssTitle;//Rss Feed 标题
        private string link;//发布者URL
        private string description;//描述
        private string copyright;//版权
        private string generator;//产生这个Rss源的应用程序
        #endregion

        #region 属性
        public string RssTitle
        {
            get { return rssTitle; }
            set { rssTitle = value; }
        }
        public string Link
        {
            get { return link; }
            set { link = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Copyright
        {
            get { return copyright; }
            set { copyright = value; }
        }
        public string Generator
        {
            get { return generator; }
            set { generator = value; }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Unity.Mvc3.Helpers
{
    public class RenderPager
    {
        #region 字段

        /// <summary>    
        /// 当前页面的ViewContext    
        /// </summary>    
        private ViewContext viewContext;
        /// <summary>    
        /// 当前页码    
        /// </summary>    
        private readonly int currentPage;
        /// <summary>    
        /// 页面要显示的数据条数    
        /// </summary>    
        private readonly int pageSize;
        /// <summary>    
        /// 总的记录数    
        /// </summary>    
        private readonly int totalCount;
        /// <summary>    
        /// Pager Helper 要显示的页数    
        /// </summary>    
        private readonly int toDisplayCount;

        private readonly string pagelink;

        #endregion

        #region 构造函数

        public RenderPager(ViewContext viewContext, int currentPage, int pageSize, int totalCount, int toDisplayCount)
        {
            this.viewContext = viewContext;
            this.currentPage = currentPage;
            this.pageSize = pageSize;
            this.totalCount = totalCount;
            this.toDisplayCount = toDisplayCount;


            string reqUrl = viewContext.RequestContext.HttpContext.Request.RawUrl;
            string link = "";

            Regex re = new Regex(@"page=(\d+)|page=", RegexOptions.IgnoreCase);

            MatchCollection results = re.Matches(reqUrl);

            if (results.Count > 0)
            {
                link = reqUrl.Replace(results[0].ToString(), "page=[%page%]");
            }
            else if (reqUrl.IndexOf("?") < 0)
            {
                link = reqUrl + "?page=[%page%]";
            }
            else
            {
                link = reqUrl + "&page=[%page%]";
            }
            this.pagelink = link;
        }

        #endregion

        #region 方法

        public string RenderHtml()
        {
            if (totalCount <= pageSize)
                return string.Empty;

            //总页数    
            int pageCount = (int)Math.Ceiling(this.totalCount / (double)this.pageSize);

            //起始页    
            int start = 1;

            //结束页    
            int end = toDisplayCount;
            if (pageCount < toDisplayCount) end = pageCount;

            //中间值    
            int centerNumber = toDisplayCount / 2;

            if (pageCount > toDisplayCount)
            {

                //显示的第一位    
                int topNumber = currentPage - centerNumber;

                if (topNumber > 1)
                {
                    start = topNumber;
                }

                if (topNumber > pageCount - toDisplayCount)
                {
                    start = pageCount - toDisplayCount + 1;
                }

                //显示的最后一位    
                int endNumber = currentPage + centerNumber;

                if (currentPage >= pageCount - centerNumber)
                {
                    end = pageCount;
                }
                else
                {
                    if (endNumber > toDisplayCount)
                    {
                        end = endNumber;
                    }
                }

            }

            StringBuilder sb = new StringBuilder();

            //Previous    
            if (this.currentPage > 1)
            {
                sb.Append(GeneratePageLink("&lt;&lt;<=Prev>", this.currentPage - 1));
            }

            if (start > 1)
            {
                sb.Append(GeneratePageLink("1", 1));
                sb.Append("...");
            }

            //end Previous    

            for (int i = start; i <= end; i++)
            {
                if (i == this.currentPage)
                {
                    sb.AppendFormat("<span class=\"current\">{0}</span>", i);
                }
                else
                {
                    sb.Append(GeneratePageLink(i.ToString(), i));
                }
            }

            //Next    
            if (end < pageCount)
            {
                sb.Append("...");
                sb.Append(GeneratePageLink(pageCount.ToString(), pageCount));
            }

            if (this.currentPage < pageCount)
            {
                sb.Append(GeneratePageLink("<=Next>&gt;&gt;", this.currentPage + 1));
            }
            //end Next    

            sb.Append(" <span class=\"pagerInput\"><input type=\"text\" value=\"<=Jump>\" onfocus=\"if(this.value==this.defaultValue)this.value='';\" onblur=\"if(this.value=='')this.value=this.defaultValue;if (!isNaN(parseInt(this.value))) window.location='" + this.pagelink.Replace("[%page%]", "") + "'+this.value;\" id=\"pagerInput\" maxlength=\"4\" /></span> ");
            //sb.Append(" <span><input type=\"button\" class=\"btn3\" onkeypress=\"return event.keyCode>=48&&event.keyCode<=57\"/></span>");

            return sb.ToString();
        }

        /// <summary>    
        /// 生成Page的链接    
        /// </summary>    
        /// <param name="linkText">文字</param>    
        /// <param name="PageNumber">页码</param>    
        /// <returns></returns>    
        private string GeneratePageLink(string linkText, int PageNumber)
        {

            string linkFormat = string.Format(" <a href=\"{0}\">{1}</a> ", this.pagelink.Replace("[%page%]", PageNumber.ToString()), linkText);

            return linkFormat;

        }

        #endregion
    }
}

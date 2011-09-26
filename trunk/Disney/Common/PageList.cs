using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class PageList
    {
        public string Build(int intNumItem, int intPerPage, int intCurPage, string strURL)
        {
            StringBuilder builder = new StringBuilder();
            int num = 8;
            int num2 = 2;
            int num3 = ((intNumItem % intPerPage) > 0) ? ((intNumItem / intPerPage) + 1) : (intNumItem / intPerPage);
            if (num3 > 1)
            {
                intCurPage = (intCurPage > num3) ? 1 : intCurPage;
                int num4 = intCurPage - num2;
                int num5 = ((intCurPage + num) - num2) - 1;
                if (num > num3)
                {
                    num4 = 1;
                    num5 = num3;
                }
                else if (num4 < 1)
                {
                    num5 = (intCurPage + 1) - num4;
                    num4 = 1;
                    if (((num5 - num4) < num) && ((num5 - num4) < num3))
                    {
                        num5 = num;
                    }
                }
                else if (num5 > num3)
                {
                    num4 = (intCurPage - num3) + num5;
                    num5 = num3;
                    if (((num5 - num4) < num) && ((num5 - num4) < num3))
                    {
                        num4 = (num3 - num) + 1;
                    }
                }
                builder.AppendFormat("<div class=\"PageList\"><h6><span>{0}</span><span>{1}/{2}</span></h6><ul>", intNumItem, intCurPage, num3);
                if (num3 > 0)
                {
                    if (num4 > 1)
                    {
                        //builder.AppendFormat("<li class=\"direct\"><a href=\"" + strURL + "\">&laquo;</a></li><li>下</li>", "1");
                        builder.AppendFormat("<li class=\"direct\"><a href=\"" + strURL + "\">&laquo;</a></li>", "1");
                    }
                    if (intCurPage > 1)
                    {
                        builder.AppendFormat("<li class=\"direct\"><a href=\"" + strURL + "\">&#8249;</a></li>", intCurPage - 1);
                    }
                    for (int i = num4; i <= num5; i++)
                    {
                        if (i != intCurPage)
                        {
                            builder.AppendFormat("<li><a href=\"" + strURL + "\">{0}</a></li>", i);
                        }
                        else
                        {
                            builder.AppendFormat("<li><span class=\"current\">{0}</span></li>", i);
                        }
                    }
                    if (intCurPage < num3)
                    {
                        builder.AppendFormat("<li class=\"direct\"><a href=\"" + strURL + "\">&#8250;</a></li>", intCurPage + 1);
                    }
                    if (num5 < num3)
                    {
                        //builder.AppendFormat("<li>上</li><li class=\"direct\"><a href=\"" + strURL + "\">&raquo;</a></li>", num3);
                        builder.AppendFormat("<li class=\"direct\"><a href=\"" + strURL + "\">&raquo;</a></li>", num3);
                    }
                }
                builder.Append("</ul></div>");
            }
            return builder.ToString();
        }
        public string Build(int intNumItem, int intPerPage, string strURL)
        {
            int intCurPage = BizObject.PageIndex;
            StringBuilder builder = new StringBuilder();
            int num = 8;
            int num2 = 2;
            int num3 = ((intNumItem % intPerPage) > 0) ? ((intNumItem / intPerPage) + 1) : (intNumItem / intPerPage);
            if (num3 > 1)
            {
                intCurPage = (intCurPage > num3) ? 1 : intCurPage;
                int num4 = intCurPage - num2;
                int num5 = ((intCurPage + num) - num2) - 1;
                if (num > num3)
                {
                    num4 = 1;
                    num5 = num3;
                }
                else if (num4 < 1)
                {
                    num5 = (intCurPage + 1) - num4;
                    num4 = 1;
                    if (((num5 - num4) < num) && ((num5 - num4) < num3))
                    {
                        num5 = num;
                    }
                }
                else if (num5 > num3)
                {
                    num4 = (intCurPage - num3) + num5;
                    num5 = num3;
                    if (((num5 - num4) < num) && ((num5 - num4) < num3))
                    {
                        num4 = (num3 - num) + 1;
                    }
                }
                builder.AppendFormat("<div class=\"PageList\"><h6><span>{0}</span><span>{1}/{2}</span></h6><ul>", intNumItem, intCurPage, num3);
                if (num3 > 0)
                {
                    if (num4 > 1)
                    {
                        builder.AppendFormat("<li class=\"direct\"><a href=\"" + strURL + "\">&laquo;</a></li>", "1");
                        //builder.AppendFormat("<li class=\"direct\"><a href=\"" + strURL + "\">&laquo;</a></li><li>下</li>", "1");
                    }
                    if (intCurPage > 1)
                    {
                        builder.AppendFormat("<li class=\"direct\"><a href=\"" + strURL + "\">&#8249;</a></li>", intCurPage - 1);
                    }
                    for (int i = num4; i <= num5; i++)
                    {
                        if (i != intCurPage)
                        {
                            builder.AppendFormat("<li><a href=\"" + strURL + "\">{0}</a></li>", i);
                        }
                        else
                        {
                            builder.AppendFormat("<li><span class=\"current\">{0}</span></li>", i);
                        }
                    }
                    if (intCurPage < num3)
                    {
                        builder.AppendFormat("<li class=\"direct\"><a href=\"" + strURL + "\">&#8250;</a></li>", intCurPage + 1);
                    }
                    if (num5 < num3)
                    {
                        //builder.AppendFormat("<li>上</li><li class=\"direct\"><a href=\"" + strURL + "\">&raquo;</a></li>", num3);
                        builder.AppendFormat("<li class=\"direct\"><a href=\"" + strURL + "\">&raquo;</a></li>", num3);
                    }
                }
                builder.Append("</ul></div>");
            }
            return builder.ToString();
        }
        public string Build(int intNumItem, int intPerPage, int intCurPage, int num, string strURL)
        {
            StringBuilder builder = new StringBuilder();
            if (num < 0)
            {
                num = 5;
            }
            int num2 = 2;
            int num3 = ((intNumItem % intPerPage) > 0) ? ((intNumItem / intPerPage) + 1) : (intNumItem / intPerPage);
            if (num3 > 1)
            {
                intCurPage = (intCurPage > num3) ? 1 : intCurPage;
                int num4 = intCurPage - num2;
                int num5 = ((intCurPage + num) - num2) - 1;
                if (num > num3)
                {
                    num4 = 1;
                    num5 = num3;
                }
                else if (num4 < 1)
                {
                    num5 = (intCurPage + 1) - num4;
                    num4 = 1;
                    if (((num5 - num4) < num) && ((num5 - num4) < num3))
                    {
                        num5 = num;
                    }
                }
                else if (num5 > num3)
                {
                    num4 = (intCurPage - num3) + num5;
                    num5 = num3;
                    if (((num5 - num4) < num) && ((num5 - num4) < num3))
                    {
                        num4 = (num3 - num) + 1;
                    }
                }
                builder.AppendFormat("<div class=\"PageList\"><h6><span>{0}</span><span>{1}/{2}</span></h6><ul>", intNumItem, intCurPage, num3);
                if (num3 > 0)
                {
                    if (num4 > 1)
                    {
                        //builder.AppendFormat("<li class=\"direct\"><a href=\"" + strURL + "\">&laquo;</a></li><li>下</li>", "1");
                        builder.AppendFormat("<li class=\"direct\"><a href=\"" + strURL + "\">&laquo;</a></li>", "1");
                    }
                    if (intCurPage > 1)
                    {
                        builder.AppendFormat("<li class=\"direct\"><a href=\"" + strURL + "\">&#8249;</a></li>", intCurPage - 1);
                    }
                    for (int i = num4; i <= num5; i++)
                    {
                        if (i != intCurPage)
                        {
                            builder.AppendFormat("<li><a href=\"" + strURL + "\">{0}</a></li>", i);
                        }
                        else
                        {
                            builder.AppendFormat("<li><span class=\"current\">{0}</span></li>", i);
                        }
                    }
                    if (intCurPage < num3)
                    {
                        builder.AppendFormat("<li class=\"direct\"><a href=\"" + strURL + "\">&#8250;</a></li>", intCurPage + 1);
                    }
                    if (num5 < num3)
                    {
                        //builder.AppendFormat("<li>上</li><li class=\"direct\"><a href=\"" + strURL + "\">&raquo;</a></li>", num3);
                        builder.AppendFormat("<li class=\"direct\"><a href=\"" + strURL + "\">&raquo;</a></li>", num3);
                    }
                }
                builder.Append("</ul></div>");
            }
            return builder.ToString();
        }
    }
}

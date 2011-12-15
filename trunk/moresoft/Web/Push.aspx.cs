using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace Web
{
    public partial class Push : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string test = "csdn<div class=\"sdjcsdn\"><a href=\"http://www.csdn.net\">csdn</a>是个好东西，我们大家都喜欢<img src=\"http://localhost:2973/images/rss.png\" alt=\"acsdnc\"/>你喜欢<a href=\"http://www.csdn.net\">csdn</a>么？ <a href=\"http://www.xuexi520.cn/juqing/\" title=\"csdn-juqing\">csdn-juqing</a>哈哈，就是这个东西。";
            //int replace_time = 0;
            //string result = Regex.Replace(test, @"(?i)(?<=^|>[^<>]*?)(?<!<a[^>]*>((?!</a).)*)csdn", delegate(Match m)
            //{
            //    return replace_time++ < 2 ? "<a href=\"http://www.csdn.net\">csdn</a>" : m.Value;
            //});
            //Response.Write(result);
            //Response.End();
        }
    }
}

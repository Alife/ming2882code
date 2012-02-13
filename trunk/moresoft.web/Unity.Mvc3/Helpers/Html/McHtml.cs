using System.Text;
using System.Web.Mvc;

namespace Unity.Mvc3.Helpers.Html
{
    /// <summary>
    /// View与后台代码里使用
    /// </summary>
    public static class McHtml
    {
        const string keyForScript = "__key_For_Js_StringBuilder";
        const string keyForStyle = "__key_For_Css_StringBuilder";

        /// <summary> 
        /// 创建的StringBuilder在ViewDataDictionary
        /// 允许访问StringBuilder类. 
        /// <</summary> 
        /// <param name="key"></param> 
        /// <param name="delimBetweenStrings">
        static void AddToStringBuilder(this ViewDataDictionary dictionary, string key, string addString, char delimBetweenStrings)
        {
            StringBuilder str;
            object viewDataObject;

            if (!dictionary.TryGetValue(key, out viewDataObject))
            {
                str = new StringBuilder();
                str.Append(addString);
                dictionary[key] = str;
            }
            else
            {
                str = viewDataObject as StringBuilder;
                str.Append(delimBetweenStrings);
                str.Append(addString);
            }
        }

        /// <summary> 
        /// 添加脚本列表. 
        /// </summary> 
        public static void Script(this HtmlHelper html, string path)
        {
            html.ViewData.AddToStringBuilder(keyForScript, path, '|');
        }

        /// <summary> 
        /// 添加样式列表. 
        /// <</summary> 
        public static void Style(this HtmlHelper html, string path)
        {
            html.ViewData.AddToStringBuilder(keyForStyle, path, '|');
        }

        /// <summary> 
        /// 将脚本存储在ViewData的清单
        /// 转换成一个“压缩”的脚本标记脚本列表 
        /// </summary> 
        public static string CompressJs(this HtmlHelper html, UrlHelper url, bool cache)
        {
            var builder = html.ViewData[keyForScript] as StringBuilder;
            string urlPath = url.Action("Script", "Zip", new { Path = builder.ToString(), Cache = cache });
            builder = null;
            return string.Format(@"<script type=""text/javascript"" src=""{0}""></script>", urlPath);
        }
        public static string CompressJs(this HtmlHelper html, UrlHelper url)
        {
            return html.CompressJs(url, true);
        }

        /// <summary> 
        /// 将样式存储在ViewData的清单 
        /// 转换成一个“压缩”的样式列表 
        /// </summary> 
        public static string CompressCss(this HtmlHelper html, UrlHelper url, bool cache)
        {
            var builder = html.ViewData[keyForStyle] as StringBuilder;
            string urlPath = url.Action("Style", "Zip", new { Path = builder.ToString(), Cache = cache });
            builder = null;
            return string.Format(@"<link href=""{0}"" type=""text/css"" rel=""stylesheet"" />", urlPath);
        }
        public static string CompressCss(this HtmlHelper html, UrlHelper url)
        {
            return html.CompressCss(url, true);
        }
    }
}
namespace System.Web.Mvc.Html
{
    /// <summary>
    /// Format 
    /// </summary>
    public static class FormatExtensions
    {
        public static MvcHtmlString Format(this HtmlHelper html, string format, object arg0)
        {
            return new MvcHtmlString(string.Format(format, arg0));
        }
        public static MvcHtmlString Format(this HtmlHelper html, string format, params object[] args)
        {
            return new MvcHtmlString(string.Format(format, args));
        }
        public static MvcHtmlString Format(this HtmlHelper html, string format, object arg0, object arg1)
        {
            return new MvcHtmlString(string.Format(format, arg0, arg1));
        }
        public static MvcHtmlString Format(this HtmlHelper html, string format, object arg0, object arg1, object arg2)
        {
            return new MvcHtmlString(string.Format(format, arg0, arg1, arg2));
        }
    }
    /// <summary>
    /// ResourceFormat 
    /// </summary>
    public static class ResourceFormatExtensions
    {
        public static MvcHtmlString ResourceFormat(this HtmlHelper html, string format, object arg0)
        {
            format = Unity.Mvc3.Resources.GetString(format.TrimStart('<').TrimStart('=').TrimEnd('>'));
            return new MvcHtmlString(string.Format(format, arg0));
        }
        public static MvcHtmlString ResourceFormat(this HtmlHelper html, string format, params object[] args)
        {
            format = Unity.Mvc3.Resources.GetString(format.TrimStart('<').TrimStart('=').TrimEnd('>'));
            return new MvcHtmlString(string.Format(format, args));
        }
        public static MvcHtmlString ResourceFormat(this HtmlHelper html, string format, object arg0, object arg1)
        {
            format = Unity.Mvc3.Resources.GetString(format.TrimStart('<').TrimStart('=').TrimEnd('>'));
            return new MvcHtmlString(string.Format(format, arg0, arg1));
        }
        public static MvcHtmlString ResourceFormat(this HtmlHelper html, string format, object arg0, object arg1, object arg2)
        {
            format = Unity.Mvc3.Resources.GetString(format.TrimStart('<').TrimStart('=').TrimEnd('>'));
            return new MvcHtmlString(string.Format(format, arg0, arg1, arg2));
        }
    }
}

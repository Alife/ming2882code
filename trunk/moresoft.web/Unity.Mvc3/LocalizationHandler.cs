using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Caching;
using System.Xml.Linq;
using System.Web;

namespace Unity.Mvc3
{
    public class LocalizationHandler : Stream
    {

        private Stream responseStream;
        public string Lang = string.Empty;

        public LocalizationHandler(Stream inputStream, string lang)
        {
            responseStream = inputStream;
            Lang = lang;
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
            responseStream.Flush();
        }

        public override long Length
        {
            get { return 0; }
        }

        long postion;
        public override long Position
        {
            get { return postion; }
            set { postion = value; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return responseStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return responseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            responseStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            string sBuffer = System.Text.UTF8Encoding.UTF8.GetString(buffer, offset, count);
            //string pattern = @"(<|<)=(.*?)(>|>)";//正则替换类似页面格式为这样的字符串如：<=OtherContent>
            string pattern = @"(\\u003c|&amp;lt;|&lt;|<)=(.*?)(>|&gt;|&amp;gt;|\\u003e)";
            sBuffer = Regex.Replace(sBuffer, pattern, delegate(Match c)
            {
                return new LoadResources().ReadLocalizationResource(Lang).FirstOrDefault(d => d.Key == c.Groups[2].Value).Value;
            });
            //ReadLocalizationResource();
            byte[] data = System.Text.UTF8Encoding.UTF8.GetBytes(sBuffer);
            responseStream.Write(data, 0, data.Length);
        }
    }
    public class LoadResources
    {
        ObjectCache cache = MemoryCache.Default;
        public string GetString(string item)
        {
            string lang = "zh-CN";
            HttpCookie langCookie = System.Web.HttpContext.Current.Request.Cookies["Lang"];
            if (langCookie != null) lang = langCookie.Value;
            Dictionary<string, string> cacheData = ReadLocalizationResource(lang);
            return cacheData[item];
        }
        public Dictionary<string, string> ReadLocalizationResource(string lang)
        {
            string _XMLPath = string.Empty;
            Dictionary<string, string> cacheData = null;
            if (cacheData != null)
                return cacheData;
            Dictionary<string, string> cachedData = new Dictionary<string, string>();
            string serverPath = System.Web.HttpContext.Current.Server.MapPath("~");
            _XMLPath = Path.Combine(serverPath, string.Format(@"Resources\{0}Resource.xml", lang));
            if (!File.Exists(_XMLPath))
            {
                lang = "zh-CN";
                _XMLPath = Path.Combine(serverPath, string.Format(@"Resources\{0}Resource.xml", "zh-CN"));
            }
            //建立缓存（使用.net4.0最新缓存机制：System.Runtime.Caching;）
            if (cache["myCache-" + lang] == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.SlidingExpiration = TimeSpan.FromMinutes(60);
                policy.ChangeMonitors.Add(new HostFileChangeMonitor(new List<string> { _XMLPath }));

                var items = XElement.Load(_XMLPath).Elements("Module").Elements("item");
                foreach (var item in items)
                {
                    string key = item.Attribute("name").Value;
                    string value = item.Value;
                    cachedData.Add(key, value);
                }
                cache.Set("myCache-" + lang, cachedData, policy);
                return cachedData;
            }
            return (Dictionary<string, string>)cache["myCache-" + lang];
        }
    }
    public class Resources
    {
        public static string GetString(string item)
        {
            return new LoadResources().GetString(item);
        }
    }
}

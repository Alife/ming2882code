using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Common
{
    public abstract class BaseObject
    {
        protected static HttpRequest Request
        {
            get { return HttpContext.Current.Request; }
        }

        protected static Cache Cache
        {
            get { return HttpContext.Current.Cache; }
        }

        protected static bool IsCache
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["IsCache"].ConnectionString == "1" ? true : false; }
        }

        /// <summary>
        /// 增加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        protected static void CacheData(string key, object data)
        {
            if (data != null && IsCache)
            {
                HttpContext.Current.Cache.Insert(key, data, null,
                   DateTime.Now.AddMinutes(10), TimeSpan.Zero);
            }
        }
        /// <summary>
        /// 增加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime">缓存的时间长度</param>
        protected static void CacheData(string key, object data, DateTime cacheTime)
        {
            if (data != null && IsCache)
            {
                HttpContext.Current.Cache.Insert(key, data, null, cacheTime, TimeSpan.Zero);
            }
        }
        /// <summary>
        /// 删除缓存
        /// </summary>
        protected static void CacheRemove(string prefix)
        {
            prefix = prefix.ToLower();
            List<string> itemsToRemove = new List<string>();

            IDictionaryEnumerator enumerator = Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString().ToLower().StartsWith(prefix))
                    itemsToRemove.Add(enumerator.Key.ToString());
            }

            foreach (string itemToRemove in itemsToRemove)
                Cache.Remove(itemToRemove);
        }
        protected static void CacheRemove()
        {
            IDictionaryEnumerator enumerator = Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Cache.Remove(enumerator.Key.ToString());
            }
        }
    }
}

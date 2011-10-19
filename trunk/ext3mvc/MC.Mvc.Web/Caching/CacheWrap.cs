using System;
using System.Web;
using System.Web.Caching;

namespace MC.Mvc.Web.Caching
{
    public class CacheWrap : ICacheWrap
    {
        private readonly Cache cache = HttpRuntime.Cache;

        public object Get(string key)
        {
            return cache.Get(key);
        }

        public void Insert(string key, object value, string fileDependency, double minutes)
        {
            cache.Insert(key, value, new CacheDependency(fileDependency), Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(minutes));
        }

        public void Insert(string key, object value, string[] fileDependencies, double minutes)
        {
            cache.Insert(key, value, new CacheDependency(fileDependencies), Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(minutes));
        }
    }
}

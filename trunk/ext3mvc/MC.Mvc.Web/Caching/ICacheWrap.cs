using System;

namespace MC.Mvc.Web.Caching
{ 
    public interface ICacheWrap
    {
        object Get(string key);
        void Insert(string key, object value, string fileDependency, double minutes);
        void Insert(string key, object value, string[] fileDependencies, double minutes);
    }
}

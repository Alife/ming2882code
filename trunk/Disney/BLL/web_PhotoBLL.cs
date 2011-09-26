namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class web_PhotoBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.web_PhotoData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("web_Photo");
            return num;
        }

        public static web_Photo GetItem(int ID)
        {
            string key = "web_Photo-" + ID;
            if (BaseObject.Cache[key] != null)
                return (web_Photo) BaseObject.Cache[key];
            web_Photo data = DataFactory.web_PhotoData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<web_Photo> GetList(int photoType)
        {
            string key = "web_Photo-all-" + photoType;
            if (BaseObject.Cache[key] != null)
                return (List<web_Photo>)BaseObject.Cache[key];
            List<web_Photo> data = DataFactory.web_PhotoData().GetList(photoType);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(web_Photo item)
        {
            int num = DataFactory.web_PhotoData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("web_Photo");
            return num;
        }

        public static int Update(web_Photo item)
        {
            int num = DataFactory.web_PhotoData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("web_Photo");
            return num;
        }
    }
}

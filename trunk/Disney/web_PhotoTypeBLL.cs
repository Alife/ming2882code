namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class web_PhotoTypeBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.web_PhotoTypeData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("web_PhotoType");
            return num;
        }

        public static web_PhotoType GetItem(int ID)
        {
            string key = "web_PhotoType-" + ID;
            if (BaseObject.Cache[key] != null)
                return (web_PhotoType) BaseObject.Cache[key];
            web_PhotoType data = DataFactory.web_PhotoTypeData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<web_PhotoType> GetList()
        {
            string key = "web_PhotoType-all";
            if (BaseObject.Cache[key] != null)
                return (List<web_PhotoType>)BaseObject.Cache[key];
            List<web_PhotoType> data = DataFactory.web_PhotoTypeData().GetList();
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(web_PhotoType item)
        {
            int num = DataFactory.web_PhotoTypeData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("web_PhotoType");
            return num;
        }

        public static int Update(web_PhotoType item)
        {
            int num = DataFactory.web_PhotoTypeData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("web_PhotoType");
            return num;
        }
    }
}

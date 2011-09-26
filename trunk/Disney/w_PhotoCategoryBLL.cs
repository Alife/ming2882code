namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class w_PhotoCategoryBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.w_PhotoCategoryData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("w_PhotoCategory");
            return num;
        }

        public static w_PhotoCategory GetItem(int ID)
        {
            string key = "w_PhotoCategory-" + ID;
            if (BaseObject.Cache[key] != null)
                return (w_PhotoCategory) BaseObject.Cache[key];
            w_PhotoCategory data = DataFactory.w_PhotoCategoryData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<w_PhotoCategory> GetList()
        {
            string key = "w_PhotoCategory-all";
            if (BaseObject.Cache[key] != null)
                return (List<w_PhotoCategory>)BaseObject.Cache[key];
            List<w_PhotoCategory> data = DataFactory.w_PhotoCategoryData().GetList();
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(w_PhotoCategory item)
        {
            int num = DataFactory.w_PhotoCategoryData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("w_PhotoCategory");
            return num;
        }

        public static int Update(w_PhotoCategory item)
        {
            int num = DataFactory.w_PhotoCategoryData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("w_PhotoCategory");
            return num;
        }
    }
}

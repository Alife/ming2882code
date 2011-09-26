namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class w_PhotoBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.w_PhotoData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("w_Photo");
            return num;
        }

        public static w_Photo GetItem(int ID)
        {
            string key = "w_Photo-" + ID;
            if (BaseObject.Cache[key] != null)
                return (w_Photo) BaseObject.Cache[key];
            w_Photo data = DataFactory.w_PhotoData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static w_PhotoList GetList(int pageIndex, int pageSize)
        {
            string key = string.Format("w_Photo-", pageIndex, pageSize);
            if (BaseObject.Cache[key] != null)
                return (w_PhotoList)BaseObject.Cache[key];
            w_PhotoList data = DataFactory.w_PhotoData().GetList(pageIndex, pageSize);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(w_Photo item)
        {
            int num = DataFactory.w_PhotoData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("w_Photo");
            return num;
        }

        public static int Update(w_Photo item)
        {
            int num = DataFactory.w_PhotoData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("w_Photo");
            return num;
        }
    }
}

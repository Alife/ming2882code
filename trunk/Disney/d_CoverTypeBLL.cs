namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class d_CoverTypeBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_CoverTypeData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_CoverType");
            return num;
        }

        public static d_CoverType GetItem(int ID)
        {
            string key = "d_CoverType-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_CoverType) BaseObject.Cache[key];
            d_CoverType data = DataFactory.d_CoverTypeData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<d_CoverType> GetList()
        {
            string key = "d_CoverType-all";
            if (BaseObject.Cache[key] != null)
                return (List<d_CoverType>)BaseObject.Cache[key];
            List<d_CoverType> data = DataFactory.d_CoverTypeData().GetList();
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(d_CoverType item)
        {
            int num = DataFactory.d_CoverTypeData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_CoverType");
            return num;
        }

        public static int Update(d_CoverType item)
        {
            int num = DataFactory.d_CoverTypeData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_CoverType");
            return num;
        }
    }
}

namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class d_InsideTypeBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_InsideTypeData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_InsideType");
            return num;
        }

        public static d_InsideType GetItem(int ID)
        {
            string key = "d_InsideType-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_InsideType) BaseObject.Cache[key];
            d_InsideType data = DataFactory.d_InsideTypeData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<d_InsideType> GetList()
        {
            string key = "d_InsideType-all";
            if (BaseObject.Cache[key] != null)
                return (List<d_InsideType>)BaseObject.Cache[key];
            List<d_InsideType> data = DataFactory.d_InsideTypeData().GetList();
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(d_InsideType item)
        {
            int num = DataFactory.d_InsideTypeData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_InsideType");
            return num;
        }

        public static int Update(d_InsideType item)
        {
            int num = DataFactory.d_InsideTypeData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_InsideType");
            return num;
        }
    }
}

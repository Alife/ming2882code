namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class d_ClassTypeBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_ClassTypeData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_ClassType");
            return num;
        }

        public static d_ClassType GetItem(int ID)
        {
            string key = "d_ClassType-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_ClassType) BaseObject.Cache[key];
            d_ClassType data = DataFactory.d_ClassTypeData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<d_ClassType> GetList()
        {
            string key = "d_ClassType-all";
            if (BaseObject.Cache[key] != null)
                return (List<d_ClassType>)BaseObject.Cache[key];
            List<d_ClassType> data = DataFactory.d_ClassTypeData().GetList();
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(d_ClassType item)
        {
            int num = DataFactory.d_ClassTypeData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_ClassType");
            return num;
        }

        public static int Update(d_ClassType item)
        {
            int num = DataFactory.d_ClassTypeData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_ClassType");
            return num;
        }
    }
}

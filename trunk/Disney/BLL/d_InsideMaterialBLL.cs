namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class d_InsideMaterialBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_InsideMaterialData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_InsideMaterial");
            return num;
        }

        public static d_InsideMaterial GetItem(int ID)
        {
            string key = "d_InsideMaterial-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_InsideMaterial) BaseObject.Cache[key];
            d_InsideMaterial data = DataFactory.d_InsideMaterialData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<d_InsideMaterial> GetList()
        {
            string key = "d_InsideMaterial-all";
            if (BaseObject.Cache[key] != null)
                return (List<d_InsideMaterial>)BaseObject.Cache[key];
            List<d_InsideMaterial> data = DataFactory.d_InsideMaterialData().GetList();
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(d_InsideMaterial item)
        {
            int num = DataFactory.d_InsideMaterialData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_InsideMaterial");
            return num;
        }

        public static int Update(d_InsideMaterial item)
        {
            int num = DataFactory.d_InsideMaterialData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_InsideMaterial");
            return num;
        }
    }
}

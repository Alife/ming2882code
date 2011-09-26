namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Data;
    using System.Collections.Generic;

    public class d_KitTypeBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_KitTypeData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_KitType");
            return num;
        }

        public static d_KitType GetItem(int ID)
        {
            string key = "d_KitType-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_KitType) BaseObject.Cache[key];
            d_KitType data = DataFactory.d_KitTypeData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<d_KitType> GetList()
        {
            string key = "d_KitType-all";
            if (BaseObject.Cache[key] != null)
                return (List<d_KitType>)BaseObject.Cache[key];
            List<d_KitType> data = DataFactory.d_KitTypeData().GetList();
            BaseObject.CacheData(key, data);
            return data;
        }
        public static DataTable GetDataTable()
        {
            string key = "d_KitType-all-t";
            if (BaseObject.Cache[key] != null)
                return (DataTable)BaseObject.Cache[key];
            DataTable data = DataFactory.d_KitTypeData().GetDataTable();
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(d_KitType item)
        {
            int num = DataFactory.d_KitTypeData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitType");
            return num;
        }

        public static int Update(d_KitType item)
        {
            int num = DataFactory.d_KitTypeData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitType");
            return num;
        }
    }
}

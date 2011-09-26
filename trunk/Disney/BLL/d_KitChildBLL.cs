namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class d_KitChildBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_KitChildData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_KitChild");
            return num;
        }

        public static d_KitChild GetItem(int ID)
        {
            string key = "d_KitChild-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_KitChild) BaseObject.Cache[key];
            d_KitChild data = DataFactory.d_KitChildData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static d_KitChild GetItem(int kitClassID, string code)
        {
            string key = string.Format("d_KitChild-{0}-{1}", kitClassID, code);
            if (BaseObject.Cache[key] != null)
                return (d_KitChild)BaseObject.Cache[key];
            d_KitChild data = DataFactory.d_KitChildData().GetItem(kitClassID, code);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<d_KitChild> GetList(int kitClassID)
        {
            string key = "d_KitChild-all-" + kitClassID;
            if (BaseObject.Cache[key] != null)
                return (List<d_KitChild>)BaseObject.Cache[key];
            List<d_KitChild> data = DataFactory.d_KitChildData().GetList(kitClassID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(d_KitChild item)
        {
            int num = DataFactory.d_KitChildData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitChild");
            return num;
        }
        public static int Insert(List<d_KitChild> list)
        {
            int num = DataFactory.d_KitChildData().Insert(list);
            if (num > 0)
                BaseObject.CacheRemove("d_KitChild");
            return num;
        }

        public static int Update(d_KitChild item)
        {
            int num = DataFactory.d_KitChildData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitChild");
            return num;
        }
    }
}

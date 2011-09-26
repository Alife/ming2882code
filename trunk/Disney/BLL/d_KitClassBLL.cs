namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class d_KitClassBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_KitClassData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_KitClass");
            return num;
        }

        public static d_KitClass GetItem(int ID)
        {
            string key = "d_KitClass-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_KitClass) BaseObject.Cache[key];
            d_KitClass data = DataFactory.d_KitClassData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static d_KitClass GetItem(int kitID, string code)
        {
            string key = string.Format("d_KitClass-{0}-{1}", kitID, code);
            if (BaseObject.Cache[key] != null)
                return (d_KitClass)BaseObject.Cache[key];
            d_KitClass data = DataFactory.d_KitClassData().GetItem(kitID, code);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<d_KitClass> GetList(int kitID)
        {
            string key = "d_KitClass-all-" + kitID;
            if (BaseObject.Cache[key] != null)
                return (List<d_KitClass>)BaseObject.Cache[key];
            List<d_KitClass> data = DataFactory.d_KitClassData().GetList(kitID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(d_KitClass item)
        {
            int num = DataFactory.d_KitClassData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitClass");
            return num;
        }
        public static int Insert(List<d_KitClass> list)
        {
            int num = DataFactory.d_KitClassData().Insert(list);
            if (num > 0)
                BaseObject.CacheRemove("d_KitClass");
            return num;
        }

        public static int Update(d_KitClass item)
        {
            int num = DataFactory.d_KitClassData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitClass");
            return num;
        }
    }
}

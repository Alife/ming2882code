namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class d_KitCostumeBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_KitCostumeData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_KitCostume");
            return num;
        }

        public static d_KitCostume GetItem(int ID)
        {
            string key = "d_KitCostume-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_KitCostume) BaseObject.Cache[key];
            d_KitCostume data = DataFactory.d_KitCostumeData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<d_KitCostume> GetList(int kitChildID)
        {
            string key = "d_KitCostume-all-" + kitChildID;
            if (BaseObject.Cache[key] != null)
                return (List<d_KitCostume>)BaseObject.Cache[key];
            List<d_KitCostume> data = DataFactory.d_KitCostumeData().GetList(kitChildID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(d_KitCostume item)
        {
            int num = DataFactory.d_KitCostumeData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitCostume");
            return num;
        }
        public static int Insert(List<d_KitCostume> list)
        {
            int num = DataFactory.d_KitCostumeData().Insert(list);
            if (num > 0)
                BaseObject.CacheRemove("d_KitCostume");
            return num;
        }

        public static int Update(d_KitCostume item)
        {
            int num = DataFactory.d_KitCostumeData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitCostume");
            return num;
        }
    }
}

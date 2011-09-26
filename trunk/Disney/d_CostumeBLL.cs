namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class d_CostumeBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_CostumeData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_Costume");
            return num;
        }

        public static d_Costume GetItem(int ID)
        {
            string key = "d_Costume-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_Costume) BaseObject.Cache[key];
            d_Costume data = DataFactory.d_CostumeData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<d_Costume> GetList()
        {
            string key = "d_Costume-all";
            if (BaseObject.Cache[key] != null)
                return (List<d_Costume>)BaseObject.Cache[key];
            List<d_Costume> data = DataFactory.d_CostumeData().GetList();
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<d_Costume> GetList(int sex)
        {
            string key = "d_Costume-all-" + sex;
            if (BaseObject.Cache[key] != null)
                return (List<d_Costume>)BaseObject.Cache[key];
            List<d_Costume> data = DataFactory.d_CostumeData().GetList(sex);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static int Insert(d_Costume item)
        {
            int num = DataFactory.d_CostumeData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_Costume");
            return num;
        }

        public static int Update(d_Costume item)
        {
            int num = DataFactory.d_CostumeData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_Costume");
            return num;
        }
    }
}

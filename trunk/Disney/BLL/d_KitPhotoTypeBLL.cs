namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Data;
    using System.Collections.Generic;

    public class d_KitPhotoTypeBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_KitPhotoTypeData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_KitPhotoType");
            return num;
        }

        public static d_KitPhotoType GetItem(int ID)
        {
            string key = "d_KitPhotoType-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_KitPhotoType) BaseObject.Cache[key];
            d_KitPhotoType data = DataFactory.d_KitPhotoTypeData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<d_KitPhotoType> GetList()
        {
            string key = "d_KitPhotoType-all";
            if (BaseObject.Cache[key] != null)
                return (List<d_KitPhotoType>)BaseObject.Cache[key];
            List<d_KitPhotoType> data = DataFactory.d_KitPhotoTypeData().GetList();
            BaseObject.CacheData(key, data);
            return data;
        }
        public static int Insert(d_KitPhotoType item)
        {
            int num = DataFactory.d_KitPhotoTypeData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitPhotoType");
            return num;
        }

        public static int Update(d_KitPhotoType item)
        {
            int num = DataFactory.d_KitPhotoTypeData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitPhotoType");
            return num;
        }
    }
}

namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Data;
    using System.Collections.Generic;

    public class d_KitPhotoBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_KitPhotoData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_KitPhoto");
            return num;
        }

        public static d_KitPhoto GetItem(int ID)
        {
            string key = "d_KitPhoto-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_KitPhoto) BaseObject.Cache[key];
            d_KitPhoto data = DataFactory.d_KitPhotoData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static List<d_KitPhoto> GetList(int kitWorkID)
        {
            string key = string.Format("d_KitPhoto-all-{0}", kitWorkID);
            if (BaseObject.Cache[key] != null)
                return (List<d_KitPhoto>)BaseObject.Cache[key];
            List<d_KitPhoto> data = DataFactory.d_KitPhotoData().GetList(kitWorkID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static DataTable GetList(int pageIndex, int pageSize, ref int records, int totolid, string arter, string beginTime, string endTime)
        {
            string key = string.Format("d_KitPhoto-{0}", pageIndex, pageSize, records, totolid, arter, beginTime, endTime);
            if (BaseObject.Cache[key] != null)
            {
                records = (int)BaseObject.Cache[key + "records"];
                return (DataTable)BaseObject.Cache[key];
            }
            DataTable data = DataFactory.d_KitPhotoData().GetList(pageIndex, pageSize, ref records, totolid, arter, beginTime, endTime);
            BaseObject.CacheData(key + "records", records);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static int GetCount(int kitID)
        {
            return DataFactory.d_KitPhotoData().GetCount(kitID);
        }

        public static int Insert(d_KitPhoto item)
        {
            int num = DataFactory.d_KitPhotoData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitPhoto");
            return num;
        }
        public static int Update(int kitWorkID, int arterID)
        {
            int num = DataFactory.d_KitPhotoData().Update(kitWorkID, arterID);
            if (num > 0)
                BaseObject.CacheRemove("d_KitPhoto");
            return num;
        }
        public static int Update(d_KitPhoto item)
        {
            int num = DataFactory.d_KitPhotoData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitPhoto");
            return num;
        }
    }
}

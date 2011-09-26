namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Data;
    using System.Collections.Generic;

    public class d_KitPhotoReturnBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_KitPhotoReturnData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_KitPhotoReturn");
            return num;
        }

        public static d_KitPhotoReturn GetItem(int ID)
        {
            string key = "d_KitPhotoReturn-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_KitPhotoReturn) BaseObject.Cache[key];
            d_KitPhotoReturn data = DataFactory.d_KitPhotoReturnData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static DataTable GetList(int pageIndex, int pageSize, ref int records, int totolid, string arter, string beginTime, string endTime)
        {
            string key = string.Format("d_KitPhotoReturn-{0}", pageIndex, pageSize, records, totolid, arter, beginTime, endTime);
            if (BaseObject.Cache[key] != null)
            {
                records = (int)BaseObject.Cache[key + "records"];
                return (DataTable)BaseObject.Cache[key];
            }
            DataTable data = DataFactory.d_KitPhotoReturnData().GetList(pageIndex, pageSize, ref records, totolid, arter, beginTime, endTime);
            BaseObject.CacheData(key + "records", records);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static int Insert(d_KitPhotoReturn item)
        {
            int num = DataFactory.d_KitPhotoReturnData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitPhotoReturn");
            return num;
        }
        public static int Insert(List<d_KitPhotoReturn> item)
        {
            int num = DataFactory.d_KitPhotoReturnData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitPhotoReturn");
            return num;
        }

        public static int Update(d_KitPhotoReturn item)
        {
            int num = DataFactory.d_KitPhotoReturnData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitPhotoReturn");
            return num;
        }
    }
}

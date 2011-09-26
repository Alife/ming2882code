namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Data;
    using System.Collections.Generic;

    public class d_ConfirmPhotoBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_ConfirmPhotoData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_ConfirmPhoto");
            return num;
        }

        public static d_ConfirmPhoto GetItem(int ID)
        {
            string key = "d_ConfirmPhoto-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_ConfirmPhoto) BaseObject.Cache[key];
            d_ConfirmPhoto data = DataFactory.d_ConfirmPhotoData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static d_ConfirmPhoto GetItem(int kitWorkID, int kitClassID)
        {
            string key = string.Format("d_KitPhoto-{0}-{1}", kitWorkID, kitClassID);
            if (BaseObject.Cache[key] != null)
                return (d_ConfirmPhoto) BaseObject.Cache[key];
            d_ConfirmPhoto data = DataFactory.d_ConfirmPhotoData().GetItem(kitWorkID, kitClassID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static DataTable GetList(int pageIndex, int pageSize, ref int records, string kitName, string userID)
        {
            string key = string.Format("d_KitPhoto-{0}-{1}-{2}-{3}-{4}",
                pageIndex, pageSize, records, kitName, userID);
            if (BaseObject.Cache[key] != null)
            {
                records = (int)BaseObject.Cache[key + "records"];
                return (DataTable)BaseObject.Cache[key];
            }
            DataTable data = DataFactory.d_ConfirmPhotoData().GetList(pageIndex, pageSize, ref records, kitName, userID);
            BaseObject.CacheData(key + "records", records);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(d_ConfirmPhoto item)
        {
            int num = DataFactory.d_ConfirmPhotoData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_ConfirmPhoto");
            return num;
        }

        public static int Update(d_ConfirmPhoto item)
        {
            int num = DataFactory.d_ConfirmPhotoData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_ConfirmPhoto");
            return num;
        }
    }
}

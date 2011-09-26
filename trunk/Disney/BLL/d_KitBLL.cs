namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Data;
    using System.Collections.Generic;

    public class d_KitBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_KitData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_Kit");
            return num;
        }

        public static d_Kit GetItem(int ID)
        {
            string key = "d_Kit-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_Kit) BaseObject.Cache[key];
            d_Kit data = DataFactory.d_KitData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static d_Kit GetItem(string ID)
        {
            string key = "d_Kit-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_Kit)BaseObject.Cache[key];
            d_Kit data = DataFactory.d_KitData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static DataTable GetList(int pageIndex, int pageSize, ref int records, string keyword,
             string custom, string userID, string state, string beginTime, string endTime)
        {
            string key = string.Format("d_Kit-{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}",
                pageIndex, pageSize, records, keyword, custom, userID, state, beginTime, endTime);
            if (BaseObject.Cache[key] != null)
            {
                records = (int)BaseObject.Cache[key + "records"];
                return (DataTable)BaseObject.Cache[key];
            }
            DataTable data = DataFactory.d_KitData().GetList(pageIndex, pageSize, ref records, keyword, custom, userID, state, beginTime, endTime);
            BaseObject.CacheData(key + "records", records);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(d_Kit item)
        {
            int num = DataFactory.d_KitData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_Kit");
            return num;
        }

        public static int Update(d_Kit item)
        {
            int num = DataFactory.d_KitData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_Kit");
            return num;
        }
    }
}

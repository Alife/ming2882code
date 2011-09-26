namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Data;
    using System.Collections.Generic;

    public class d_ArtistMonthBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_ArtistMonthData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_ArtistMonth");
            return num;
        }

        public static d_ArtistMonth GetItem(int ID)
        {
            string key = "d_ArtistMonth-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_ArtistMonth) BaseObject.Cache[key];
            d_ArtistMonth data = DataFactory.d_ArtistMonthData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<d_ArtistMonth> GetList()
        {
            string key = "d_ArtistMonth-all";
            if (BaseObject.Cache[key] != null)
                return (List<d_ArtistMonth>)BaseObject.Cache[key];
            List<d_ArtistMonth> data = DataFactory.d_ArtistMonthData().GetList();
            BaseObject.CacheData(key, data);
            return data;
        }
        public static DataTable GetList(int pageIndex, int pageSize, ref int records, string arter, string beginTime, string endTime)
        {
            string key = string.Format("d_ArtistMonth-{0}-{1}-{2}-{3}-{4}-{5}", pageIndex, pageSize, records, arter, beginTime, endTime);
            if (BaseObject.Cache[key] != null)
                return (DataTable)BaseObject.Cache[key];
            DataTable data = DataFactory.d_ArtistMonthData().GetList(pageIndex, pageSize, ref records, arter, beginTime, endTime);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(d_ArtistMonth item)
        {
            int num = DataFactory.d_ArtistMonthData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_ArtistMonth");
            return num;
        }

        public static int Insert(List<d_ArtistMonth> list)
        {
            int num = DataFactory.d_ArtistMonthData().Insert(list);
            if (num > 0)
                BaseObject.CacheRemove("d_ArtistMonth");
            return num;
        }
        public static int Update(d_ArtistMonth item)
        {
            int num = DataFactory.d_ArtistMonthData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_ArtistMonth");
            return num;
        }
        public static int Update(List<string> ids)
        {
            int num = DataFactory.d_ArtistMonthData().Update(ids);
            if (num > 0)
                BaseObject.CacheRemove("d_ArtistMonth");
            return num;
        }
    }
}

namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Data;
    using System.Collections.Generic;

    public class d_TotolMonthBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_TotolMonthData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_TotolMonth");
            return num;
        }
        public static d_TotolMonth GetItem()
        {
            string key = "d_TotolMonth";
            if (BaseObject.Cache[key] != null)
                return (d_TotolMonth)BaseObject.Cache[key];
            d_TotolMonth data = DataFactory.d_TotolMonthData().GetItem();
            BaseObject.CacheData(key, data);
            return data;
        }

        public static d_TotolMonth GetItem(int ID)
        {
            string key = "d_TotolMonth-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_TotolMonth) BaseObject.Cache[key];
            d_TotolMonth data = DataFactory.d_TotolMonthData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<d_TotolMonth> GetList()
        {
            string key = "d_TotolMonth-all";
            if (BaseObject.Cache[key] != null)
                return (List<d_TotolMonth>)BaseObject.Cache[key];
            List<d_TotolMonth> data = DataFactory.d_TotolMonthData().GetList();
            BaseObject.CacheData(key, data);
            return data;
        }
        public static DataTable GetList(int pageIndex, int pageSize, ref int records, int arterid, string state, string beginTime, string endTime)
        {
            string key = string.Format("d_TotolMonth-{0}-{1}-{2}-{3}-{4}-{5}-{6}", pageIndex, pageSize, records, arterid, state, beginTime, endTime);
            if (BaseObject.Cache[key] != null)
            {
                records = (int)BaseObject.Cache[key + "records"];
                return (DataTable)BaseObject.Cache[key];
            }
            DataTable data = DataFactory.d_TotolMonthData().GetList(pageIndex, pageSize, ref records, arterid, state, beginTime, endTime);
            BaseObject.CacheData(key + "records", records);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(d_TotolMonth item)
        {
            int num = DataFactory.d_TotolMonthData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_TotolMonth");
            return num;
        }
        public static int Update(d_TotolMonth item)
        {
            int num = DataFactory.d_TotolMonthData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_TotolMonth");
            return num;
        }
    }
}

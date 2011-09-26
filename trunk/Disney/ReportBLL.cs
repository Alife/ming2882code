namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Data;
    using System.Collections.Generic;

    public class ReportBLL : BaseObject
    {
        #region 总的月结
        public static DataTable totolorderdetail(int totolid, string arter, string beginTime, string endTime)
        {
            string key = string.Format("Report-{0}-{1}-{2}-{3}", totolid, arter, beginTime, endTime);
            if (BaseObject.Cache[key] != null)
                return (DataTable)BaseObject.Cache[key];
            DataTable data = DataFactory.ReportData().totolorderdetail(totolid, arter, beginTime, endTime);
            BaseObject.CacheData(key, data);
            return data;
        }
        #endregion
        #region 美工月结
        public static DataTable ArterMonth(int totolid, string arter, string beginTime, string endTime)
        {
            string key = string.Format("Report-ArterMonth-{0}-{1}-{2}-{3}",totolid, arter, beginTime, endTime);
            if (BaseObject.Cache[key] != null)
                return (DataTable)BaseObject.Cache[key];
            DataTable data = DataFactory.ReportData().ArterMonth(totolid, arter, beginTime, endTime);
            BaseObject.CacheData(key, data);
            return data;
        }
        #endregion
        #region 完成明显
        public static DataTable finishtotol(string arter, string beginTime, string endTime)
        {
            string key = string.Format("Report-finishtotol-{0}-{1}-{2}", arter, beginTime, endTime);
            if (BaseObject.Cache[key] != null)
                return (DataTable)BaseObject.Cache[key];
            DataTable data = DataFactory.ReportData().finishtotol(arter, beginTime, endTime);
            BaseObject.CacheData(key, data);
            return data;
        }
        #endregion

        public static int GetMainTotol(string type, string userid)
        {
            string key = string.Format("Report-GetMainTotol-{0}-{1}", type, userid);
            if (BaseObject.Cache[key] != null)
                return (int)BaseObject.Cache[key];
            int data = DataFactory.ReportData().GetMainTotol(type, userid);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static DataTable workordertotol()
        {
            string key = string.Format("Report-workordertotol");
            if (BaseObject.Cache[key] != null)
                return (DataTable)BaseObject.Cache[key];
            DataTable data = DataFactory.ReportData().workordertotol();
            BaseObject.CacheData(key, data);
            return data;
        }
    }
}

namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Data;
    using System.Collections.Generic;

    public class d_KitWorkBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_KitWorkData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_KitWork");
            return num;
        }

        public static d_KitWork GetItem(int ID)
        {
            string key = "d_KitWork-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_KitWork) BaseObject.Cache[key];
            d_KitWork data = DataFactory.d_KitWorkData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static d_KitWork GetItemByFinish(int kitworkid, int userID)
        {
            string key = string.Format("d_KitWork-{0}-{1}", kitworkid, userID);
            if (BaseObject.Cache[key] != null)
                return (d_KitWork)BaseObject.Cache[key];
            d_KitWork data = DataFactory.d_KitWorkData().GetItemByFinish(kitworkid, userID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static DataTable GetList(int pageIndex, int pageSize, ref int records, string keyword, string custom, string arter, string userID, string arterID,
            string state, string proofState, string sendBeginTime, string sendEndTime, string finishBeginTime, string finishEndTime)
        {
            string key = string.Format("d_KitWork-{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}-{11}-{12}-{13}",
                pageIndex, pageSize, records, keyword, state, proofState, custom, arter, userID, arterID, sendBeginTime, sendEndTime, finishBeginTime, finishEndTime);
            if (BaseObject.Cache[key] != null)
            {
                records = (int)BaseObject.Cache[key + "records"];
                return (DataTable)BaseObject.Cache[key];
            }
            DataTable data = DataFactory.d_KitWorkData().GetList(pageIndex, pageSize, ref records, keyword, custom, arter, userID, arterID,
                state, proofState, sendBeginTime, sendEndTime, finishBeginTime, finishEndTime);
            BaseObject.CacheData(key + "records", records);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(d_KitWork item)
        {
            int num = DataFactory.d_KitWorkData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitWork");
            return num;
        }

        public static int Update(d_KitWork item)
        {
            int num = DataFactory.d_KitWorkData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitWork");
            return num;
        }
        public static int Update(List<string> ID, string uploadFile)
        {
            int num = DataFactory.d_KitWorkData().Update(ID, uploadFile);
            if (num > 0)
            {
                BaseObject.CacheRemove("d_KitWork");
                BaseObject.CacheRemove("d_TotolMonth");
            }
            return num;
        }
        public static int Update(List<string> ID, int totolID)
        {
            int num = DataFactory.d_KitWorkData().Update(ID, totolID);
            if (num > 0)
            {
                BaseObject.CacheRemove("d_KitWork");
                BaseObject.CacheRemove("d_TotolMonth");
            }
            return num;
        }
        public static int Update(List<string> ID, string fid, string workname)
        {
            int num = DataFactory.d_KitWorkData().Update(ID, fid, workname);
            if (num > 0)
                BaseObject.CacheRemove("d_KitWork");
            return num;
        }
    }
}

namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Data;
    using System.Collections.Generic;

    public class d_KitQuestionBLL : BaseObject
    {
        public static int Delete(int workID)
        {
            int num = DataFactory.d_KitQuestionData().Delete(workID);
            if (num > 0)
                BaseObject.CacheRemove("d_KitQuestion");
            return num;
        }
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_KitQuestionData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_KitQuestion");
            return num;
        }
        public static int Update(List<string> ID, int state, int kitWorkID)
        {
            int num = DataFactory.d_KitQuestionData().Update(ID, state, kitWorkID);
            if (num > 0)
            {
                BaseObject.CacheRemove("d_KitQuestion");
                BaseObject.CacheRemove("d_KitWork");
            }
            return num;
        }
        public static d_KitQuestion GetItem(int ID)
        {
            string key = string.Format("d_KitQuestion-{0}", ID);
            if (BaseObject.Cache[key] != null)
                return (d_KitQuestion)BaseObject.Cache[key];
            d_KitQuestion data = DataFactory.d_KitQuestionData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static d_KitQuestion GetItem(int kitWorkID, int kitClassID, int? kitChildID, string fileName)
        {
            string key = string.Format("d_KitQuestion-{0}-{1}-{2}-{3}", kitWorkID, kitClassID, kitChildID, fileName);
            if (BaseObject.Cache[key] != null)
                return (d_KitQuestion) BaseObject.Cache[key];
            d_KitQuestion data = DataFactory.d_KitQuestionData().GetItem(kitWorkID, kitClassID, kitChildID, fileName);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static List<d_KitQuestion> GetListByAll(int kitWorkID)
        {
            string key = "d_KitQuestion-allid-" + kitWorkID;
            if (BaseObject.Cache[key] != null)
                return (List<d_KitQuestion>)BaseObject.Cache[key];
            List<d_KitQuestion> data = DataFactory.d_KitQuestionData().GetListByAll(kitWorkID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static DataTable GetList(int kitWorkID)
        {
            string key = "d_KitQuestion-all-" + kitWorkID;
            if (BaseObject.Cache[key] != null)
                return (DataTable)BaseObject.Cache[key];
            DataTable data = DataFactory.d_KitQuestionData().GetList(kitWorkID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(d_KitQuestion item)
        {
            int num = DataFactory.d_KitQuestionData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitQuestion");
            return num;
        }

        public static int Update(d_KitQuestion item)
        {
            int num = DataFactory.d_KitQuestionData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_KitQuestion");
            return num;
        }
    }
}

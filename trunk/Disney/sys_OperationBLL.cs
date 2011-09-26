using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Models;

namespace BLL
{
    public class sys_OperationBLL : BaseObject
    {
        public static int Insert(sys_Operation item)
        {
            int num = DataFactory.sys_OperationData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("hip_sys_Operation");
            return num;
        }
        public static int Update(sys_Operation item)
        {
            int num = DataFactory.sys_OperationData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("hip_sys_Operation");
            return num;
        }
        public static int Update(List<string> ID, List<string> OrderID)
        {
            int num = DataFactory.sys_OperationData().Update(ID, OrderID);
            if (num > 0)
                BaseObject.CacheRemove("hip_sys_Operation");
            return num;
        }
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.sys_OperationData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("hip_sys_Operation");
            return num;
        }
        public static sys_Operation GetItem(int ID)
        {
            string key = "hip_sys_Operation-" + ID;
            sys_Operation data = null;
            if (Cache[key] != null)
                data = (sys_Operation)Cache[key];
            else
            {
                data = DataFactory.sys_OperationData().GetItem(ID);
                CacheData(key, data);
            }
            return data;
        }
        public static List<sys_Operation> GetList(int applicationID)
        {
            string key = string.Format("hip_sys_Operation-list-{0}", applicationID);
            List<sys_Operation> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<sys_Operation>)BaseObject.Cache[key];
            }
            data = DataFactory.sys_OperationData().GetList(applicationID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static List<sys_Operation> GetList(int uid, string appCode)
        {
            string key = string.Format("hip_sys_Operation-list-{0}-{1}", uid, appCode);
            List<sys_Operation> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<sys_Operation>)BaseObject.Cache[key];
            }
            data = DataFactory.sys_OperationData().GetList(uid, appCode);
            BaseObject.CacheData(key, data);
            return data;
        }
    }
}
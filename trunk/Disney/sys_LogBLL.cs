using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Models;

namespace BLL
{
    public class sys_LogBLL : BaseObject
    {
        public static int Insert(sys_Log item)
        {
            int num = DataFactory.sys_LogData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("sys_Log");
            return num;
        }

        public static int Update(sys_Log item)
        {
            int num = DataFactory.sys_LogData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("sys_Log");
            return num;
        }
        public static List<sys_Log> GetList()
        {
            string key = string.Format("sys_Log-all");
            List<sys_Log> data = null;
            if (BaseObject.Cache[key] != null)
                data = (List<sys_Log>)BaseObject.Cache[key];
            else
            {
                data = DataFactory.sys_LogData().GetList();
                BaseObject.CacheData(key, data);
            }
            return data;
        }
        public static sys_LogList GetList(int pageIndex, int pageSize, int categoryid, int opid, string usercode, string objcode, DateTime? startDate, DateTime? endDate)
        {
            string key = string.Format("sys_Log-{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", pageIndex, pageSize, categoryid, opid, usercode, objcode, startDate, endDate);
            sys_LogList data = null;
            if (BaseObject.Cache[key] != null)
                data = (sys_LogList)BaseObject.Cache[key];
            else
            {
                data = DataFactory.sys_LogData().GetList(pageIndex, pageSize, categoryid, opid, usercode, objcode, startDate, endDate);
                BaseObject.CacheData(key, data);
            }
            return data;
        }

        public static int Delete(List<string> ID)
        {
            int num = DataFactory.sys_LogData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("sys_Log");
            return num;
        }

        public static sys_Log GetItem(int id)
        {
            string key = "sys_Log-" + id;
            sys_Log data = null;
            if (Cache[key] != null)
                data = (sys_Log)Cache[key];
            else
            {
                data = DataFactory.sys_LogData().GetItem(id);
                CacheData(key, data);
            }
            return data;
        }
    }
}

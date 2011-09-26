using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Models;

namespace BLL
{
    public class sys_LogCategoryBLL : BaseObject
    {
        public static int Insert(sys_LogCategory item)
        {
            int num = DataFactory.sys_LogCategoryData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("sys_LogCategory");
            return num;
        }

        public static int Update(sys_LogCategory item)
        {
            int num = DataFactory.sys_LogCategoryData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("sys_LogCategory");
            return num;
        }
        public static List<sys_LogCategory> GetList()
        {
            string key = string.Format("sys_LogCategory-all");
            List<sys_LogCategory> data = null;
            if (BaseObject.Cache[key] != null)
                data = (List<sys_LogCategory>)BaseObject.Cache[key];
            else
            {
                data = DataFactory.sys_LogCategoryData().GetList();
                BaseObject.CacheData(key, data);
            }
            return data;
        }

        public static int Delete(List<string> ID)
        {
            int num = DataFactory.sys_LogCategoryData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("sys_LogCategory");
            return num;
        }

        public static sys_LogCategory GetItem(int id)
        {
            string key = "sys_LogCategory-" + id;
            sys_LogCategory data = null;
            if (Cache[key] != null)
                data = (sys_LogCategory)Cache[key];
            else
            {
                data = DataFactory.sys_LogCategoryData().GetItem(id);
                CacheData(key, data);
            }
            return data;
        }
    }
}

namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class sys_LinkBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.sys_LinkData().Delete(ID);
            if (num > 0)
            {
                BaseObject.CacheRemove("sys_Link");
            }
            return num;
        }

        public static sys_Link GetItem(int ID)
        {
            string key = "sys_Link-" + ID;
            sys_Link data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (sys_Link) BaseObject.Cache[key];
            }
            data = DataFactory.sys_LinkData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<sys_Link> GetList(int num)
        {
            string key = "sys_Link-" + num;
            List<sys_Link> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<sys_Link>) BaseObject.Cache[key];
            }
            data = DataFactory.sys_LinkData().GetList(num);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(sys_Link item)
        {
            int num = DataFactory.sys_LinkData().Insert(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("sys_Link");
            }
            return num;
        }

        public static int Update(sys_Link item)
        {
            int num = DataFactory.sys_LinkData().Update(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("sys_Link");
            }
            return num;
        }

        public static int Update(List<string> ID, List<string> OrderID)
        {
            int num = DataFactory.sys_LinkData().Update(ID, OrderID);
            if (num > 0)
            {
                BaseObject.CacheRemove("sys_Link");
            }
            return num;
        }
    }
}

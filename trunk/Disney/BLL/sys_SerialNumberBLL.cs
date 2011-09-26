using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Models;

namespace BLL
{
    public class sys_SerialNumberBLL : BaseObject
    {
        public static int Insert(sys_SerialNumber item)
        {
            int num = DataFactory.sys_SerialNumberData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("sys_SerialNumber");
            return num;
        }

        public static int Delete(List<string> ID)
        {
            int num = DataFactory.sys_SerialNumberData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("sys_SerialNumber");
            return num;
        }

        public static sys_SerialNumber GetItem(int flag)
        {
            string key = "sys_SerialNumber-" + flag;
            sys_SerialNumber data = null;
            if (Cache[key] != null)
                data = (sys_SerialNumber)Cache[key];
            else
            {
                data = DataFactory.sys_SerialNumberData().GetItem(flag);
                CacheData(key, data);
            }
            return data;
        }

        public static int Update(sys_SerialNumber item)
        {
            int num = DataFactory.sys_SerialNumberData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("sys_SerialNumber");
            return num;
        }
    }
}

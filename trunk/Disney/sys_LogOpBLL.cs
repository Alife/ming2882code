using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Models;

namespace BLL
{
    public class sys_LogOpBLL : BaseObject
    {
        public static int Insert(sys_LogOp item)
        {
            int num = DataFactory.sys_LogOpData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("sys_LogOp");
            return num;
        }

        public static int Update(sys_LogOp item)
        {
            int num = DataFactory.sys_LogOpData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("sys_LogOp");
            return num;
        }
        public static List<sys_LogOp> GetList(int categoryid)
        {
            string key = string.Format("sys_LogOp-all-{0}", categoryid);
            List<sys_LogOp> data = null;
            if (BaseObject.Cache[key] != null)
                data = (List<sys_LogOp>)BaseObject.Cache[key];
            else
            {
                data = DataFactory.sys_LogOpData().GetList(categoryid);
                BaseObject.CacheData(key, data);
            }
            return data;
        }

        public static int Delete(List<string> ID)
        {
            int num = DataFactory.sys_LogOpData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("sys_LogOp");
            return num;
        }

        public static sys_LogOp GetItem(int id)
        {
            string key = "sys_LogOp-" + id;
            sys_LogOp data = null;
            if (Cache[key] != null)
                data = (sys_LogOp)Cache[key];
            else
            {
                data = DataFactory.sys_LogOpData().GetItem(id);
                CacheData(key, data);
            }
            return data;
        }
        public static sys_LogOp GetItem(string opcode)
        {
            string key = string.Format("sys_LogOp-{0}", opcode);
            sys_LogOp data = null;
            if (Cache[key] != null)
                data = (sys_LogOp)Cache[key];
            else
            {
                data = DataFactory.sys_LogOpData().GetItem(opcode);
                CacheData(key, data);
            }
            return data;
        }
    }
}

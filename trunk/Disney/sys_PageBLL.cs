using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Models;

namespace BLL
{
    public class sys_PageBLL: BaseObject
    {
        public static int Exists(string _Code)
        {
            return DataFactory.PageData().Exists(_Code);
        }
        public static int Insert(sys_Page item)
        {
            int num = DataFactory.PageData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("hip_sys_Page");
            return num;
        }
        public static int Update(sys_Page item)
        {
            int num = DataFactory.PageData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("hip_sys_Page");
            return num;
        }
        public static int Update(List<string> ID, List<string> OrderID)
        {
            int num = DataFactory.PageData().Update(ID, OrderID);
            if (num > 0)
                BaseObject.CacheRemove("hip_sys_Page");
            return num;
        }
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.PageData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("hip_sys_Page");
            return num;
        }
        public static sys_Page GetItem(int ID)
        {
            string key = string.Format("hip_sys_Page-{0}-{1}", ID, 0);
            sys_Page data = null;
            if (Cache[key] != null)
                data = (sys_Page)Cache[key];
            else
            {
                data = DataFactory.PageData().GetItem(ID.ToString(), 0);
                CacheData(key, data);
            }
            return data;
        }
        public static sys_Page GetItem(string code)
        {
            string key = string.Format("hip_sys_Page-{0}-{1}", code, 1);
            sys_Page data = null;
            if (Cache[key] != null)
                data = (sys_Page)Cache[key];
            else
            {
                data = DataFactory.PageData().GetItem(code, 1);
                CacheData(key, data);
            }
            return data;
        }
        public static List<sys_Page> GetList()
        {
            string key = "hip_sys_Page-list-";
            List<sys_Page> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<sys_Page>)BaseObject.Cache[key];
            }
            data = DataFactory.PageData().GetList();
            BaseObject.CacheData(key, data);
            return data;
        }
        public static List<sys_Page> GetList(int parentID)
        {
            string key = string.Format("hip_sys_Page-list-{0}", parentID);
            List<sys_Page> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<sys_Page>)BaseObject.Cache[key];
            }
            data = DataFactory.PageData().GetList(parentID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static List<sys_Page> GetListByChild(int parentID)
        {
            string key = string.Format("hip_sys_Page-listchild-{0}", parentID);
            List<sys_Page> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<sys_Page>)BaseObject.Cache[key];
            }
            data = DataFactory.PageData().GetListByChild(parentID);
            BaseObject.CacheData(key, data);
            return data;
        }
    }
}

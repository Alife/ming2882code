using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Models;

namespace BLL
{
    public class sys_RoleBLL : BaseObject
    {
        public static int Insert(sys_Role item)
        {
            int num = DataFactory.sys_RoleData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("hip_sys_Role");
            return num;
        }
        public static int Update(sys_Role item)
        {
            int num = DataFactory.sys_RoleData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("hip_sys_Role");
            return num;
        }
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.sys_RoleData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("hip_sys_Role");
            return num;
        }
        public static sys_Role GetItem(int ID)
        {
            string key = "hip_sys_Role-" + ID;
            sys_Role data = null;
            if (Cache[key] != null)
                data = (sys_Role)Cache[key];
            else
            {
                data = DataFactory.sys_RoleData().GetItem(ID);
                CacheData(key, data);
            }
            return data;
        }
        public static List<sys_Role> GetList()
        {
            string key = "hip_sys_Role-list-";
            List<sys_Role> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<sys_Role>)BaseObject.Cache[key];
            }
            data = DataFactory.sys_RoleData().GetList();
            BaseObject.CacheData(key, data);
            return data;
        }
    }
}

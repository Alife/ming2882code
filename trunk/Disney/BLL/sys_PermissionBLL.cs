using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Models;

namespace BLL
{
    public class sys_PermissionBLL : BaseObject
    {
        public static int Save(List<sys_Permission> list)
        {
            int num = DataFactory.sys_PermissionData().Save(list);
            if (num > 0)
                BaseObject.CacheRemove("hip_sys_Permission");
            return num;
        }
        public static sys_Permission GetItem(int ID)
        {
            string key = "hip_sys_Permission-" + ID;
            sys_Permission data = null;
            if (Cache[key] != null)
                data = (sys_Permission)Cache[key];
            else
            {
                data = DataFactory.sys_PermissionData().GetItem(ID);
                CacheData(key, data);
            }
            return data;
        }
        public static List<sys_Permission> GetList(int _roleID, int _operationID)
        {
            string key = string.Format("hip_sys_Permission-list-{0}-{1}", _roleID, _operationID);
            List<sys_Permission> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<sys_Permission>)BaseObject.Cache[key];
            }
            data = DataFactory.sys_PermissionData().GetList(_roleID, _operationID);
            BaseObject.CacheData(key, data);
            return data;
        }
    }
}
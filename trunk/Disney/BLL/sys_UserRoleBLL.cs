using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Models;

namespace BLL
{
    public class sys_UserRoleBLL : BaseObject
    {
        public static int Save(List<sys_UserRole> list)
        {
            int num = DataFactory.sys_UserRoleData().Save(list);
            if (num > 0)
                BaseObject.CacheRemove("hip_sys_UserRole");
            return num;
        }
        public static List<sys_UserRole> GetList(int _userID)
        {
            string key = "hip_sys_UserRole-list-" + _userID;
            List<sys_UserRole> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<sys_UserRole>)BaseObject.Cache[key];
            }
            data = DataFactory.sys_UserRoleData().GetList(_userID);
            BaseObject.CacheData(key, data);
            return data;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Models;

namespace BLL
{
    public class sys_PermissionFieldBLL : BaseObject
    {
        public static int Save(List<sys_PermissionField> list)
        {
            int num = DataFactory.sys_PermissionFieldData().Save(list);
            if (num > 0)
                BaseObject.CacheRemove("hip_sys_PermissionField");
            return num;
        }
        public static List<sys_PermissionField> GetList(int _permissionID)
        {
            string key = string.Format("hip_sys_PermissionField-list-{0}", _permissionID);
            List<sys_PermissionField> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<sys_PermissionField>)BaseObject.Cache[key];
            }
            data = DataFactory.sys_PermissionFieldData().GetList(_permissionID);
            BaseObject.CacheData(key, data);
            return data;
        }
    }
}
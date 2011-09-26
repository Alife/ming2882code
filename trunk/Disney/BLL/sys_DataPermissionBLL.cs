using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Models;
using Models.Enums;

namespace BLL
{
    public class sys_DataPermissionBLL : BaseObject
    {
        public static int Insert(sys_DataPermission model)
        {
            int num = DataFactory.sys_DataPermissionData().Insert(model);
            if (num > 0)
                BaseObject.CacheRemove("sys_DataPermission");
            return num;
        }
        public static int Update(sys_DataPermission model)
        {
            int num = DataFactory.sys_DataPermissionData().Update(model);
            if (num > 0)
                BaseObject.CacheRemove("sys_DataPermission");
            return num;
        }
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.sys_DataPermissionData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("sys_DataPermission");
            return num;
        }
        public static sys_DataPermission GetItem(int ID)
        {
            string key = "sys_DataPermission-" + ID;
            sys_DataPermission data = null;
            if (Cache[key] != null)
                data = (sys_DataPermission)Cache[key];
            else
            {
                data = DataFactory.sys_DataPermissionData().GetItem(ID);
                CacheData(key, data);
            }
            return data;
        }
        public static sys_DataPermission GetItem(int uid, ResourceType type)
        {
            string key = string.Format("sys_DataPermission-{0}-{1}", uid, type);
            sys_DataPermission data = null;
            if (Cache[key] != null)
                data = (sys_DataPermission)Cache[key];
            else
            {
                data = DataFactory.sys_DataPermissionData().GetItem(uid, type);
                CacheData(key, data);
            }
            return data;
        }
        public static List<sys_DataPermission> GetList(int roleID)
        {
            string key = string.Format("sys_DataPermission-list-{0}", roleID);
            List<sys_DataPermission> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<sys_DataPermission>)BaseObject.Cache[key];
            }
            data = DataFactory.sys_DataPermissionData().GetList(roleID);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static string GetList(int uid, ResourceType type)
        {
            string key = string.Format("sys_DataPermission-list-{0}-{1}", uid, type);
            string data = null;
            if (BaseObject.Cache[key] != null)
                return (string)BaseObject.Cache[key];
            data = DataFactory.sys_DataPermissionData().GetList(uid, type);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static List<sys_DataPermission> GetLists(int uid, ResourceType type)
        {
            string key = string.Format("sys_DataPermission-lists-{0}-{1}", uid, type);
            List<sys_DataPermission> data = null;
            if (BaseObject.Cache[key] != null)
                return (List<sys_DataPermission>)BaseObject.Cache[key];
            data = DataFactory.sys_DataPermissionData().GetLists(uid, type);
            BaseObject.CacheData(key, data);
            return data;
        }
    }
}
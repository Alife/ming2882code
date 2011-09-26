using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Models;

namespace BLL
{
    public class sys_ApplicationBLL: BaseObject
    {
        public static int Exists(string _Code)
        {
            return DataFactory.sys_ApplicationData().Exists(_Code);
        }
        public static int Insert(sys_Application item)
        {
            int num = DataFactory.sys_ApplicationData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("hip_sys_Application");
            return num;
        }
        public static int Update(sys_Application item)
        {
            int num = DataFactory.sys_ApplicationData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("hip_sys_Application");
            return num;
        }
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.sys_ApplicationData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("hip_sys_Application");
            return num;
        }
        public static sys_Application GetItem(int ID)
        {
            string key = "hip_sys_Application-" + ID;
            sys_Application data = null;
            if (Cache[key] != null)
                data = (sys_Application)Cache[key];
            else
            {
                data = DataFactory.sys_ApplicationData().GetItem(ID);
                CacheData(key, data);
            }
            return data;
        }
        /// <summary>
        /// _id:0全部
        /// _type:1,所有子类,不包含自己;2包含自己的所有子类;3不包含自己所有父类4;包含自己所有父类
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_type"></param>
        /// <returns></returns>
        public static List<sys_Application> GetList(int _id, int _type)
        {
            string key = string.Format("hip_sys_Application-{0}-{1}", _id, _type);
            List<sys_Application> list = null;
            if (Cache[key] != null)
                list = (List<sys_Application>)Cache[key];
            else
            {
                list = DataFactory.sys_ApplicationData().GetList(_id, _type);
                CacheData(key, list);
            }
            return list;
        }
        public static List<sys_Application> GetUserList(int uid, int parentID)
        {
            string key = string.Format("hip_sys_Application-User-{0}-{1}", uid, parentID);
            List<sys_Application> list = null;
            if (Cache[key] != null)
                list = (List<sys_Application>)Cache[key];
            else
            {
                list = DataFactory.sys_ApplicationData().GetUserList(uid, parentID);
                CacheData(key, list);
            }
            return list;
        }
    }
}

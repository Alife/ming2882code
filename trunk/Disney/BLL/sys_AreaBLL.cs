using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using Common;

namespace BLL
{
    public class sys_AreaBLL : BaseObject
    {
        /// <summary>
        /// _id:0全部
        /// _type:1,所有子类,不包含自己;2包含自己的所有子类;3不包含自己所有父类4;包含自己所有父类
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_type"></param>
        /// <returns></returns>
        public static List<sys_Area> GetList(int _id, int _type)
        {
            string key = string.Format("sys_Area-{0}-{1}", _id, _type);
            List<sys_Area> list = null;
            if (Cache[key] != null)
                list = (List<sys_Area>)Cache[key];
            else
            {
                list = DataFactory.sys_AreaData().GetList(_id, _type);
                CacheData(key, list);
            }
            return list;
        }
        public static List<sys_Area> GetList(int parentID)
        {
            string key = string.Format("sys_Area-{0}", parentID);
            List<sys_Area> list = null;
            if (Cache[key] != null)
                list = (List<sys_Area>)Cache[key];
            else
            {
                list = DataFactory.sys_AreaData().GetList(parentID);
                CacheData(key, list);
            }
            return list;
        }
        public static sys_Area GetItem(int _id)
        {
            string key = "sys_Area-" + _id;
            sys_Area item = null;
            if (Cache[key] != null)
                item = (sys_Area)Cache[key];
            else
            {
                item = DataFactory.sys_AreaData().GetItem(_id);
                CacheData(key, item);
            }
            return item;
        }
        public static int Insert(sys_Area item)
        {
            int num = DataFactory.sys_AreaData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("sys_Area");
            return num;
        }
        public static int Update(sys_Area item)
        {
            int num = DataFactory.sys_AreaData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("sys_Area");
            return num;
        }
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.sys_AreaData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("sys_Area");
            return num;
        }
        public static string GetArea(int cityID)
        {
            sys_Area item = GetItem(cityID);
            string province = GetItem(item.ParentID).Name;
            string citya = item.Name;
            if (province == citya)
                return citya;
            return province + citya;
        }
    }
}

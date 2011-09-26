namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class d_DepartmentBLL : BaseObject
    {
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.d_DepartmentData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("d_Department");
            return num;
        }

        public static d_Department GetItem(int ID)
        {
            string key = "d_Department-" + ID;
            if (BaseObject.Cache[key] != null)
                return (d_Department) BaseObject.Cache[key];
            d_Department data = DataFactory.d_DepartmentData().GetItem(ID);
            BaseObject.CacheData(key, data);
            return data;
        }
        /// <summary>
        /// _id:0全部
        /// _type:1,所有子类,不包含自己;2包含自己的所有子类;3不包含自己所有父类4;包含自己所有父类
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_type"></param>
        /// <returns></returns>
        public static List<d_Department> GetList(int _id, int _type)
        {
            string key = string.Format("d_Department-{0}-{1}", _id, _type);
            if (BaseObject.Cache[key] != null)
                return (List<d_Department>)BaseObject.Cache[key];
            List<d_Department> data = DataFactory.d_DepartmentData().GetList(_id, _type);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static int Insert(d_Department item)
        {
            int num = DataFactory.d_DepartmentData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("d_Department");
            return num;
        }

        public static int Update(d_Department item)
        {
            int num = DataFactory.d_DepartmentData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("d_Department");
            return num;
        }
    }
}

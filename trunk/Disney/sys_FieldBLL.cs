using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Models;

namespace BLL
{
    public class sys_FieldBLL : BaseObject
    {
        public static int Insert(sys_Field item)
        {
            int num = DataFactory.sys_FieldData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("hip_sys_Field");
            return num;
        }
        public static int Update(sys_Field item)
        {
            int num = DataFactory.sys_FieldData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("hip_sys_Field");
            return num;
        }
        public static int Delete(List<string> ID)
        {
            int num = DataFactory.sys_FieldData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("hip_sys_Field");
            return num;
        }
        public static sys_Field GetItem(int ID)
        {
            string key = "hip_sys_Field-" + ID;
            sys_Field data = null;
            if (Cache[key] != null)
                data = (sys_Field)Cache[key];
            else
            {
                data = DataFactory.sys_FieldData().GetItem(ID);
                CacheData(key, data);
            }
            return data;
        }
        public static List<sys_Field> GetList(int operationID)
        {
            string key = "hip_sys_Field-list-" + operationID;
            List<sys_Field> data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (List<sys_Field>)BaseObject.Cache[key];
            }
            data = DataFactory.sys_FieldData().GetList(operationID);
            BaseObject.CacheData(key, data);
            return data;
        }
    }
}

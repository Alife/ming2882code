using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Models;

namespace BLL
{
    public class t_UserPointBLL : BaseObject
    {
        public static int Insert(t_UserPoint item)
        {
            int num = DataFactory.t_UserPointData().Insert(item);
            if (num > 0)
                BaseObject.CacheRemove("t_UserPoint");
            return num;
        }
        public static int Insert(List<t_UserPoint> list)
        {
            int num = DataFactory.t_UserPointData().Insert(list);
            if (num > 0)
                BaseObject.CacheRemove("t_UserPoint");
            return num;
        }

        public static int Update(t_UserPoint item)
        {
            int num = DataFactory.t_UserPointData().Update(item);
            if (num > 0)
                BaseObject.CacheRemove("t_UserPoint");
            return num;
        }

        public static int Update(int orderid)
        {
            int num = DataFactory.t_UserPointData().Update(orderid);
            if (num > 0)
                BaseObject.CacheRemove("t_UserPoint");
            return num;
        }
        public static t_UserPoint GetItem(int id)
        {
            string key = "t_UserPoint-" + id;
            t_UserPoint data = null;
            if (Cache[key] != null)
                data = (t_UserPoint)Cache[key];
            else
            {
                data = DataFactory.t_UserPointData().GetItem(id);
                CacheData(key, data);
            }
            return data;
        }

        public static int GetUserPoint(int uid)
        {
            string key = "t_UserPoint-p-" + uid;
            int data = 0;
            if (Cache[key] != null)
                data = (int)Cache[key];
            else
            {
                data = DataFactory.t_UserPointData().GetUserPoint(uid);
                CacheData(key, data);
            }
            return data;
        }
        public static t_UserPointList GetList(int pageIndex, int pageSize, string trueName, string userCode)
        {
            string key = string.Format("t_UserPoint-{0}-{1}-{2}-{3}", pageIndex, pageSize, trueName, userCode);
            t_UserPointList data = null;
            if (BaseObject.Cache[key] != null)
                data = (t_UserPointList)BaseObject.Cache[key];
            else
            {
                data = DataFactory.t_UserPointData().GetList(pageIndex, pageSize, trueName, userCode);
                BaseObject.CacheData(key, data);
            }
            return data;
        }

        public static int Delete(List<string> ID)
        {
            int num = DataFactory.t_UserPointData().Delete(ID);
            if (num > 0)
                BaseObject.CacheRemove("t_UserPoint");
            return num;
        }
        public static int Delete(List<int> orderid)
        {
            int num = DataFactory.t_UserPointData().Delete(orderid);
            if (num > 0)
                BaseObject.CacheRemove("t_UserPoint");
            return num;
        }
    }
}

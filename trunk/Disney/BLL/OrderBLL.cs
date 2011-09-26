namespace BLL
{
    using Common;
    using Models;
    using System;
    using System.Collections.Generic;

    public class OrderBLL : BaseObject
    {
        public static int Delete(int ID)
        {
            int num = DataFactory.OrderData().Delete(ID);
            if (num > 0)
            {
                BaseObject.CacheRemove("Order");
            }
            return num;
        }
        public static int DeleteOrderList(int ID)
        {
            int num = DataFactory.OrderData().DeleteOrderList(ID);
            if (num > 0)
            {
                BaseObject.CacheRemove("Order");
            }
            return num;
        }

        public static Order GetItem(int _id, int _uid)
        {
            string key = string.Format("Order-{0}-{1}", _id, _uid);
            Order data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (Order) BaseObject.Cache[key];
            }
            data = DataFactory.OrderData().GetItem(_id, _uid);
            BaseObject.CacheData(key, data);
            return data;
        }

        public static List<OrderList> GetList(int orderID)
        {
            return DataFactory.OrderData().GetList(orderID);
        }

        public static OrderLists GetList(int _userID, int _status, int _pageIndex, int _pageSize)
        {
            string key = string.Format("Order-{0}-{1}-{2}-{3}", new object[] { _userID, _status, _pageIndex, _pageSize });
            OrderLists data = null;
            if (BaseObject.Cache[key] != null)
            {
                return (OrderLists) BaseObject.Cache[key];
            }
            data = DataFactory.OrderData().GetList(_userID, _status, _pageIndex, _pageSize);
            BaseObject.CacheData(key, data);
            return data;
        }
        public static int Insert(Order model, List<OrderList> list)
        {
            int num = DataFactory.OrderData().Insert(model, list);
            if (num > 0)
                BaseObject.CacheRemove("Order");
            return num;
        }
        public static int Insert(Order item)
        {
            int num = DataFactory.OrderData().Insert(item);
            if (num > 0)
            {
                BaseObject.CacheRemove("Order");
                BaseObject.CacheRemove("Cart");
            }
            return num;
        }
        public static int Update(Order model, List<OrderList> list)
        {
            int num = DataFactory.OrderData().Update(model, list);
            if (num > 0)
                BaseObject.CacheRemove("Order");
            return num;
        }
        public static int Update(int ID, int status)
        {
            int num = DataFactory.OrderData().Update(ID, status);
            if (num > 0)
            {
                BaseObject.CacheRemove("Order");
            }
            return num;
        }
    }
}

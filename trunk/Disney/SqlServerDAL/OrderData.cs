namespace SqlServerDAL
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class OrderData : DALHelper
    {
        public int Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete d_OrderList where OrderID=@ID; ");
            builder.Append("delete d_Order where ID=@ID; ");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, ID) };
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), cmdParms);
        }
        public int DeleteOrderList(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete d_OrderList where ID=@ID; ");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, ID) };
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), cmdParms);
        }

        public Order GetItem(int _id, int _uid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,OrderCode,UserID,Price,OrderTime,Status,TrueName,CityID,Address,Mobile,Tel,Zip,ShippingID from d_Order ");
            builder.Append(" where ID=@ID");
            if (_uid > 0)
                builder.Append(" and UserID=@UserID");
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, _id), 
                DALHelper.DBHelper.CreateInDbParameter("@UserID", DbType.Int32, 4, _uid) };
            Order order = null;
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            order = GetItem(reader, new Order());
                        }
                    }
                }
                finally
                {
                    if (reader  != null && !reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
            }
            return order;
        }

        public List<OrderList> GetList(int orderID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select a.ID,OrderID,OrderProductID,Quantity,a.Price,OrderTime,ProductID,ProductName,ProductCode,FilePath from d_OrderList as a ");
            builder.Append(" left join d_OrderProduct as b on a.OrderProductID=b.ID");
            builder.Append(" where OrderID=@OrderID Order By a.ID Desc");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@OrderID", DbType.Int32, 4, orderID) };
            List<OrderList> list = new List<OrderList>();
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            OrderList list2 = new OrderList();
                            list2.ID = reader.GetInt32(0);
                            list2.OrderID = reader.GetInt32(1);
                            list2.OrderProductID = reader.GetInt32(2);
                            list2.Quantity = reader.GetInt32(3);
                            list2.Price = reader.GetDecimal(4);
                            list2.OrderTime = reader.GetDateTime(5);
                            list2.OrderProduct.ProductID = reader.GetInt32(6);
                            list2.OrderProduct.ProductName = reader.GetString(7);
                            list2.OrderProduct.ProductCode = reader.GetString(8);
                            list2.OrderProduct.FilePath = reader.GetString(9);
                            list.Add(list2);
                        }
                    }
                }
                finally
                {
                    if (reader  != null && !reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
            }
            return list;
        }

        public OrderLists GetList(int _userID, int _Status, int _pageIndex, int _pageSize)
        {
            OrderLists lists = new OrderLists();
            DbConnection connectionString = DALHelper.DBHelper.CreateConnection();
            try
            {
                string str = string.Empty;
                string str2 = "order by ID desc";
                List<DbParameter> list = new List<DbParameter>();
                list.Add(DALHelper.DBHelper.CreateInDbParameter("@PageSize", DbType.Int32, _pageSize));
                list.Add(DALHelper.DBHelper.CreateInDbParameter("@PageIndex", DbType.Int32, _pageIndex));
                if (_userID > 0)
                {
                    str = str + "and UserID=@UserID ";
                    list.Add(DALHelper.DBHelper.CreateInDbParameter("@UserID", DbType.Int32, _userID));
                }
                if (_Status > 0)
                {
                    str = str + "and Status=@Status ";
                    list.Add(DALHelper.DBHelper.CreateInDbParameter("@Status", DbType.Int32, _Status));
                }
                DbParameter[] cmdParms = list.ToArray();
                string cmdText = "SELECT COUNT(ID) FROM d_Order where 1=1 " + str;
                object obj2 = DALHelper.DBHelper.ExecuteScalar(connectionString, CommandType.Text, cmdText, cmdParms);
                if (obj2 != null)
                {
                    lists.RecordNumber = int.Parse(obj2.ToString());
                }
                else
                {
                    return lists;
                }
                if (lists.RecordNumber != 0)
                {
                    cmdText = string.Format("SELECT ID,OrderCode,UserID,Price,OrderTime,Status,TrueName,CityID,Address,Mobile,Tel,Zip,ShippingID FROM " +
                        " (select ID,OrderCode,UserID,Price,OrderTime,Status,TrueName,CityID,Address,Mobile,Tel,Zip,ShippingID,ROW_NUMBER() Over({0}) as rowNum from d_Order " +
                        " where 1=1 {1}) as temptable WHERE rowNum BETWEEN ((@PageIndex-1)*@PageSize+1) and (@PageIndex)*@PageSize", str2, str);
                    DbDataReader reader = DALHelper.DBHelper.ExecuteReader(connectionString, CommandType.Text, cmdText, cmdParms);
                    while (reader.Read())
                        lists.Add(GetItem(reader, new Order()));
                }
            }
            finally
            {
                connectionString.Close();
                connectionString.Dispose();
            }
            return lists;
        }

        private Order GetItem(DbDataReader dr, Order item)
        {
            item.ID = DALHelper.DBHelper.GetInt(dr["ID"]);
            item.OrderCode = DALHelper.DBHelper.GetString(dr["OrderCode"]);
            item.UserID = DALHelper.DBHelper.GetInt(dr["UserID"]);
            item.Price = DALHelper.DBHelper.GetDecimal(dr["Price"]);
            item.OrderTime = DALHelper.DBHelper.GetDateTime(dr["OrderTime"]);
            item.Status = DALHelper.DBHelper.GetInt(dr["Status"]);
            item.TrueName = DALHelper.DBHelper.GetString(dr["TrueName"]);
            item.CityID = DALHelper.DBHelper.GetInt(dr["CityID"]);
            item.Address = DALHelper.DBHelper.GetString(dr["Address"]);
            item.Mobile = DALHelper.DBHelper.GetString(dr["Mobile"]);
            item.Tel = DALHelper.DBHelper.GetString(dr["Tel"]);
            item.Zip = DALHelper.DBHelper.GetString(dr["Zip"]);
            item.ShippingID = DALHelper.DBHelper.GetInt(dr["ShippingID"]);
            return item;
        }

        public int Insert(Order model, List<OrderList> list)
        {
            int revalue = 0;
            DbConnection connectionString = DALHelper.DBHelper.CreateConnection();
            try
            {
                DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@OrderCode", DbType.String, 50, model.OrderCode), 
                DALHelper.DBHelper.CreateInDbParameter("@UserID", DbType.Int32, 4, model.UserID), 
                DALHelper.DBHelper.CreateInDbParameter("@Price", DbType.Decimal, 5, model.Price), 
                DALHelper.DBHelper.CreateInDbParameter("@Status", DbType.Int32, 4, model.Status), 
                DALHelper.DBHelper.CreateInDbParameter("@OrderTime", DbType.DateTime, model.OrderTime) , 
                DALHelper.DBHelper.CreateInDbParameter("@TrueName", DbType.String, 20, model.TrueName), 
                DALHelper.DBHelper.CreateInDbParameter("@CityID", DbType.Int32, 4, model.CityID),  
                DALHelper.DBHelper.CreateInDbParameter("@Address", DbType.String, 100, model.Address), 
                DALHelper.DBHelper.CreateInDbParameter("@Mobile", DbType.String, 15, model.Mobile), 
                DALHelper.DBHelper.CreateInDbParameter("@Tel", DbType.String, 15, model.Tel),
                DALHelper.DBHelper.CreateInDbParameter("@Zip", DbType.String, 10, model.Zip), 
                DALHelper.DBHelper.CreateInDbParameter("@ShippingID", DbType.Int32, model.ShippingID)};
                string cmdText = "insert into d_Order(OrderCode,UserID,Price,OrderTime,Status,TrueName,CityID,Address,Mobile,Tel,Zip,ShippingID)  \r\n " +
                                " values (@OrderCode,@UserID,@Price,@OrderTime,@Status,@TrueName,@CityID,@Address,@Mobile,@Tel,@Zip,@ShippingID); \r\n select @@IDENTITY; \r\n";
                object obj = DALHelper.DBHelper.ExecuteScalar(connectionString, CommandType.Text, cmdText, cmdParms);
                if (obj != null)
                {
                    cmdText = string.Empty;
                    foreach (OrderList item in list)
                        cmdText += string.Format("insert into d_OrderList(OrderID,OrderProductID,Quantity,Price,OrderTime) \r\n" +
                                  "values('{0}','{1}','{2}','{3}','{4}'); \r\n", obj, item.OrderProductID, item.Quantity, item.Price, item.OrderTime);
                    revalue += DALHelper.DBHelper.ExecuteNonQuery(connectionString, CommandType.Text, cmdText, null);
                }
            }
            finally
            {
                connectionString.Close();
                connectionString.Dispose();
            }
            return revalue;
        }

        public int Insert(Order model)
        {
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@OrderCode", DbType.String, 50, model.OrderCode), 
                DALHelper.DBHelper.CreateInDbParameter("@UserID", DbType.Int32, 4, model.UserID), 
                DALHelper.DBHelper.CreateInDbParameter("@Price", DbType.Decimal, 5, model.Price), 
                DALHelper.DBHelper.CreateInDbParameter("@Status", DbType.Int32, 4, model.Status), 
                DALHelper.DBHelper.CreateInDbParameter("@OrderTime", DbType.DateTime, model.OrderTime) , 
                DALHelper.DBHelper.CreateInDbParameter("@TrueName", DbType.String, 20, model.TrueName), 
                DALHelper.DBHelper.CreateInDbParameter("@CityID", DbType.Int32, 4, model.CityID),  
                DALHelper.DBHelper.CreateInDbParameter("@Address", DbType.String, 100, model.Address), 
                DALHelper.DBHelper.CreateInDbParameter("@Mobile", DbType.String, 15, model.Mobile), 
                DALHelper.DBHelper.CreateInDbParameter("@Tel", DbType.String, 15, model.Tel),
                DALHelper.DBHelper.CreateInDbParameter("@Zip", DbType.String, 10, model.Zip), 
                DALHelper.DBHelper.CreateInDbParameter("@ShippingID", DbType.Int32, model.ShippingID)};
            string cmdText = "declare @OrderID int \r\n "+
                " insert into d_Order(OrderCode,UserID,Price,OrderTime,Status,TrueName,CityID,Address,Mobile,Tel,Zip,ShippingID)  \r\n " +
                " values (@OrderCode,@UserID,@Price,@OrderTime,@Status,@TrueName,@CityID,@Address,@Mobile,@Tel,@Zip,@ShippingID); \r\n " +
                " set @OrderID = @@IDENTITY; \r\n " +
                " insert into d_OrderList(OrderID,OrderProductID,Quantity,Price,OrderTime) \r\n " +
                " (select @OrderID,OrderProductID,Quantity,Price,OrderTime from d_Cart where UserID=@UserID); \r\n " +
                " delete d_Cart where UserID=@UserID; \r\n ";
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, cmdText, cmdParms);
        }
        public int Update(Order model, List<OrderList> list)
        {
            int revalue = 0;
            DbConnection connectionString = DALHelper.DBHelper.CreateConnection();
            try
            {
                DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, model.ID), 
                DALHelper.DBHelper.CreateInDbParameter("@UserID", DbType.Int32, 4, model.UserID), 
                DALHelper.DBHelper.CreateInDbParameter("@Price", DbType.Decimal, 5, model.Price), 
                DALHelper.DBHelper.CreateInDbParameter("@Status", DbType.Int32, 4, model.Status),  
                DALHelper.DBHelper.CreateInDbParameter("@TrueName", DbType.String, 20, model.TrueName), 
                DALHelper.DBHelper.CreateInDbParameter("@CityID", DbType.Int32, 4, model.CityID),  
                DALHelper.DBHelper.CreateInDbParameter("@Address", DbType.String, 100, model.Address), 
                DALHelper.DBHelper.CreateInDbParameter("@Mobile", DbType.String, 15, model.Mobile), 
                DALHelper.DBHelper.CreateInDbParameter("@Tel", DbType.String, 15, model.Tel),
                DALHelper.DBHelper.CreateInDbParameter("@Zip", DbType.String, 10, model.Zip), 
                DALHelper.DBHelper.CreateInDbParameter("@ShippingID", DbType.Int32, model.ShippingID)};
                string cmdText = "update d_Order set Price=@Price,Status=@Status,TrueName=@TrueName," +
                    "CityID=@CityID,Address=@Address,Mobile=@Mobile,Tel=@Tel,Zip=@Zip,ShippingID=@ShippingID where ID=@ID;";
                object obj = DALHelper.DBHelper.ExecuteNonQuery(connectionString, CommandType.Text, cmdText, cmdParms);
                if (obj != null)
                {
                    cmdText = string.Empty;
                    foreach (OrderList item in list)
                    {
                        if (item.ID == 0)
                            cmdText += string.Format("insert into d_OrderList(OrderID,OrderProductID,Quantity,Price,OrderTime) \r\n" +
                                      "values('{0}','{1}','{2}','{3}','{4}'); \r\n", model.ID, item.OrderProductID, item.Quantity, item.Price, item.OrderTime);
                        else
                            cmdText += string.Format("update d_OrderList set Quantity={1} where ID={0}; \r\n", item.ID, item.Quantity);
                    }
                    revalue += DALHelper.DBHelper.ExecuteNonQuery(connectionString, CommandType.Text, cmdText, null);
                }
            }
            finally
            {
                connectionString.Close();
                connectionString.Dispose();
            }
            return revalue;
        }
        public int Update(int ID, int status)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update d_Order set Status=@Status ");
            builder.Append(" where ID=@ID ");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, ID), DALHelper.DBHelper.CreateInDbParameter("@Status", DbType.Int32, 4, status) };
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), cmdParms);
        }
    }
}

namespace SqlServerDAL
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class ShippingData : DALHelper
    {
        public int Delete(List<string> ids)
        {
            StringBuilder builder = new StringBuilder();
            if (ids.Count > 0)
            {
                foreach (string id in ids)
                {
                    builder.AppendFormat("if not exists (select id from d_Order where ShippingID={0}) \r\n", id);
                    builder.Append("begin \r\n");
                    builder.AppendFormat("DELETE FROM d_Shipping WHERE ID={0};\r\n", id);
                    builder.Append("end");
                }
                return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
            } 
            return 0;
        }

        private Shipping GetItem(DbDataReader dr, Shipping item)
        {
            item.ID = DALHelper.DBHelper.GetInt(dr["ID"]);
            item.Name = DALHelper.DBHelper.GetString(dr["Name"]);
            item.Price = DALHelper.DBHelper.GetDecimal(dr["Price"]);
            item.OrderID = DALHelper.DBHelper.GetInt(dr["OrderID"]);
            return item;
        }

        public Shipping GetItem(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ID,Name,Price,OrderID FROM d_Shipping");
            builder.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID) };
            Shipping item = null;
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            item = new Shipping();
                            this.GetItem(reader, item);
                        }
                    }
                }
                finally
                {
                    if (reader  != null && !reader.IsClosed)
                    {
                        reader.Close();
                        reader.Dispose();
                    }
                }
                return item;
            }
        }

        public List<Shipping> GetList()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ID,Name,Price,OrderID FROM d_Shipping ORDER BY OrderID");
            List<Shipping> list = new List<Shipping>();
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            Shipping item = new Shipping();
                            list.Add(this.GetItem(reader, item));
                        }
                    }
                }
                finally
                {
                    if (reader  != null && !reader.IsClosed)
                    {
                        reader.Close();
                        reader.Dispose();
                    }
                }
                return list;
            }
        }

        public int Insert(Shipping item)
        {
            string str = string.Empty;
            int num = 0;
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@Name", DbType.String, item.Name), 
                DALHelper.DBHelper.CreateInDbParameter("@Price", DbType.Decimal, item.Price), 
                DALHelper.DBHelper.CreateInDbParameter("@OrderID", DbType.Int32, item.OrderID) };
            str = "INSERT INTO d_Shipping(Name,Price,OrderID) \r\n  " +
                "VALUES(@Name,@Price,@OrderID);select @@IDENTITY;";
            object obj2 = DALHelper.DBHelper.ExecuteScalar(CommandType.Text, str, cmdParms);
            if (obj2 != null)
            {
                num = int.Parse(obj2.ToString());
            }
            return num;
        }

        public int Update(Shipping item)
        {
            string str = string.Empty;
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, item.ID), 
                DALHelper.DBHelper.CreateInDbParameter("@Name", DbType.String, item.Name), 
                DALHelper.DBHelper.CreateInDbParameter("@Price", DbType.Decimal, item.Price), 
                DALHelper.DBHelper.CreateInDbParameter("@OrderID", DbType.Int32, item.OrderID) };
            str = "UPDATE d_Shipping SET Name=@Name,Price=@Price,OrderID=@OrderID where ID=@ID";
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), cmdParms);
        }
    }
}

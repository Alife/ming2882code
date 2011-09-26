namespace SqlServerDAL
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class OrderProductData : DALHelper
    {
        public OrderProduct GetItem(int _id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,ProductID,ProductName,ProductCode,FilePath,[Content] from d_OrderProduct ");
            builder.Append(" where ProductID=@ID");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, _id) };
            OrderProduct item = null;
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            item = new OrderProduct();
                            item.ID = reader.GetInt32(0);
                            item.ProductID = reader.GetInt32(1);
                            item.ProductName = reader.GetString(2);
                            item.ProductCode = reader.GetString(3);
                            item.FilePath = (reader.GetValue(4) != DBNull.Value) ? reader.GetString(4) : string.Empty;
                            item.Content = (reader.GetValue(5) != DBNull.Value) ? reader.GetString(5) : string.Empty;
                        }
                    }
                }
                finally
                {
                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
            }
            return item;
        }
        public int Insert(OrderProduct item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into d_OrderProduct(");
            builder.Append("ProductID,ProductName,ProductCode,FilePath,[Content])");
            builder.Append(" values (");
            builder.Append("@ProductID,@ProductName,@ProductCode,@FilePath,@Content)");
            builder.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[] {
                DALHelper.DBHelper.CreateInDbParameter("@ProductID", DbType.Int32, 4, item.ProductID), 
                DALHelper.DBHelper.CreateInDbParameter("@ProductName", DbType.String, 50, item.ProductName), 
                DALHelper.DBHelper.CreateInDbParameter("@ProductCode", DbType.String, 50, item.ProductCode), 
                DALHelper.DBHelper.CreateInDbParameter("@FilePath", DbType.String, 100, item.FilePath), 
                DALHelper.DBHelper.CreateInDbParameter("@Content", DbType.String, item.Content) };
            object obj2 = DALHelper.DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), cmdParms);
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }

        public int Update(OrderProduct item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update d_OrderProduct set ");
            builder.Append("ProductID=@ProductID,");
            builder.Append("ProductName=@ProductName,");
            builder.Append("ProductCode=@ProductCode,");
            builder.Append("FilePath=@FilePath,");
            builder.Append("[Content]=@Content");
            builder.Append(" where ProductID=@ProductID ");
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@ProductID", DbType.Int32, 4, item.ProductID), 
                DALHelper.DBHelper.CreateInDbParameter("@ProductName", DbType.String, 50, item.ProductName), 
                DALHelper.DBHelper.CreateInDbParameter("@ProductCode", DbType.String, 50, item.ProductCode), 
                DALHelper.DBHelper.CreateInDbParameter("@FilePath", DbType.String, 100, item.FilePath), 
                DALHelper.DBHelper.CreateInDbParameter("@Content", DbType.String, item.Content) 
            };
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), cmdParms);
        }
    }
}

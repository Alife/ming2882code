namespace SqlServerDAL
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class ProductCommentData : DALHelper
    {
        public int Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete d_ProductComment ");
            builder.Append(" where ID=@ID ");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, ID) };
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), cmdParms);
        }

        public ProductComment GetItem(int _id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,UserID,ProductID,ParentID,[Content],CreateTime from d_ProductComment ");
            builder.Append(" where ID=@ID");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, _id) };
            ProductComment item = null;
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            item = new ProductComment();
                            GetItem(reader, item);
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

        private ProductComment GetItem(DbDataReader dr, ProductComment item)
        {
            item.ID = DALHelper.DBHelper.GetInt(dr["ID"]);
            item.UserID = DALHelper.DBHelper.GetInt(dr["UserID"]);
            item.ProductID = DALHelper.DBHelper.GetInt(dr["ProductID"]);
            item.ParentID = DALHelper.DBHelper.GetInt(dr["ParentID"]);
            item.Content = DALHelper.DBHelper.GetString(dr["Content"]);
            item.CreateTime = DALHelper.DBHelper.GetDateTime(dr["CreateTime"]);
            return item;
        }
        public List<ProductComment> GetList(int ID, int _productID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ID,UserID,ProductID,ParentID,[Content],CreateTime FROM d_ProductComment WHERE ParentID=@in_ID and ProductID=@in_ProductID ORDER BY CreateTime Desc");
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID),
                DALHelper.DBHelper.CreateInDbParameter("@in_ProductID", DbType.Int32, _productID)};
            List<ProductComment> list = new List<ProductComment>();
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            ProductComment item = new ProductComment();
                            list.Add(this.GetItem(reader, item));
                        }
                    }
                }
                finally
                {
                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                        reader.Dispose();
                    }
                }
                return list;
            }
        }
        public ProductCommentList GetList(int _productID, int _pageIndex, int _pageSize)
        {
            ProductCommentList list = new ProductCommentList();
            DbConnection connectionString = DALHelper.DBHelper.CreateConnection();
            try
            {
                string str = string.Empty;
                string str2 = "order by CreateTime desc";
                List<DbParameter> list2 = new List<DbParameter>();
                list2.Add(DALHelper.DBHelper.CreateInDbParameter("@PageSize", DbType.Int32, _pageSize));
                list2.Add(DALHelper.DBHelper.CreateInDbParameter("@PageIndex", DbType.Int32, _pageIndex));
                if (_productID > 0)
                    str += string.Format("and ProductID={0}", _productID);
                DbParameter[] cmdParms = list2.ToArray();
                string cmdText = "SELECT COUNT(ID) FROM d_ProductComment where 1=1 " + str;
                object obj2 = DALHelper.DBHelper.ExecuteScalar(connectionString, CommandType.Text, cmdText, cmdParms);
                if (obj2 != null)
                {
                    list.RecordNumber = int.Parse(obj2.ToString());
                }
                else
                {
                    return list;
                }
                if (list.RecordNumber != 0)
                {
                    cmdText = string.Format("SELECT ID,UserID,ProductID,ParentID,[Content],CreateTime\r\n" +
                        " FROM \r\n" +
                        " (select ID,UserID,ProductID,ParentID,[Content],CreateTime\r\n " +
                        " ,ROW_NUMBER() Over({0}) as rowNum from d_ProductComment where 1=1 {1}) as temptable\r\n" +
                        " WHERE rowNum BETWEEN ((@PageIndex-1)*@PageSize+1) and (@PageIndex)*@PageSize", str2, str);
                    DbDataReader reader = DALHelper.DBHelper.ExecuteReader(connectionString, CommandType.Text, cmdText, cmdParms);
                    while (reader.Read())
                    {
                        ProductComment item = new ProductComment();
                        GetItem(reader, item);
                        list.Add(item);
                    }
                }
            }
            finally
            {
                connectionString.Close();
                connectionString.Dispose();
            }
            return list;
        }

        public int Insert(ProductComment item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into d_ProductComment(");
            builder.Append("UserID,ProductID,ParentID,[Content],CreateTime)");
            builder.Append(" values (");
            builder.Append("@UserID,@ProductID,@ParentID,@Content,@CreateTime)");
            builder.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[] {
                DALHelper.DBHelper.CreateInDbParameter("@UserID", DbType.Int32, 4, item.UserID), 
                DALHelper.DBHelper.CreateInDbParameter("@ProductID", DbType.Int32, 4, item.ProductID), 
                DALHelper.DBHelper.CreateInDbParameter("@ParentID", DbType.Int32, 4, item.ParentID), 
                DALHelper.DBHelper.CreateInDbParameter("@Content", DbType.String, item.Content), 
                DALHelper.DBHelper.CreateInDbParameter("@CreateTime", DbType.DateTime, item.CreateTime) };
            object obj2 = DALHelper.DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), cmdParms);
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }

        public int Update(ProductComment item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update d_ProductComment set ");
            builder.Append("UserID=@UserID,");
            builder.Append("ProductID=@ProductID,");
            builder.Append("ParentID=@ParentID,");
            builder.Append("Content=@Content,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where ID=@ID ");
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, item.ID), 
                DALHelper.DBHelper.CreateInDbParameter("@UserID", DbType.Int32, 4, item.UserID), 
                DALHelper.DBHelper.CreateInDbParameter("@ProductID", DbType.Int32, 4, item.ProductID), 
                DALHelper.DBHelper.CreateInDbParameter("@ParentID", DbType.Int32, 4, item.ParentID), 
                DALHelper.DBHelper.CreateInDbParameter("@Content", DbType.String, item.Content), 
                DALHelper.DBHelper.CreateInDbParameter("@CreateTime", DbType.DateTime, item.CreateTime)
            };
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), cmdParms);
        }
    }
}

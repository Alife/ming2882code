namespace SqlServerDAL
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class sys_LinkData : DALHelper
    {
        public int Delete(List<string> ID)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string item in ID)
                builder.AppendFormat("DELETE FROM sys_Link WHERE ID={0}; \r\n", item);
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }

        public sys_Link GetItem(int _id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  ID,LinkName,PicUrl,Url,OrderID from sys_Link ");
            builder.Append(" where ID=@ID");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, _id) };
            sys_Link link = null;
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            link = new sys_Link();
                            link.ID = reader.GetInt32(0);
                            link.LinkName = reader.GetString(1);
                            link.PicUrl = (reader.GetValue(2) != DBNull.Value) ? reader.GetString(2) : string.Empty;
                            link.Url = reader.GetString(3);
                            link.OrderID = reader.GetInt32(4);
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
            return link;
        }

        public List<sys_Link> GetList(int num)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select {0} ID,LinkName,PicUrl,Url,OrderID from sys_Link ", num > 0 ? string.Format(" top {0}", num) : "");
            builder.Append(" Order By OrderID Desc");
            List<sys_Link> list = new List<sys_Link>();
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            sys_Link link = new sys_Link();
                            link.ID = reader.GetInt32(0);
                            link.LinkName = reader.GetString(1);
                            link.PicUrl = (reader.GetValue(2) != DBNull.Value) ? reader.GetString(2) : string.Empty;
                            link.Url = reader.GetString(3);
                            link.OrderID = reader.GetInt32(4);
                            list.Add(link);
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

        public int Insert(sys_Link item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into sys_Link(");
            builder.Append("LinkName,PicUrl,Url,OrderID)");
            builder.Append(" values (");
            builder.Append("@LinkName,@PicUrl,@Url,@OrderID)");
            builder.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@LinkName", DbType.String, 50, item.LinkName), DALHelper.DBHelper.CreateInDbParameter("@PicUrl", DbType.String, 100, item.PicUrl), DALHelper.DBHelper.CreateInDbParameter("@Url", DbType.String, 50, item.Url), DALHelper.DBHelper.CreateInDbParameter("@OrderID", DbType.Int32, 4, item.OrderID) };
            object obj2 = DALHelper.DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), cmdParms);
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }

        public int Update(sys_Link item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update sys_Link set ");
            builder.Append("LinkName=@LinkName,");
            builder.Append("PicUrl=@PicUrl,");
            builder.Append("Url=@Url,");
            builder.Append("OrderID=@OrderID");
            builder.Append(" where ID=@ID ");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, item.ID), DALHelper.DBHelper.CreateInDbParameter("@LinkName", DbType.String, 50, item.LinkName), DALHelper.DBHelper.CreateInDbParameter("@PicUrl", DbType.String, 100, item.PicUrl), DALHelper.DBHelper.CreateInDbParameter("@Url", DbType.String, 50, item.Url), DALHelper.DBHelper.CreateInDbParameter("@OrderID", DbType.Int32, 4, item.OrderID) };
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), cmdParms);
        }

        public int Update(List<string> ID, List<string> OrderID)
        {
            string strSql = string.Empty;
            int i = 0;
            foreach (string item in ID)
            {
                strSql += string.Format("update sys_Link set orderid={1} WHERE ID={0};\r\n", item, OrderID[i]);
                i++;
            }
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }
    }
}

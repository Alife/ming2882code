namespace SqlServerDAL
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class PageData : DALHelper
    {
        public int Delete(List<string> ID)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string item in ID)
            {
                builder.AppendFormat("if not exists (select id from sys_Page where ParentID={0}) \r\n", item);
                builder.Append("begin \r\n");
                builder.AppendFormat("DELETE FROM sys_Page WHERE ID={0}; \r\n", item);
                builder.Append("end \r\n");
            }
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }
        private sys_Page GetItem(DbDataReader dr, sys_Page item)
        {
            item.ID = DALHelper.DBHelper.GetInt(dr["ID"]);
            item.Name = DALHelper.DBHelper.GetString(dr["Name"]);
            item.Code = DALHelper.DBHelper.GetString(dr["Code"]);
            item.Content = DALHelper.DBHelper.GetString(dr["Content"]);
            item.Url = DALHelper.DBHelper.GetString(dr["Url"]);
            item.ParentID = DALHelper.DBHelper.GetInt(dr["ParentID"]);
            item.OrderID = DALHelper.DBHelper.GetInt(dr["OrderID"]);
            item.Path = DALHelper.DBHelper.GetInt(dr["Path"]);
            item.IsHide = DALHelper.DBHelper.GetBool(dr["IsHide"]);
            return item;
        }
        public int Exists(string _Code)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID from sys_Page");
            builder.AppendFormat(" where Code='{0}' ", _Code);
            object obj2 = DALHelper.DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), null);
            if (obj2 != null)
            {
                return int.Parse(obj2.ToString());
            }
            return 0;
        }
        public int Update(List<string> ID, List<string> OrderID)
        {
            string strSql = string.Empty;
            int i = 0;
            foreach (string item in ID)
            {
                strSql += string.Format("update sys_Page set orderid={1} WHERE ID={0};\r\n", item, OrderID[i]);
                i++;
            }
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }

        public sys_Page GetItem(string _id, int _type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,Name,Code,ParentID,Content,Url,OrderID,Path,IsHide from sys_Page ");
            if (_type == 0)
                builder.Append(" where ID=@ID");
            else
                builder.Append(" where Code=@ID");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.String, _id) };
            sys_Page item = null;
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            item = new sys_Page();
                            GetItem(reader, item);
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
            return item;
        }
        public List<sys_Page> GetList()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,Name,Code,ParentID,Content,Url,OrderID,Path,IsHide from sys_Page ");
            builder.Append(" Order By OrderID");
            List<sys_Page> list = new List<sys_Page>();
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            list.Add(GetItem(reader, new sys_Page()));
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
            return list;
        }

        public List<sys_Page> GetList(int parentID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,Name,Code,ParentID,Content,Url,OrderID,Path,IsHide from sys_Page ");
            builder.AppendFormat(" where ParentID={0} and IsHide=0", parentID);
            builder.Append(" Order By OrderID");
            List<sys_Page> list = new List<sys_Page>();
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            list.Add(GetItem(reader, new sys_Page()));
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
        public List<sys_Page> GetListByChild(int parentid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ID,Name,Code,ParentID,Content,Url,OrderID,Path,IsHide from sys_Page");
            builder.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@in_ID", DbType.String, parentid) };
            List<sys_Page> list = new List<sys_Page>();
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            sys_Page item = new sys_Page();
                            list.Add(this.GetItem(reader, item));
                            list.AddRange(this.GetListByChild(item.ParentID));
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
            }
            return list;
        }

        public int Insert(sys_Page item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into sys_Page(");
            builder.Append("Name,Code,ParentID,Content,Url,OrderID,Path,IsHide)");
            builder.Append(" values (");
            builder.Append("@Name,@Code,@ParentID,@Content,@Url,@OrderID,@Path,@IsHide)");
            builder.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@Name", DbType.String, 20, item.Name), 
                DALHelper.DBHelper.CreateInDbParameter("@Code", DbType.String, 20, item.Code), 
                DALHelper.DBHelper.CreateInDbParameter("@ParentID", DbType.Int32, item.ParentID), 
                DALHelper.DBHelper.CreateInDbParameter("@Content", DbType.String, item.Content), 
                DALHelper.DBHelper.CreateInDbParameter("@Url", DbType.String, 50, item.Url), 
                DALHelper.DBHelper.CreateInDbParameter("@OrderID", DbType.Int32, 4, item.OrderID), 
                DALHelper.DBHelper.CreateInDbParameter("@Path", DbType.Int32, 4, item.Path),
                DALHelper.DBHelper.CreateInDbParameter("@IsHide", DbType.Boolean, item.IsHide)};
            object obj2 = DALHelper.DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), cmdParms);
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }

        public int Update(sys_Page item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update sys_Page set ");
            builder.Append("Name=@Name,");
            builder.Append("Code=@Code,");
            builder.Append("ParentID=@ParentID,");
            builder.Append("Content=@Content,");
            builder.Append("Url=@Url,");
            builder.Append("OrderID=@OrderID,");
            builder.Append("Path=@Path,");
            builder.Append("IsHide=@IsHide");
            builder.Append(" where ID=@ID ");
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, item.ID), 
                DALHelper.DBHelper.CreateInDbParameter("@Name", DbType.String, 20, item.Name), 
                DALHelper.DBHelper.CreateInDbParameter("@Code", DbType.String, 20, item.Code), 
                DALHelper.DBHelper.CreateInDbParameter("@ParentID", DbType.Int32, item.ParentID), 
                DALHelper.DBHelper.CreateInDbParameter("@Content", DbType.String, item.Content), 
                DALHelper.DBHelper.CreateInDbParameter("@Url", DbType.String, 50, item.Url), 
                DALHelper.DBHelper.CreateInDbParameter("@OrderID", DbType.Int32, 4, item.OrderID), 
                DALHelper.DBHelper.CreateInDbParameter("@Path", DbType.Int32, 4, item.Path) ,
                DALHelper.DBHelper.CreateInDbParameter("@IsHide", DbType.Boolean, item.IsHide)}; 
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), cmdParms);
        }
    }
}

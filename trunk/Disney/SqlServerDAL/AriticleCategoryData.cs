namespace SqlServerDAL
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class ArticleCategoryData : DALHelper
    {
        public int Delete(List<string> ID)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string item in ID)
            {
                builder.AppendFormat("if not exists (select id from article where CategoryID={0}) \r\n", item);
                builder.Append("begin \r\n");
                builder.AppendFormat("DELETE FROM article_Category WHERE ID={0}; \r\n", item);
                builder.Append("end \r\n");
            }
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }
        public int Exists(string _Code)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID from article_Category");
            builder.AppendFormat(" where Code='{0}' ", _Code);
            object obj2 = DALHelper.DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), null);
            if (obj2 != null)
            {
                return int.Parse(obj2.ToString());
            }
            return 0;
        }

        private ArticleCategory GetItem(DbDataReader dr, ArticleCategory item)
        {
            item.ID = DALHelper.DBHelper.GetInt(dr["ID"]);
            item.Code = DALHelper.DBHelper.GetString(dr["Code"]);
            item.ParentID = DALHelper.DBHelper.GetInt(dr["ParentID"]);
            item.Category = DALHelper.DBHelper.GetString(dr["Category"]);
            item.OrderID = DALHelper.DBHelper.GetInt(dr["OrderID"]);
            item.Path = DALHelper.DBHelper.GetInt(dr["Path"]);
            item.Description = DALHelper.DBHelper.GetString(dr["Description"]);
            item.MetaKeywords = DALHelper.DBHelper.GetString(dr["MetaKeywords"]);
            item.MetaDescription = DALHelper.DBHelper.GetString(dr["MetaDescription"]);
            item.ReadCategory = DALHelper.DBHelper.GetString(dr["ReadCategory"]);
            return item;
        }

        public ArticleCategory GetItem(string ID, int type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ID,Code,Category,ParentID,OrderID,Path,MetaKeywords,MetaDescription,Description,ReadCategory FROM article_Category");
            if (type == 0)
            {
                builder.Append(" WHERE ID=@in_ID");
            }
            else
            {
                builder.Append(" WHERE Code=@in_ID");
            }
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@in_ID", DbType.String, ID) };
            ArticleCategory item = null;
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            item = new ArticleCategory();
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

        public List<ArticleCategory> GetList(string _unQuery)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ID,Code,Category,ParentID,OrderID,Path,MetaKeywords,MetaDescription,Description,ReadCategory FROM article_Category where 1=1 ");
            if (!string.IsNullOrEmpty(_unQuery))
            {
                string temsql = "and Code not in (";
                foreach (string temType in _unQuery.Split(','))
                    temsql += string.Format("'{0}',", temType);
                temsql = temsql.TrimEnd(',');
                temsql += ") ";
                builder.Append(temsql);
            }
            builder.Append(" ORDER BY OrderID");
            List<ArticleCategory> list = new List<ArticleCategory>();
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            ArticleCategory item = new ArticleCategory();
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

        public List<ArticleCategory> GetListByChild(int parentid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ID,Code,Category,ParentID,OrderID,Path,MetaKeywords,MetaDescription,Description,ReadCategory FROM article_Category");
            builder.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@in_ID", DbType.String, parentid) };
            List<ArticleCategory> list = new List<ArticleCategory>();
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            ArticleCategory item = new ArticleCategory();
                            list.Add(this.GetItem(reader, item));
                            list.AddRange(this.GetListByChild(item.ParentID));
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

        public List<ArticleCategory> GetList(int id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ID,Code,Category,ParentID,OrderID,Path,MetaKeywords,MetaDescription,Description,ReadCategory FROM article_Category");
            builder.Append(" WHERE ParentID=@in_ParentID ORDER BY OrderID");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@in_ParentID", DbType.String, id) };
            List<ArticleCategory> list = new List<ArticleCategory>();
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            list.Add(this.GetItem(reader, new ArticleCategory()));
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

        public int Insert(ArticleCategory item)
        {
            string str = string.Empty;
            int num = 0;
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@Code", DbType.String, item.Code), 
                DALHelper.DBHelper.CreateInDbParameter("@Category", DbType.String, item.Category), 
                DALHelper.DBHelper.CreateInDbParameter("@ParentID", DbType.Int32, item.ParentID), 
                DALHelper.DBHelper.CreateInDbParameter("@OrderID", DbType.Int32, item.OrderID), 
                DALHelper.DBHelper.CreateInDbParameter("@Path", DbType.Int32, item.Path), 
                DALHelper.DBHelper.CreateInDbParameter("@MetaKeywords", DbType.String, item.MetaKeywords), 
                DALHelper.DBHelper.CreateInDbParameter("@MetaDescription", DbType.String, item.MetaDescription) , 
                DALHelper.DBHelper.CreateInDbParameter("@Description", DbType.String, item.Description) , 
                DALHelper.DBHelper.CreateInDbParameter("@ReadCategory", DbType.String, item.ReadCategory) };
            str = "INSERT INTO article_Category(Code,Category,ParentID,OrderID,Path,MetaKeywords,MetaDescription,Description,ReadCategory) \r\n  " +
                "VALUES(@Code,@Category,@ParentID,@OrderID,@Path,@MetaKeywords,@MetaDescription,@Description,@ReadCategory);select @@IDENTITY;";
            object obj2 = DALHelper.DBHelper.ExecuteScalar(CommandType.Text, str, cmdParms);
            if (obj2 != null)
            {
                num = int.Parse(obj2.ToString());
            }
            return num;
        }

        public int Update(ArticleCategory item)
        {
            string str = string.Empty;
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, item.ID), 
                DALHelper.DBHelper.CreateInDbParameter("@Code", DbType.String, item.Code), 
                DALHelper.DBHelper.CreateInDbParameter("@Category", DbType.String, item.Category), 
                DALHelper.DBHelper.CreateInDbParameter("@ParentID", DbType.Int32, item.ParentID), 
                DALHelper.DBHelper.CreateInDbParameter("@OrderID", DbType.Int32, item.OrderID), 
                DALHelper.DBHelper.CreateInDbParameter("@Path", DbType.Int32, item.Path), 
                DALHelper.DBHelper.CreateInDbParameter("@MetaKeywords", DbType.String, item.MetaKeywords), 
                DALHelper.DBHelper.CreateInDbParameter("@MetaDescription", DbType.String, item.MetaDescription) , 
                DALHelper.DBHelper.CreateInDbParameter("@Description", DbType.String, item.Description) , 
                DALHelper.DBHelper.CreateInDbParameter("@ReadCategory", DbType.String, item.ReadCategory)  };
            str = "UPDATE article_Category SET Code=@Code,Category=@Category,ParentID=@ParentID,OrderID=@OrderID,Path=@Path,\r\n" +
                "MetaKeywords=@MetaKeywords,MetaDescription=@MetaDescription,Description=@Description,ReadCategory=@ReadCategory\r\n " +
                "where ID=@ID";
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), cmdParms);
        }

        public int Update(List<ArticleCategory> list)
        {
            string str = string.Empty;
            foreach (ArticleCategory category in list)
            {
                str = str + string.Format("UPDATE article_Category SET OrderID={1} where ID={0};", category.ID, category.OrderID);
            }
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), null);
        }
    }
}

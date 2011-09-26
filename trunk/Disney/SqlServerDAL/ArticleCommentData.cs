namespace SqlServerDAL
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class ArticleCommentData : DALHelper
    {
        public int Delete(List<string> ID)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string item in ID)
            {
                builder.AppendFormat("delete Article_Comment where ParentID={0}; ", item);
                builder.AppendFormat("delete Article_Comment where ID={0}; ", item);
            }
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }
        private ArticleComment GetItem(DbDataReader dr, ArticleComment item)
        {
            item.ID = DALHelper.DBHelper.GetInt(dr["ID"]);
            item.ArticleID = DALHelper.DBHelper.GetInt(dr["ArticleID"]);
            item.UserID = DALHelper.DBHelper.GetInt(dr["UserID"]);
            item.ParentID = DALHelper.DBHelper.GetInt(dr["ParentID"]);
            item.Content = DALHelper.DBHelper.GetString(dr["Content"]);
            item.CreateTime = DALHelper.DBHelper.GetDateTime(dr["CreateTime"]);
            return item;
        }

        public ArticleComment GetItem(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,ArticleID,UserID,ParentID,Content,CreateTime from Article_Comment ");
            builder.Append(" where ID=@ID");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, ID) };
            ArticleComment item = null;
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            item = new ArticleComment();
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

        public ArticleCommentList GetList(int articleID, DateTime? beginTime, DateTime? endTime, int pageIndex, int pageSize)
        {
            ArticleCommentList list = new ArticleCommentList();
            DbConnection connectionString = DALHelper.DBHelper.CreateConnection();
            try
            {
                string query = "and ParentID=0 ";
                string order = "order by CreateTime desc";
                List<DbParameter> cmdParms = new List<DbParameter>();
                cmdParms.Add(DALHelper.DBHelper.CreateInDbParameter("@PageSize", DbType.Int32, pageSize));
                cmdParms.Add(DALHelper.DBHelper.CreateInDbParameter("@PageIndex", DbType.Int32, pageIndex));
                if (articleID > 0)
                {
                    query += "and ArticleID=@articleID ";
                    cmdParms.Add(DALHelper.DBHelper.CreateInDbParameter("@articleID", DbType.Int32, articleID));
                }
                if (beginTime.HasValue && endTime.HasValue && beginTime <= endTime)
                {
                    query += "and CreateTime between @beginTime and @endtime ";
                    cmdParms.Add(DBHelper.CreateInDbParameter("@beginTime", DbType.DateTime, beginTime));
                    cmdParms.Add(DBHelper.CreateInDbParameter("@endTime", DbType.DateTime, endTime.Value.AddDays(1)));
                }
                string cmdText = "SELECT COUNT(ID) FROM Article_Comment where 1=1 " + query;
                object obj = DALHelper.DBHelper.ExecuteScalar(connectionString, CommandType.Text, cmdText, cmdParms.ToArray());
                if (obj != null)
                {
                    list.RecordNumber = int.Parse(obj.ToString());
                }
                else
                {
                    return list;
                }
                if (list.RecordNumber != 0)
                {
                    cmdText = string.Format("select ID,ArticleID,UserID,ParentID,Content,CreateTime\r\n" +
                        " FROM \r\n" +
                        " (select ID,ArticleID,UserID,ParentID,Content,CreateTime\r\n " +
                        " ,ROW_NUMBER() Over({0}) as rowNum from Article_Comment where 1=1 {1}) as temptable\r\n" +
                        " WHERE rowNum BETWEEN ((@PageIndex-1)*@PageSize+1) and (@PageIndex)*@PageSize", order, query);
                    DbDataReader reader = DALHelper.DBHelper.ExecuteReader(connectionString, CommandType.Text, cmdText, cmdParms.ToArray());
                    while (reader.Read())
                    {
                        ArticleComment item = new ArticleComment();
                        GetItem(reader, item);
                        item.Dots[0] = int.Parse(DALHelper.DBHelper.ExecuteScalar(connectionString, CommandType.Text,
                            "SELECT isnull(COUNT(ID),0) FROM Article_Dot where dot=1 and CommentID=" + item.ID, cmdParms.ToArray()).ToString());
                        item.Dots[1] = int.Parse(DALHelper.DBHelper.ExecuteScalar(connectionString, CommandType.Text,
                            "SELECT isnull(COUNT(ID),0) FROM Article_Dot where dot=2 and CommentID=" + item.ID, cmdParms.ToArray()).ToString());
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

        public List<ArticleComment> GetList(int parentID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,ArticleID,UserID,ParentID,Content,CreateTime from Article_Comment ");
            builder.Append(" where parentID=@parentID");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@parentID", DbType.Int32, 4, parentID) };
            List<ArticleComment> list = new List<ArticleComment>();
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            ArticleComment item = new ArticleComment();
                            list.Add(GetItem(reader, item));
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

        public int Insert(ArticleComment item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Article_Comment(");
            builder.Append("ArticleID,UserID,ParentID,Content,CreateTime)");
            builder.Append(" values (");
            builder.Append("@ArticleID,@UserID,@ParentID,@Content,@CreateTime)");
            builder.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[] {
                DALHelper.DBHelper.CreateInDbParameter("@ArticleID", DbType.Int32, item.ArticleID), 
                DALHelper.DBHelper.CreateInDbParameter("@UserID", DbType.Int32, item.UserID), 
                DALHelper.DBHelper.CreateInDbParameter("@ParentID", DbType.Int32, item.ParentID), 
                DALHelper.DBHelper.CreateInDbParameter("@Content", DbType.String, item.Content), 
                DALHelper.DBHelper.CreateInDbParameter("@CreateTime", DbType.DateTime, item.CreateTime)};
            object obj2 = DALHelper.DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), cmdParms);
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }

        public int Update(ArticleComment item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Article_Comment set ");
            builder.Append("ArticleID=@ArticleID,");
            builder.Append("UserID=@UserID,");
            builder.Append("ParentID=@ParentID,");
            builder.Append("Content=@Content,");
            builder.Append("CreateTime=@CreateTime");
            builder.Append(" where ID=@ID ");
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, item.ID), 
                DALHelper.DBHelper.CreateInDbParameter("@ArticleID", DbType.Int32, item.ArticleID), 
                DALHelper.DBHelper.CreateInDbParameter("@UserID", DbType.Int32, item.UserID), 
                DALHelper.DBHelper.CreateInDbParameter("@ParentID", DbType.Int32, item.ParentID), 
                DALHelper.DBHelper.CreateInDbParameter("@Content", DbType.String, item.Content), 
                DALHelper.DBHelper.CreateInDbParameter("@CreateTime", DbType.DateTime, item.CreateTime)
            };
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), cmdParms);
        }
    }
}

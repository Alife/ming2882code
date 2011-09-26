using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace SqlServerDAL
{
    public class ArticleData : DALHelper
    {
        public int Delete(List<string> ID)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string item in ID)
            {
                builder.AppendFormat("delete Article_File where ArticleID={0}; ", item);
                builder.AppendFormat("delete Article_Comment where ArticleID={0}; ", item);
                builder.AppendFormat("delete Article_Top where ArticleID={0}; ", item);
                builder.AppendFormat("delete Article where ID={0}; ", item);
            }
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }
        private Article GetItem(DbDataReader dr, Article item)
        {
            item.ID = DALHelper.DBHelper.GetInt(dr["ID"]);
            item.CategoryID = DALHelper.DBHelper.GetInt(dr["CategoryID"]);
            item.UserID = DALHelper.DBHelper.GetIntByNull(dr["UserID"]);
            item.Title = DALHelper.DBHelper.GetString(dr["Title"]);
            item.Content = DALHelper.DBHelper.GetString(dr["Content"]);
            item.CreateTime = DALHelper.DBHelper.GetDateTime(dr["CreateTime"]);
            item.Source = DALHelper.DBHelper.GetString(dr["Source"]);
            item.Tags = DALHelper.DBHelper.GetString(dr["Tags"]);
            item.Hits = DALHelper.DBHelper.GetInt(dr["Hits"]);
            item.WriterID = DALHelper.DBHelper.GetInt(dr["WriterID"]);
            item.Writer = DALHelper.DBHelper.GetString(dr["Writer"]);
            item.TitleStyle = DALHelper.DBHelper.GetString(dr["TitleStyle"]);
            item.Url = DALHelper.DBHelper.GetString(dr["Url"]);
            item.Elite = DALHelper.DBHelper.GetInt(dr["Elite"]);
            item.Top = DALHelper.DBHelper.GetInt(dr["Top"]);
            item.IsComment = DALHelper.DBHelper.GetBool(dr["IsComment"]);
            return item;
        }
        public Article GetItem(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,CategoryID,UserID,Title,[Content],CreateTime,Source,Tags,Hits,WriterID,Writer,TitleStyle,Url,Elite,[Top],IsComment from Article ");
            builder.Append(" where ID=@ID");
            DbParameter[] cmdParms;
            cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, ID) };

            Article item = null;
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            item = new Article();
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
        public List<Article> GetList(string categorycode, int _elite, int _top, int num)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select top {0} ID,CategoryID,UserID,Title,[Content],CreateTime", num);
            builder.Append(",Source,Tags,Hits,WriterID,Writer,TitleStyle,Url,Elite,[Top],IsComment from Article where 1=1");
            if (categorycode != string.Empty)
            {
                string tempcategorycode = string.Empty;
                foreach (string code in categorycode.Split(','))
                    tempcategorycode += string.Format("'{0}',", code);
                tempcategorycode = tempcategorycode.TrimEnd(',');
                builder.AppendFormat(" and CategoryID in (select id from article_Category where Code in ({0}))", tempcategorycode);
            }
            if (_elite > 0)
                builder.AppendFormat(" and Elite={0}", _elite);
            if (_top > 0)
                builder.AppendFormat(" and [Top]={0}", _top);
            builder.Append(" Order By CreateTime desc");
            List<Article> list = new List<Article>();
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            list.Add(GetItem(reader, new Article()));
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
        public ArticleList GetList(int _categoryID, string _unQuery, string _title, int _elite, int _top, int _pageIndex, int _pageSize)
        {
            ArticleList list = new ArticleList();
            DbConnection connectionString = DALHelper.DBHelper.CreateConnection();
            try
            {
                string query = string.Empty;
                string order = "order by CreateTime desc";
                List<DbParameter> list2 = new List<DbParameter>();
                list2.Add(DALHelper.DBHelper.CreateInDbParameter("@PageSize", DbType.Int32, _pageSize));
                list2.Add(DALHelper.DBHelper.CreateInDbParameter("@PageIndex", DbType.Int32, _pageIndex));
                if (!string.IsNullOrEmpty(_unQuery))
                {
                    string temsql = "and CategoryID not in (select id FROM article_Category where code in (";
                    foreach (string temType in _unQuery.Split(','))
                        temsql += string.Format("'{0}',", temType);
                    temsql = temsql.TrimEnd(',');
                    temsql += ")) ";
                    query += temsql;
                }
                if (_categoryID > 0)
                    query += string.Format("and CategoryID={0}", _categoryID);
                if (_title != string.Empty)
                    query += string.Format("and Title like '%{0}%' ", _title);
                if (_elite > 0)
                    query += string.Format("and Elite={0}", _elite);
                if (_top > 0)
                    query += string.Format("and [Top]={0}", _top);
                DbParameter[] cmdParms = list2.ToArray();
                string cmdText = "SELECT COUNT(ID) FROM Article where 1=1 " + query;
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
                    cmdText = string.Format("SELECT ID,CategoryID,UserID,Title,[Content],CreateTime,Source,Tags,Hits,WriterID,Writer,TitleStyle,Url,Elite,[Top],IsComment\r\n" +
                        " FROM \r\n" +
                        " (select ID,CategoryID,UserID,Title,[Content],CreateTime,Source,Tags,Hits,WriterID,Writer,TitleStyle,Url,Elite,[Top],IsComment\r\n " +
                        " ,ROW_NUMBER() Over({0}) as rowNum from Article where 1=1 {1}) as temptable\r\n" +
                        " WHERE rowNum BETWEEN ((@PageIndex-1)*@PageSize+1) and (@PageIndex)*@PageSize", order, query);
                    DbDataReader reader = DALHelper.DBHelper.ExecuteReader(connectionString, CommandType.Text, cmdText, cmdParms);
                    while (reader.Read())
                    {
                        Article item = new Article();
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
        public int Insert(Article item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Article(");
            builder.Append("CategoryID,UserID,Title,[Content],CreateTime,Source,Tags,Hits,WriterID,Writer,TitleStyle,Url,Elite,[Top],IsComment)");
            builder.Append(" values (");
            builder.Append("@CategoryID,@UserID,@Title,@Content,@CreateTime,@Source,@Tags,@Hits,@WriterID,@Writer,@TitleStyle,@Url,@Elite,@Top,@IsComment)");
            builder.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@CategoryID", DbType.Int32, item.CategoryID), 
                DALHelper.DBHelper.CreateInDbParameter("@UserID", DbType.Int32, item.UserID), 
                DALHelper.DBHelper.CreateInDbParameter("@Title", DbType.String, 50, item.Title), 
                DALHelper.DBHelper.CreateInDbParameter("@Content", DbType.String, item.Content), 
                DALHelper.DBHelper.CreateInDbParameter("@CreateTime", DbType.DateTime, item.CreateTime), 
                DALHelper.DBHelper.CreateInDbParameter("@Source", DbType.String, item.Source), 
                DALHelper.DBHelper.CreateInDbParameter("@Tags", DbType.String, item.Tags), 
                DALHelper.DBHelper.CreateInDbParameter("@Hits", DbType.Int32, 4, item.Hits), 
                DALHelper.DBHelper.CreateInDbParameter("@WriterID", DbType.Int32, item.WriterID), 
                DALHelper.DBHelper.CreateInDbParameter("@Writer", DbType.String, item.Writer), 
                DALHelper.DBHelper.CreateInDbParameter("@TitleStyle", DbType.String, item.TitleStyle), 
                DALHelper.DBHelper.CreateInDbParameter("@Url", DbType.String, item.Url), 
                DALHelper.DBHelper.CreateInDbParameter("@Elite", DbType.Int32, item.Elite), 
                DALHelper.DBHelper.CreateInDbParameter("@Top", DbType.Int32, item.Top), 
                DALHelper.DBHelper.CreateInDbParameter("@IsComment", DbType.Boolean, item.IsComment) };
            object obj2 = DALHelper.DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), cmdParms);
            if (obj2 == null)
                return 0;
            return Convert.ToInt32(obj2);
        }
        public int Update(Article item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Article set ");
            builder.Append("CategoryID=@CategoryID,");
            builder.Append("UserID=@UserID,");
            builder.Append("Title=@Title,");
            builder.Append("Content=@Content,");
            builder.Append("CreateTime=@CreateTime,");
            builder.Append("Source=@Source,");
            builder.Append("Hits=@Hits,");
            builder.Append("WriterID=@WriterID,");
            builder.Append("Writer=@Writer,");
            builder.Append("TitleStyle=@TitleStyle,");
            builder.Append("Url=@Url,");
            builder.Append("Elite=@Elite,");
            builder.Append("Tags=@Tags,");
            builder.Append("[Top]=@Top,");
            builder.Append("IsComment=@IsComment");
            builder.Append(" where ID=@ID ");
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, item.ID),
                DALHelper.DBHelper.CreateInDbParameter("@CategoryID", DbType.Int32, item.CategoryID),
                DALHelper.DBHelper.CreateInDbParameter("@UserID", DbType.Int32, item.UserID), 
                DALHelper.DBHelper.CreateInDbParameter("@Title", DbType.String, 50, item.Title), 
                DALHelper.DBHelper.CreateInDbParameter("@Content", DbType.String, item.Content), 
                DALHelper.DBHelper.CreateInDbParameter("@CreateTime", DbType.DateTime, item.CreateTime), 
                DALHelper.DBHelper.CreateInDbParameter("@Source", DbType.String, item.Source), 
                DALHelper.DBHelper.CreateInDbParameter("@Tags", DbType.String, item.Tags), 
                DALHelper.DBHelper.CreateInDbParameter("@Hits", DbType.Int32, 4, item.Hits), 
                DALHelper.DBHelper.CreateInDbParameter("@WriterID", DbType.Int32, item.WriterID), 
                DALHelper.DBHelper.CreateInDbParameter("@Writer", DbType.String, item.Writer), 
                DALHelper.DBHelper.CreateInDbParameter("@TitleStyle", DbType.String, item.TitleStyle), 
                DALHelper.DBHelper.CreateInDbParameter("@Url", DbType.String, item.Url), 
                DALHelper.DBHelper.CreateInDbParameter("@Elite", DbType.Int32, item.Elite), 
                DALHelper.DBHelper.CreateInDbParameter("@Top", DbType.Int32, item.Top), 
                DALHelper.DBHelper.CreateInDbParameter("@IsComment", DbType.Boolean, item.IsComment) };
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), cmdParms);
        }
    }
}

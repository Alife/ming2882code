using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Models;

namespace SqlServerDAL
{
    public class ArticleTopData : DALHelper
    {
        public int Delete(List<string> ID)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string item in ID)
            {
                builder.AppendFormat("delete Article_Top where ID={0}; ", item);
            }
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }
        private ArticleTop GetItem(DbDataReader dr, ArticleTop item)
        {
            item.ID = DALHelper.DBHelper.GetInt(dr["ID"]);
            item.ArticleID = DALHelper.DBHelper.GetInt(dr["ArticleID"]);
            item.Intro = DALHelper.DBHelper.GetString(dr["Intro"]);
            item.Title = DALHelper.DBHelper.GetString(dr["Title"]);
            item.Url = DALHelper.DBHelper.GetString(dr["Url"]);
            item.FilePath = DALHelper.DBHelper.GetString(dr["FilePath"]);
            return item;
        }
        public ArticleTop GetItem(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,ArticleID,Intro,Title,Url,FilePath from Article_Top ");
            builder.Append(" where ID=@ID");
            DbParameter[] cmdParms;
            cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, ID) };

            ArticleTop item = null;
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            item = new ArticleTop();
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
        public ArticleTop GetItemByArticle(int articleID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,ArticleID,Intro,Title,Url,FilePath from Article_Top ");
            builder.Append(" where articleID=@articleID");
            DbParameter[] cmdParms;
            cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@articleID", DbType.Int32, 4, articleID) };

            ArticleTop item = null;
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            item = new ArticleTop();
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
        public List<ArticleTop> GetList(int num)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select");
            if (num > 0)
                builder.AppendFormat(" top {0}", num);
            builder.Append(" ID,ArticleID,Intro,Title,Url,FilePath from Article_Top");
            builder.Append(" Order By ID desc");
            List<ArticleTop> list = new List<ArticleTop>();
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            list.Add(GetItem(reader, new ArticleTop()));
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
        public int Insert(ArticleTop item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Article_Top(");
            builder.Append("ArticleID,Intro,Title,Url,FilePath)");
            builder.Append(" values (");
            builder.Append("@ArticleID,@Intro,@Title,@Url,@FilePath)");
            builder.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@ArticleID", DbType.Int32, item.ArticleID), 
                DALHelper.DBHelper.CreateInDbParameter("@Intro", DbType.String, item.Intro), 
                DALHelper.DBHelper.CreateInDbParameter("@Title", DbType.String, 50, item.Title), 
                DALHelper.DBHelper.CreateInDbParameter("@Url", DbType.String, item.Url),
                DALHelper.DBHelper.CreateInDbParameter("@FilePath", DbType.String, item.FilePath) };
            object obj2 = DALHelper.DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), cmdParms);
            if (obj2 == null)
                return 0;
            return Convert.ToInt32(obj2);
        }
        public int Update(ArticleTop item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Article_Top set ");
            builder.Append("ArticleID=@ArticleID,");
            builder.Append("Intro=@Intro,");
            builder.Append("Title=@Title,");
            builder.Append("Url=@Url,");
            builder.Append("FilePath=@FilePath");
            builder.Append(" where ID=@ID ");
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, item.ID),
                DALHelper.DBHelper.CreateInDbParameter("@ArticleID", DbType.Int32, item.ArticleID), 
                DALHelper.DBHelper.CreateInDbParameter("@Intro", DbType.String, item.Intro), 
                DALHelper.DBHelper.CreateInDbParameter("@Title", DbType.String, 50, item.Title), 
                DALHelper.DBHelper.CreateInDbParameter("@Url", DbType.String, item.Url),
                DALHelper.DBHelper.CreateInDbParameter("@FilePath", DbType.String, item.FilePath)};
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), cmdParms);
        }
    }
}

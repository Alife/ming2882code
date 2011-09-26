namespace SqlServerDAL
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class ArticleFileData : DALHelper
    {
        public int Delete(int ID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete Article_File ");
            builder.Append(" where ID=@ID ");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, ID) };
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), cmdParms);
        }
        private ArticleFile GetItem(DbDataReader dr, ArticleFile item)
        {
            item.ID = DALHelper.DBHelper.GetInt(dr["ID"]);
            item.ArticleID = DALHelper.DBHelper.GetInt(dr["ArticleID"]);
            item.FilePath = DALHelper.DBHelper.GetString(dr["FilePath"]);
            item.FileType = DALHelper.DBHelper.GetString(dr["FileType"]);
            item.FileName = DALHelper.DBHelper.GetString(dr["FileName"]);
            item.FileSize = DALHelper.DBHelper.GetDecimal(dr["FileSize"]);
            item.IsTop = DALHelper.DBHelper.GetBool(dr["IsTop"]);
            return item;
        }

        public ArticleFile GetItem(int _id, bool _istop)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID,ArticleID,FilePath,FileType,FileName,FileSize,IsTop from Article_File ");
            if (_istop)
                builder.Append(" where ArticleID=@ID and IsTop=1");
            else
                builder.Append(" where ID=@ID");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, _id) };
            ArticleFile item = null;
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            item = new ArticleFile();
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

        public List<ArticleFile> GetList(int articleID)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select ID,ArticleID,FilePath,FileType,FileName,FileSize,IsTop from Article_File where ArticleID={0}", articleID);
            builder.Append(" Order By ID Desc");
            List<ArticleFile> list = new List<ArticleFile>();
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            list.Add(GetItem(reader, new ArticleFile()));
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

        public int Insert(ArticleFile item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Article_File(");
            builder.Append("ArticleID,FilePath,FileType,FileName,FileSize,IsTop)");
            builder.Append(" values (");
            builder.Append("@ArticleID,@FilePath,@FileType,@FileName,@FileSize,@IsTop)");
            builder.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[] {
                DALHelper.DBHelper.CreateInDbParameter("@ArticleID", DbType.Int32, 4, item.ArticleID), 
                DALHelper.DBHelper.CreateInDbParameter("@FilePath", DbType.String, item.FilePath), 
                DALHelper.DBHelper.CreateInDbParameter("@FileType", DbType.String, item.FileType), 
                DALHelper.DBHelper.CreateInDbParameter("@FileName", DbType.String, item.FileName), 
                DALHelper.DBHelper.CreateInDbParameter("@FileSize", DbType.Decimal, 5, item.FileSize), 
                DALHelper.DBHelper.CreateInDbParameter("@IsTop", DbType.Boolean, 1, item.IsTop)};
            object obj2 = DALHelper.DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), cmdParms);
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }

        public int Update(ArticleFile item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Article_File set ");
            builder.Append("ArticleID=@ArticleID,");
            builder.Append("FilePath=@FilePath,");
            builder.Append("FileType=@FileType,");
            builder.Append("FileName=@FileName,");
            builder.Append("FileSize=@FileSize,");
            builder.Append("IsTop=@IsTop");
            builder.Append(" where ID=@ID ");
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, 4, item.ID), 
                DALHelper.DBHelper.CreateInDbParameter("@ArticleID", DbType.Int32, 4, item.ArticleID), 
                DALHelper.DBHelper.CreateInDbParameter("@FilePath", DbType.String, item.FilePath), 
                DALHelper.DBHelper.CreateInDbParameter("@FileType", DbType.String, item.FileType), 
                DALHelper.DBHelper.CreateInDbParameter("@FileName", DbType.String, item.FileName), 
                DALHelper.DBHelper.CreateInDbParameter("@FileSize", DbType.Decimal, 5, item.FileSize), 
                DALHelper.DBHelper.CreateInDbParameter("@IsTop", DbType.Boolean, 1, item.IsTop)
            };
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), cmdParms);
        }
    }
}

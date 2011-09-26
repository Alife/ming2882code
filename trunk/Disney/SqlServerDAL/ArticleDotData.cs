namespace SqlServerDAL
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class ArticleDotData : DALHelper
    {
        public int Save(ArticleDot item)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("if not exists (select id from Article_Dot where CommentID=@CommentID and UserID=@UserID) \r\n");
            builder.Append("begin \r\n");
            builder.Append("insert into Article_Dot(CommentID,UserID,Dot) values (@CommentID,@UserID,@Dot); \r\n ");
            builder.Append("end \r\n");
            DbParameter[] cmdParms = new DbParameter[] {
                DALHelper.DBHelper.CreateInDbParameter("@CommentID", DbType.Int32, item.CommentID), 
                DALHelper.DBHelper.CreateInDbParameter("@UserID", DbType.Int32, item.UserID), 
                DALHelper.DBHelper.CreateInDbParameter("@Dot", DbType.Int32, item.Dot)};
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), cmdParms);
        }
        public int GetCount(int articleID, int dot)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select count(id) from Article_Dot where CommentID in (select ID from Article_Comment where articleID={0}) and Dot={1}", articleID, dot);
            object obj = DALHelper.DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), null);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }
    }
}

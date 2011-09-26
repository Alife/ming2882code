using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;
using Models.Enums;
using DBUtility;

namespace SqlServerDAL
{
    public class d_KitPhotoReturnData : DALHelper
    {
        public int Insert(d_KitPhotoReturn model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO d_KitPhotoReturn(");
            strSql.Append("KitPhotoID, UserID, KitClassID, KitChildID, FileName, Intro, QuestionType, Tw)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_KitPhotoID, @in_UserID, @in_KitClassID, @in_KitChildID, @in_FileName, @in_Intro, @in_QuestionType, @in_Tw)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_KitPhotoID", DbType.Int32, model.KitPhotoID),
                DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
                DBHelper.CreateInDbParameter("@in_KitClassID", DbType.Int32, model.KitClassID),
                DBHelper.CreateInDbParameter("@in_KitChildID", DbType.Int32, model.KitChildID),
                DBHelper.CreateInDbParameter("@in_FileName", DbType.String, model.FileName),
                DBHelper.CreateInDbParameter("@in_Intro", DbType.String, model.Intro),
                DBHelper.CreateInDbParameter("@in_QuestionType", DbType.Int32, model.QuestionType),
                DBHelper.CreateInDbParameter("@in_Tw", DbType.String, model.Tw)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }
        public int Insert(List<d_KitPhotoReturn> list)
        {
            int revalue = 0;
            if (list.Count > 0)
            {
                List<CommandInfo> cmdList = new List<CommandInfo>();
                foreach (var model in list)
                {
                    StringBuilder strSql = new StringBuilder();
                    if (model.ID == 0)
                    {
                        strSql.Append("INSERT INTO d_KitPhotoReturn(");
                        strSql.Append("KitPhotoID, UserID, KitClassID, KitChildID, FileName, Intro, QuestionType, Tw)");
                        strSql.Append(" VALUES (");
                        strSql.Append("@in_KitPhotoID, @in_UserID, @in_KitClassID, @in_KitChildID, @in_FileName, @in_Intro, @in_QuestionType, @in_Tw)");
                        strSql.Append("\r\n");
                    }
                    DbParameter[] cmdParms = new DbParameter[]{
                        DBHelper.CreateInDbParameter("@in_KitPhotoID", DbType.Int32, model.KitPhotoID),
                        DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
                        DBHelper.CreateInDbParameter("@in_KitClassID", DbType.Int32, model.KitClassID),
                        DBHelper.CreateInDbParameter("@in_KitChildID", DbType.Int32, model.KitChildID),
                        DBHelper.CreateInDbParameter("@in_FileName", DbType.String, model.FileName),
                        DBHelper.CreateInDbParameter("@in_Intro", DbType.String, model.Intro),
                        DBHelper.CreateInDbParameter("@in_QuestionType", DbType.Int32, model.QuestionType),
                        DBHelper.CreateInDbParameter("@in_Tw", DbType.String, model.Tw)};
                    cmdList.Add(new CommandInfo(strSql.ToString(), cmdParms, EffentNextType.ExcuteEffectRows));
                }
                DbConnection conn = DBHelper.CreateConnection();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                DbTransaction tran = conn.BeginTransaction();
                revalue = DBHelper.ExecuteNonQuery(tran, CommandType.Text, cmdList);
            }
            return revalue;
        }

        public int Update(d_KitPhotoReturn model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE d_KitPhotoReturn SET ");
            strSql.Append("KitPhotoID=@in_KitPhotoID,");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("KitClassID=@in_KitClassID,");
            strSql.Append("KitChildID=@in_KitChildID,");
            strSql.Append("FileName=@in_FileName,");
            strSql.Append("Intro=@in_Intro,");
            strSql.Append("QuestionType=@in_QuestionType,");
            strSql.Append("Tw=@in_Tw");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_KitPhotoID", DbType.Int32, model.KitPhotoID),
                DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
                DBHelper.CreateInDbParameter("@in_KitClassID", DbType.Int32, model.KitClassID),
                DBHelper.CreateInDbParameter("@in_KitChildID", DbType.Int32, model.KitChildID),
                DBHelper.CreateInDbParameter("@in_FileName", DbType.String, model.FileName),
                DBHelper.CreateInDbParameter("@in_Intro", DbType.String, model.Intro),
                DBHelper.CreateInDbParameter("@in_QuestionType", DbType.Int32, model.QuestionType),
                DBHelper.CreateInDbParameter("@in_Tw", DbType.String, model.Tw),
                DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }
        public int Delete(List<string> ids)
        {
            StringBuilder strSql = new StringBuilder();
            if (ids.Count > 0)
            {
                foreach (string id in ids)
                    strSql.AppendFormat("delete d_KitPhotoReturn WHERE ID={0};\r\n", id);
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }

        public d_KitPhotoReturn GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_KitPhotoReturn ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            d_KitPhotoReturn item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public DataTable GetList(int pageIndex, int pageSize, ref int records, int totolid, string arter, string beginTime, string endTime)
        {
            string query = string.Format("where kw.State={0}", (int)KitPhotoState.MonthEnd);
            List<DbParameter> para = new List<DbParameter>();
            para.Add(DBHelper.CreateInDbParameter("@PageIndex", DbType.Int32, pageIndex + 1));
            para.Add(DBHelper.CreateInDbParameter("@PageSize", DbType.Int32, pageIndex + pageSize));
            if (!string.IsNullOrEmpty(arter))
            {
                query += " and (u.TrueName like '%'+@arter+'%' or u.UserCode like '%'+@arter+'%') ";
                para.Add(DBHelper.CreateInDbParameter("@arter", DbType.String, arter));
            }
            if (!string.IsNullOrEmpty(beginTime) && !string.IsNullOrEmpty(endTime))
            {
                query += " AND FinishTime BETWEEN @beginTime AND @endTime ";
                para.Add(DBHelper.CreateInDbParameter("@beginTime", DbType.DateTime, DateTime.Parse(beginTime)));
                para.Add(DBHelper.CreateInDbParameter("@endTime", DbType.DateTime, Convert.ToDateTime(endTime).AddDays(1)));
            }
            DbParameter[] cmdParms = para.ToArray();
            string sql = @"select kp.*,WorkName,u.TrueName,kpt.Name as KitPhotoType,FinishTime{0} 
                            from d_KitPhotoReturn as kp 
                            inner join d_KitWork as kw on kw.ID=kp.KitWorkID 
                            inner join t_User as u on u.ID=kp.ArterID 
                            inner join d_KitPhotoReturnType as kpt on kpt.ID=kp.KitPhotoTypeID {1}";
            string strSql = string.Format(@"select count(1) from ({0}) as temptable", string.Format(sql, "", query));
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql, cmdParms);
            DataTable dt = new DataTable();
            if (obj != null && !obj.Equals(0))
            {
                records = int.Parse(obj.ToString());
                strSql = string.Format(@"SELECT * FROM ({0}) as temptable WHERE rowNum BETWEEN @PageIndex and @PageSize",
                                        string.Format(sql, ",ROW_NUMBER() Over(order by WorkName) as rowNum", query));
                dt = DBHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), cmdParms).Tables[0];
            }
            return dt;
        }
        #region 私有
        private d_KitPhotoReturn GetItem(d_KitPhotoReturn model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new d_KitPhotoReturn();
                        GetModel(model, dr);
                    }
                }
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                    dr.Dispose();
                }
            }
            return model;
        }
        private void GetModel(d_KitPhotoReturn model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.KitPhotoID = DBHelper.GetInt(dr["KitPhotoID"]);
            model.UserID = DBHelper.GetInt(dr["UserID"]);
            model.KitClassID = DBHelper.GetInt(dr["KitClassID"]);
            model.KitChildID = DBHelper.GetIntByNull(dr["KitChildID"]);
            model.FileName = DBHelper.GetString(dr["FileName"]);
            model.Intro = DBHelper.GetString(dr["Intro"]);
            model.QuestionType = DBHelper.GetIntByNull(dr["QuestionType"]);
            model.Tw = DBHelper.GetString(dr["Tw"]);
        }
        private List<d_KitPhotoReturn> GetItem(List<d_KitPhotoReturn> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        d_KitPhotoReturn model = new d_KitPhotoReturn();
                        GetModel(model, dr);
                        list.Add(model);
                    }
                }
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                    dr.Dispose();
                }
            }
            return list;
        }
        #endregion
    }
}
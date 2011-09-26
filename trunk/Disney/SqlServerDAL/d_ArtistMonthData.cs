using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;
using DBUtility;

namespace SqlServerDAL
{
    public class d_ArtistMonthData : DALHelper
    {
        public int Insert(d_ArtistMonth model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO d_ArtistMonth(");
            strSql.Append("UserID,KitPhotoID,PeopleNum,PhotoNum,TeacherNum,Amount,Amt,BalanceTime,Remark)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_UserID,@in_KitPhotoID,@in_PeopleNum,@in_PhotoNum,@in_TeacherNum,@in_Amount,@in_Amt,@in_BalanceTime,@in_Remark)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
                DBHelper.CreateInDbParameter("@in_KitPhotoID", DbType.Int32, model.KitPhotoID),
                DBHelper.CreateInDbParameter("@in_PeopleNum", DbType.Int32, model.PeopleNum),
                DBHelper.CreateInDbParameter("@in_PhotoNum", DbType.Int32, model.PhotoNum),
                DBHelper.CreateInDbParameter("@in_TeacherNum", DbType.Int32, model.TeacherNum),
                DBHelper.CreateInDbParameter("@in_Amount", DbType.Decimal, model.Amount),
                DBHelper.CreateInDbParameter("@in_Amt", DbType.Decimal, model.Amt),
                DBHelper.CreateInDbParameter("@in_BalanceTime", DbType.DateTime, model.BalanceTime),
                DBHelper.CreateInDbParameter("@in_Remark", DbType.String, model.Remark)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }
        public int Insert(List<d_ArtistMonth> list)
        {
            int revalue = 0;
            if (list.Count > 0)
            {
                List<CommandInfo> cmdList = new List<CommandInfo>();
                foreach (var model in list)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("INSERT INTO d_ArtistMonth(");
                    strSql.Append("UserID,KitPhotoID,PeopleNum,PhotoNum,TeacherNum,Amount,Amt,BalanceTime,State,Remark)");
                    strSql.Append(" VALUES (");
                    strSql.Append("@in_UserID,@in_KitPhotoID,@in_PeopleNum,@in_PhotoNum,@in_TeacherNum,@in_Amount,@in_Amt,@in_BalanceTime,@in_State,@in_Remark)");
                    strSql.Append("\r\n");
                    DbParameter[] cmdParms = new DbParameter[]{
                        DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
                        DBHelper.CreateInDbParameter("@in_KitPhotoID", DbType.Int32, model.KitPhotoID),
                        DBHelper.CreateInDbParameter("@in_PeopleNum", DbType.Int32, model.PeopleNum),
                        DBHelper.CreateInDbParameter("@in_PhotoNum", DbType.Int32, model.PhotoNum),
                        DBHelper.CreateInDbParameter("@in_TeacherNum", DbType.Int32, model.TeacherNum),
                        DBHelper.CreateInDbParameter("@in_Amount", DbType.Decimal, model.Amount),
                        DBHelper.CreateInDbParameter("@in_Amt", DbType.Decimal, model.Amt),
                        DBHelper.CreateInDbParameter("@in_BalanceTime", DbType.DateTime, model.BalanceTime),
                        DBHelper.CreateInDbParameter("@in_State", DbType.Int32, model.State),
                        DBHelper.CreateInDbParameter("@in_Remark", DbType.String, model.Remark)};
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
        public int Update(d_ArtistMonth model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE d_ArtistMonth SET ");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("KitPhotoID=@in_KitPhotoID,");
            strSql.Append("PeopleNum=@in_PeopleNum,");
            strSql.Append("PhotoNum=@in_PhotoNum,");
            strSql.Append("TeacherNum=@in_TeacherNum,");
            strSql.Append("Amount=@in_Amount,");
            strSql.Append("Amt=@in_Amt,");
            strSql.Append("BalanceTime=@in_BalanceTime,");
            strSql.Append("State=@in_State,");
            strSql.Append("Remark=@in_Remark");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
                DBHelper.CreateInDbParameter("@in_KitPhotoID", DbType.Int32, model.KitPhotoID),
                DBHelper.CreateInDbParameter("@in_PeopleNum", DbType.Int32, model.PeopleNum),
                DBHelper.CreateInDbParameter("@in_PhotoNum", DbType.Int32, model.PhotoNum),
                DBHelper.CreateInDbParameter("@in_TeacherNum", DbType.Int32, model.TeacherNum),
                DBHelper.CreateInDbParameter("@in_Amount", DbType.Decimal, model.Amount),
                DBHelper.CreateInDbParameter("@in_Amt", DbType.Decimal, model.Amt),
                DBHelper.CreateInDbParameter("@in_BalanceTime", DbType.DateTime, model.BalanceTime),
                DBHelper.CreateInDbParameter("@in_State", DbType.Int32, model.State),
                DBHelper.CreateInDbParameter("@in_Remark", DbType.String, model.Remark),
                DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }
        public int Update(List<string> ids)
        {
            StringBuilder strSql = new StringBuilder();
            if (ids.Count > 0)
            {
                DateTime dt = DateTime.Now;
                foreach (string id in ids)
                {
                    if (!string.IsNullOrEmpty(id))
                        strSql.AppendFormat("update d_ArtistMonth set State={1},BalanceTime='{2}' WHERE ID={0};\r\n", id, 2, dt);
                }
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }


        public int Delete(List<string> ids)
        {
            StringBuilder strSql = new StringBuilder();
            if (ids.Count > 0)
            {
                foreach (string id in ids)
                    strSql.AppendFormat("DELETE FROM d_ArtistMonth WHERE ID={0};\r\n", id);
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }

        public d_ArtistMonth GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_ArtistMonth ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            d_ArtistMonth item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public List<d_ArtistMonth> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_ArtistMonth ");
            strSql.Append(" Order by OrderID");
            List<d_ArtistMonth> list = new List<d_ArtistMonth>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
                GetItem(list, dr);
            return list;
        }
        public DataTable GetList(int pageIndex, int pageSize, ref int records, string arter, string beginTime, string endTime)
        {
            string query = string.Empty;
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
            string sql = @"select am.*,WorkName,u.TrueName,kpt.Name as KitPhotoType,FinishTime{0} 
                            from d_ArtistMonth as am 
                            inner join d_KitPhoto as kp on kp.ID=am.KitPhotoID 
                            inner join d_KitWork as kw on kw.ID=kp.KitWorkID 
                            inner join t_User as u on u.ID=am.UserID 
                            inner join d_KitPhotoType as kpt on kpt.ID=kp.KitPhotoTypeID where am.State=1{1}";
            string strSql = string.Format(@"select count(1) from ({0}) as temptable", string.Format(sql, "", query));
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql, cmdParms);
            DataTable dt = new DataTable();
            if (obj != null && !obj.Equals(0))
            {
                records = int.Parse(obj.ToString());
                strSql = string.Format(@"SELECT * FROM ({0}) as temptable WHERE rowNum BETWEEN @PageIndex and @PageSize",
                                        string.Format(sql, ",ROW_NUMBER() Over(order by BalanceTime) as rowNum", query));
                dt = DBHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), cmdParms).Tables[0];
            }
            return dt;
        }
        #region 私有
        private d_ArtistMonth GetItem(d_ArtistMonth model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new d_ArtistMonth();
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
        private void GetModel(d_ArtistMonth model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.UserID = DBHelper.GetInt(dr["UserID"]);
            model.KitPhotoID = DBHelper.GetInt(dr["KitPhotoID"]);
            model.PeopleNum = DBHelper.GetInt(dr["PeopleNum"]);
            model.PhotoNum = DBHelper.GetInt(dr["PhotoNum"]);
            model.TeacherNum = DBHelper.GetInt(dr["TeacherNum"]);
            model.Amount = DBHelper.GetDecimal(dr["Amount"]);
            model.Amt = DBHelper.GetDecimal(dr["Amt"]);
            model.BalanceTime = DBHelper.GetDateTimeByNull(dr["BalanceTime"]);
            model.State = DBHelper.GetInt(dr["State"]);
            model.Remark = DBHelper.GetString(dr["Remark"]);
        }
        private List<d_ArtistMonth> GetItem(List<d_ArtistMonth> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        d_ArtistMonth model = new d_ArtistMonth();
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
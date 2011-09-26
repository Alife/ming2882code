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
    public class d_TotolMonthData : DALHelper
    {
        public int Insert(d_TotolMonth model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO d_TotolMonth(");
            strSql.Append("OrderName,BeginTime,EndTime,State,BalanceTime,BalanceAccount,ArterBalanceTime)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_OrderName,@in_BeginTime,@in_EndTime,@in_State,@in_BalanceTime,@in_BalanceAccount,@in_ArterBalanceTime)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_OrderName", DbType.String, model.OrderName),
                DBHelper.CreateInDbParameter("@in_BeginTime", DbType.DateTime, model.BeginTime),
                DBHelper.CreateInDbParameter("@in_EndTime", DbType.DateTime, model.EndTime),
                DBHelper.CreateInDbParameter("@in_State", DbType.Int32, model.State),
                DBHelper.CreateInDbParameter("@in_BalanceTime", DbType.DateTime, model.BalanceTime),
                DBHelper.CreateInDbParameter("@in_BalanceAccount", DbType.Decimal, model.BalanceAccount),
                DBHelper.CreateInDbParameter("@in_ArterBalanceTime", DbType.DateTime, model.ArterBalanceTime)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }
        public int Update(d_TotolMonth model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE d_TotolMonth SET ");
            strSql.Append("OrderName=@in_OrderName,");
            strSql.Append("BeginTime=@in_BeginTime,");
            strSql.Append("EndTime=@in_EndTime,");
            strSql.Append("State=@in_State,");
            strSql.Append("BalanceTime=@in_BalanceTime,");
            strSql.Append("BalanceAccount=@in_BalanceAccount,");
            strSql.Append("ArterBalanceTime=@in_ArterBalanceTime");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_OrderName", DbType.String, model.OrderName),
                DBHelper.CreateInDbParameter("@in_BeginTime", DbType.DateTime, model.BeginTime),
                DBHelper.CreateInDbParameter("@in_EndTime", DbType.DateTime, model.EndTime),
                DBHelper.CreateInDbParameter("@in_State", DbType.Int32, model.State),
                DBHelper.CreateInDbParameter("@in_BalanceTime", DbType.DateTime, model.BalanceTime),
                DBHelper.CreateInDbParameter("@in_BalanceAccount", DbType.Decimal, model.BalanceAccount),
                DBHelper.CreateInDbParameter("@in_ArterBalanceTime", DbType.DateTime, model.ArterBalanceTime),
                DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        public int Delete(List<string> ids)
        {
            StringBuilder strSql = new StringBuilder();
            if (ids.Count > 0)
            {
                foreach (string id in ids)
                    strSql.AppendFormat("DELETE FROM d_TotolMonth WHERE ID={0};\r\n", id);
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }

        public d_TotolMonth GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_TotolMonth ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            d_TotolMonth item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public d_TotolMonth GetItem()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT top 1 * FROM d_TotolMonth where state={0}", (int)Models.Enums.Balance.Normal);
            strSql.Append(" order by id desc");
            d_TotolMonth item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
                item = GetItem(item, dr);
            return item;
        }
        public List<d_TotolMonth> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_TotolMonth ");
            strSql.Append(" Order by ID desc");
            List<d_TotolMonth> list = new List<d_TotolMonth>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
                GetItem(list, dr);
            return list;
        }
        public DataTable GetList(int pageIndex, int pageSize, ref int records, int arterid, string state, string beginTime, string endTime)
        {
            string query = " where 1=1 ";
            List<DbParameter> para = new List<DbParameter>();
            para.Add(DBHelper.CreateInDbParameter("@PageIndex", DbType.Int32, pageIndex + 1));
            para.Add(DBHelper.CreateInDbParameter("@PageSize", DbType.Int32, pageIndex + pageSize));
            if (!string.IsNullOrEmpty(state))
            {
                string tempquery = string.Empty;
                foreach (string item in state.Split(','))
                    tempquery += string.Format("or tm.state='{0}' ", item);
                query += string.Format("and ({0})", tempquery.Substring(2));
            }
            if (arterid > 0)
            {
                query += " AND kp.ArterID=@ArterID ";
                para.Add(DBHelper.CreateInDbParameter("@ArterID", DbType.Int32, arterid));
            }
            if (!string.IsNullOrEmpty(beginTime) && !string.IsNullOrEmpty(endTime))
            {
                query += " AND (BeginTime<=@beginTime AND EndTime>=@endTime) ";
                para.Add(DBHelper.CreateInDbParameter("@beginTime", DbType.DateTime, DateTime.Parse(beginTime)));
                para.Add(DBHelper.CreateInDbParameter("@endTime", DbType.DateTime, Convert.ToDateTime(endTime).AddDays(1)));
            }
            DbParameter[] cmdParms = para.ToArray();
            string sql = @"select tm.ID,tm.OrderName,tm.BeginTime,tm.EndTime,tm.State,tm.BalanceTime,tm.BalanceAccount,tm.ArterBalanceTime
                            ,sum((case when(Category=1 or Category=6)then cast(PhotoNum as decimal(38,2))/2
                                when(Category=3 and kpt.ID!=3)then cast(cast((kp.PeopleNum+TeacherNum)as decimal(38,2))/2 as decimal(38,2))
                                when(Category=3 and kpt.ID=3)then cast(cast((kp.PeopleNum*2+TeacherNum)as decimal(38,2))/2 as decimal(38,2))
                                else kp.PhotoNum end)
                                )as PhotoNum
                            ,sum(Amount)as Amount,sum(Amt)as Amt{0} 
                            from d_TotolMonth as tm
                            left join d_KitWork as kw on kw.TotolMonthID=tm.ID
                            left join d_KitPhoto as kp on kp.KitWorkID=kw.ID
                            left join d_KitPhotoType as kpt on kp.KitPhotoTypeID=kpt.ID
                            {1}
                            group by tm.ID,tm.OrderName,tm.BeginTime,tm.EndTime,tm.State,tm.BalanceTime,tm.BalanceAccount,tm.ArterBalanceTime";
            string strSql = string.Format(@"select count(1) from ({0}) as temptable", string.Format(sql, "", query));
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql, cmdParms);
            DataTable dt = new DataTable();
            if (obj != null && !obj.Equals(0))
            {
                records = int.Parse(obj.ToString());
                strSql = string.Format(@"SELECT * FROM ({0}) as temptable WHERE rowNum BETWEEN @PageIndex and @PageSize",
                                        string.Format(sql, ",ROW_NUMBER() Over(order by tm.ID desc) as rowNum", query));
                dt = DBHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), cmdParms).Tables[0];
            }
            return dt;
        }
        #region 私有
        private d_TotolMonth GetItem(d_TotolMonth model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new d_TotolMonth();
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
        private void GetModel(d_TotolMonth model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.OrderName = DBHelper.GetString(dr["OrderName"]);
            model.BeginTime = DBHelper.GetDateTime(dr["BeginTime"]);
            model.EndTime = DBHelper.GetDateTime(dr["EndTime"]);
            model.State = DBHelper.GetInt(dr["State"]);
            model.BalanceTime = DBHelper.GetDateTimeByNull(dr["BalanceTime"]);
            model.BalanceAccount = DBHelper.GetDecimalByNull(dr["BalanceAccount"]);
            model.ArterBalanceTime = DBHelper.GetDateTimeByNull(dr["ArterBalanceTime"]);
        }
        private List<d_TotolMonth> GetItem(List<d_TotolMonth> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        d_TotolMonth model = new d_TotolMonth();
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
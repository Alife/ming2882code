using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Collections;
using Models;
using DBUtility;

namespace SqlServerDAL
{
    public class t_UserPointData : DALHelper
    {
        public int Insert(t_UserPoint model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO t_UserPoint(");
            strSql.Append("order_id,trd_dtm,user_id,trd_qty,point,daynum,valid_time,isvalid,reason)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_order_id,@in_trd_dtm,@in_user_id,@in_trd_qty,@in_point,@in_daynum,@in_valid_time,@in_isvalid,@in_reason)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = {
				DBHelper.CreateInDbParameter("@in_order_id", DbType.Int32, model.order_id),
				DBHelper.CreateInDbParameter("@in_trd_dtm", DbType.DateTime, model.trd_dtm),
				DBHelper.CreateInDbParameter("@in_user_id", DbType.Int32, model.user_id),
				DBHelper.CreateInDbParameter("@in_trd_qty", DbType.Decimal, model.trd_qty),
				DBHelper.CreateInDbParameter("@in_point", DbType.Decimal, model.point),
				DBHelper.CreateInDbParameter("@in_daynum", DbType.Int32, model.daynum),
				DBHelper.CreateInDbParameter("@in_isvalid", DbType.Boolean, model.isvalid),
				DBHelper.CreateInDbParameter("@in_valid_time", DbType.DateTime, model.valid_time),
				DBHelper.CreateInDbParameter("@in_reason", DbType.Int32, model.reason)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }
        public int Insert(List<t_UserPoint> list)
        {
            int revalue = 0;
            if (list.Count > 0)
            {
                List<CommandInfo> cmdList = new List<CommandInfo>();
                foreach (t_UserPoint model in list)
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("INSERT INTO t_UserPoint(");
                    strSql.Append("order_id,trd_dtm,user_id,trd_qty,point,daynum,valid_time,isvalid,reason)");
                    strSql.Append(" VALUES (");
                    strSql.Append("@in_order_id,@in_trd_dtm,@in_user_id,@in_trd_qty,@in_point,@in_daynum,@in_valid_time,@in_isvalid,@in_reason);\r\n");
                    DbParameter[] cmdParms = {
				    DBHelper.CreateInDbParameter("@in_order_id", DbType.Int32, model.order_id),
				    DBHelper.CreateInDbParameter("@in_trd_dtm", DbType.DateTime, model.trd_dtm),
				    DBHelper.CreateInDbParameter("@in_user_id", DbType.Int32, model.user_id),
				    DBHelper.CreateInDbParameter("@in_trd_qty", DbType.Decimal, model.trd_qty),
				    DBHelper.CreateInDbParameter("@in_point", DbType.Decimal, model.point),
				    DBHelper.CreateInDbParameter("@in_daynum", DbType.Int32, model.daynum),
				    DBHelper.CreateInDbParameter("@in_isvalid", DbType.Boolean, model.isvalid),
				    DBHelper.CreateInDbParameter("@in_valid_time", DbType.DateTime, model.valid_time),
				    DBHelper.CreateInDbParameter("@in_reason", DbType.Int32, model.reason)};
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
        public int Update(t_UserPoint model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE t_UserPoint SET ");
            strSql.Append("order_id=@in_order_id,");
            strSql.Append("trd_dtm=@in_trd_dtm,");
            strSql.Append("user_id=@in_user_id,");
            strSql.Append("trd_qty=@in_trd_qty,");
            strSql.Append("point=@in_point,");
            strSql.Append("daynum=@in_daynum,");
            strSql.Append("isvalid=@in_isvalid,");
            strSql.Append("valid_time=@in_valid_time,");
            strSql.Append("reason=@in_reason");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = {
				DBHelper.CreateInDbParameter("@in_order_id", DbType.Int32, model.order_id),
				DBHelper.CreateInDbParameter("@in_trd_dtm", DbType.DateTime, model.trd_dtm),
				DBHelper.CreateInDbParameter("@in_user_id", DbType.Int32, model.user_id),
				DBHelper.CreateInDbParameter("@in_trd_qty", DbType.Decimal, model.trd_qty),
				DBHelper.CreateInDbParameter("@in_point", DbType.Decimal, model.point),
                DBHelper.CreateInDbParameter("@in_daynum", DbType.Int32, model.daynum),
			    DBHelper.CreateInDbParameter("@in_isvalid", DbType.Boolean, model.isvalid),
                DBHelper.CreateInDbParameter("@in_valid_time", DbType.DateTime, model.valid_time),
				DBHelper.CreateInDbParameter("@in_reason", DbType.Int32, model.reason),
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public t_UserPoint GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_UserPoint ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            t_UserPoint item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            item = GetItem(new t_UserPoint(), dr);
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
                return item;
            }
        }
        public int Update(int order_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("with temp as ( ");
            //strSql.Append(" select *, rowNum=ROW_NUMBER() Over(order by  ID ) from t_UserPoint ");
            //strSql.Append(" where point=@point AND isvalid=@isvalid) ");
            //strSql.Append(" update temp ");
            //strSql.Append(" set isvalid=1,reason=5 where rowNum<=@num ");
            strSql.Append("update t_UserPoint set isvalid=1 where order_id=@order_id");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@order_id", DbType.Int32,order_id)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }
        public int GetUserPoint(int uid)
        {
            string strSql = "select sum(point) from t_UserPoint where user_id=" + uid + "";
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), null);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }

        public t_UserPointList GetList(int pageIndex, int pageSize, string trueName, string userCode)
        {
            string query = string.Empty, order = string.Empty;
            List<DbParameter> para = new List<DbParameter>();
            para.Add(DBHelper.CreateInDbParameter("@PageIndex", DbType.Int32, pageIndex + 1));
            para.Add(DBHelper.CreateInDbParameter("@PageSize", DbType.Int32, pageIndex + pageSize));
            query += " and isvalid=1 ";
            if (!string.IsNullOrEmpty(trueName))
            {
                query += " and user_id in (select ID from t_user where TrueName like '%'+@TrueName+'%') ";
                para.Add(DBHelper.CreateInDbParameter("@trueName", DbType.String, trueName));
            }
            if (!string.IsNullOrEmpty(userCode))
            {
                query += " and user_id in (select ID from t_user where UserCode=@userCode) ";
                para.Add(DBHelper.CreateInDbParameter("@userCode", DbType.String, userCode));
            }
            order = "order by ID desc ";
            DbParameter[] cmdParms = para.ToArray();
            t_UserPointList list = new t_UserPointList();
            string strSql = string.Format("select count(1) from t_UserPoint where 1=1 {0}", query);
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql, cmdParms);
            if (obj != null)
                list.records = int.Parse(obj.ToString());
            else
                return list;
            if (list.records == 0)
                return list;

            strSql = @"SELECT ID,order_id,trd_dtm,user_id,trd_qty,point,daynum,valid_time,isvalid,reason
                       FROM 
                            (select ID,order_id,trd_dtm,user_id,trd_qty,point,daynum,valid_time,isvalid,reason
                        ,ROW_NUMBER() Over({0}) as rowNum from t_UserPoint where 1=1 {1}) as temptable
                       WHERE rowNum BETWEEN @PageIndex and @PageSize";
            strSql = string.Format(strSql, order, query);
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            list.data.Add(GetItem(new t_UserPoint(), dr));
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
            }
            return list;
        }


        private t_UserPoint GetItem(t_UserPoint model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.order_id = DBHelper.GetInt(dr["order_id"]);
            model.trd_dtm = DBHelper.GetDateTime(dr["trd_dtm"]);
            model.user_id = DBHelper.GetInt(dr["user_id"]);
            model.trd_qty = DBHelper.GetDecimal(dr["trd_qty"]);
            model.point = DBHelper.GetDecimal(dr["point"]);
            model.daynum = DBHelper.GetInt(dr["daynum"]);
            model.isvalid = DBHelper.GetBool(dr["isvalid"]);
            model.valid_time = DBHelper.GetDateTime(dr["valid_time"]);
            model.reason = DBHelper.GetInt(dr["reason"]);
            return model;
        }

        public int Delete(List<string> ID)
        {
            string strSql = string.Empty;
            foreach (string item in ID)
                strSql += string.Format("DELETE FROM t_UserPoint WHERE ID={0};\r\n", item);
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }

        public int Delete(List<int> orderid)
        {
            StringBuilder strSql = new StringBuilder();
            foreach (int item in orderid)
                strSql.AppendFormat(@"insert into t_UserPoint (order_id,trd_dtm,user_id,trd_qty,point,daynum,valid_time,isvalid,reason)
                                (select order_id,getdate(),user_id,trd_qty,-point,0,getdate(),1,2 from t_UserPoint where order_id={0});", item);
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }
    }
}

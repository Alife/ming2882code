using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Models;

namespace SqlServerDAL
{
    public class sys_LogData : DALHelper
    {
        public int Insert(sys_Log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO sys_Log(");
            strSql.Append("UserID,IP,LogTime,OpID,ObjCode,Content)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_UserID,@in_IP,@in_LogTime,@in_OpID,@in_ObjCode,@in_Content)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
                DBHelper.CreateInDbParameter("@in_IP", DbType.String, model.IP),
                DBHelper.CreateInDbParameter("@in_LogTime", DbType.DateTime, model.LogTime),
                DBHelper.CreateInDbParameter("@in_OpID", DbType.Int32, model.OpID),
                DBHelper.CreateInDbParameter("@in_ObjCode", DbType.String, model.ObjCode),
                DBHelper.CreateInDbParameter("@in_Content", DbType.String, model.Content)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }

        public int Update(sys_Log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE sys_Log SET ");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("IP=@in_IP,");
            strSql.Append("LogTime=@in_LogTime,");
            strSql.Append("OpID=@in_OpID,");
            strSql.Append("ObjCode=@in_ObjCode,");
            strSql.Append("Content=@in_Content");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
                DBHelper.CreateInDbParameter("@in_IP", DbType.String, model.IP),
                DBHelper.CreateInDbParameter("@in_LogTime", DbType.DateTime, model.LogTime),
                DBHelper.CreateInDbParameter("@in_OpID", DbType.Int32, model.OpID),
                DBHelper.CreateInDbParameter("@in_ObjCode", DbType.String, model.ObjCode),
                DBHelper.CreateInDbParameter("@in_Content", DbType.String, model.Content),
                DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }
        public List<sys_Log> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ID,UserID,'' as TrueName,IP,LogTime,OpID,ObjCode,Content FROM sys_Log ");
            List<sys_Log> list = new List<sys_Log>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            list.Add(GetItem(new sys_Log(), dr));
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
        }
        public sys_LogList GetList(int pageIndex, int pageSize, int categoryid, int opid, string usercode, string objcode, DateTime? startDate, DateTime? endDate)
        {
            string query = string.Empty, order = string.Empty;
            List<DbParameter> para = new List<DbParameter>();
            para.Add(DBHelper.CreateInDbParameter("@PageIndex", DbType.Int32, pageIndex + 1));
            para.Add(DBHelper.CreateInDbParameter("@PageSize", DbType.Int32, pageIndex + pageSize));
            query += "";
            order = "order by l.ID desc ";
            if (categoryid > 0 && opid==0)
            {
                query += " and opid in (select id from sys_logop where categoryid=@categoryid) ";
                para.Add(DBHelper.CreateInDbParameter("@categoryid", DbType.Int32, categoryid));
            }
            else if (categoryid > 0 && opid > 0)
            {
                query += " and opid=@opid ";
                para.Add(DBHelper.CreateInDbParameter("@opid", DbType.Int32, opid));
            }
            if (!string.IsNullOrEmpty(usercode))
            {
                query += " and usercode=@usercode ";
                para.Add(DBHelper.CreateInDbParameter("@usercode", DbType.String, usercode));
            }
            if (!string.IsNullOrEmpty(objcode))
            {
                query += " and objcode=@objcode ";
                para.Add(DBHelper.CreateInDbParameter("@objcode", DbType.String, objcode));
            }
            if (startDate.HasValue && endDate.HasValue)
            {
                query += " and LogTime between @startDate and @endDate  ";
                para.Add(DBHelper.CreateInDbParameter("@startDate", DbType.String, startDate));
                para.Add(DBHelper.CreateInDbParameter("@endDate", DbType.String, endDate.Value.AddDays(1)));
            }
            DbParameter[] cmdParms = para.ToArray();
            sys_LogList list = new sys_LogList();
            string strSql = string.Format("select count(1) from sys_Log as l left join t_user as u on u.id=l.userid where 1=1 {0}", query);
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql, cmdParms);
            if (obj != null)
                list.records = int.Parse(obj.ToString());
            else
                return list;
            if (list.records == 0)
                return list;
            strSql = @"SELECT ID,UserID,TrueName,IP,LogTime,OpID,ObjCode,Content
                       FROM 
                            (select l.ID,UserID,TrueName,IP,LogTime,OpID,ObjCode,Content
                        ,ROW_NUMBER() Over({0}) as rowNum from sys_Log as l
                        left join t_user as u on u.id=l.userid
                        where 1=1 {1}) as temptable
                       WHERE rowNum BETWEEN @PageIndex and @PageSize";
            strSql = string.Format(strSql, order, query);
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        list.data = new List<sys_Log>();
                        while (dr.Read())
                            list.data.Add(GetItem(new sys_Log(), dr));
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

        private sys_Log GetItem(sys_Log model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.UserID = DBHelper.GetInt(dr["UserID"]);
            model.TrueName = DBHelper.GetString(dr["TrueName"]);
            model.IP = DBHelper.GetString(dr["IP"]);
            model.LogTime = DBHelper.GetDateTime(dr["LogTime"]);
            model.OpID = DBHelper.GetInt(dr["OpID"]);
            model.ObjCode = DBHelper.GetString(dr["ObjCode"]);
            model.Content = DBHelper.GetString(dr["Content"]);
            return model;
        }

        public int Delete(List<string> ID)
        {
            string strSql = string.Empty;
            foreach (string item in ID)
                strSql += string.Format("DELETE FROM sys_Log WHERE ID={0};\r\n", item);
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }

        public sys_Log GetItem(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ID,UserID,'' as TrueName,IP,LogTime,OpID,ObjCode,Content FROM sys_Log ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, id)};
            sys_Log item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            item = GetItem(new sys_Log(), dr);
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
    }
}

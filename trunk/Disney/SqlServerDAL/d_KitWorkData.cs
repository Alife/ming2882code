using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;
using Models;
using Models.Enums;

namespace SqlServerDAL
{
    public class d_KitWorkData : DALHelper
    {
        public int Insert(d_KitWork model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO d_KitWork(");
            strSql.Append("WorkName,KitID,TotolMonthID,SendTime,PlanTime,BeginTime,FinishTime,ArterID,PeopleNum,");
            strSql.Append("State,ProofState,ProofBeginTime,ProofEndTime,ProofTime,IsCooperate,UploadFile,Type,Remark)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_WorkName,@in_KitID,@in_TotolMonthID,@in_SendTime,@in_PlanTime,@in_BeginTime,@in_FinishTime,@in_ArterID,@in_PeopleNum,");
            strSql.Append("@in_State,@in_ProofState,@in_ProofBeginTime,@in_ProofEndTime,@in_ProofTime,@in_IsCooperate,@in_UploadFile,@in_Type,@in_Remark)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_KitID", DbType.Int32, model.KitID),
                DBHelper.CreateInDbParameter("@in_TotolMonthID", DbType.Int32, model.TotolMonthID),
                DBHelper.CreateInDbParameter("@in_WorkName", DbType.String, model.WorkName),
                DBHelper.CreateInDbParameter("@in_SendTime", DbType.DateTime, model.SendTime),
                DBHelper.CreateInDbParameter("@in_PlanTime", DbType.DateTime, model.PlanTime),
                DBHelper.CreateInDbParameter("@in_BeginTime", DbType.DateTime, model.BeginTime),
                DBHelper.CreateInDbParameter("@in_FinishTime", DbType.DateTime, model.FinishTime),
                DBHelper.CreateInDbParameter("@in_ArterID", DbType.Int32, model.ArterID),
                DBHelper.CreateInDbParameter("@in_PeopleNum", DbType.Int32, model.PeopleNum),
                DBHelper.CreateInDbParameter("@in_State", DbType.Int32, model.State),
                DBHelper.CreateInDbParameter("@in_ProofState", DbType.Int32, model.ProofState),
                DBHelper.CreateInDbParameter("@in_ProofBeginTime", DbType.DateTime, model.ProofBeginTime),
                DBHelper.CreateInDbParameter("@in_ProofEndTime", DbType.DateTime, model.ProofEndTime),
                DBHelper.CreateInDbParameter("@in_ProofTime", DbType.DateTime, model.ProofTime),
                DBHelper.CreateInDbParameter("@in_IsCooperate", DbType.Boolean, model.IsCooperate),
                DBHelper.CreateInDbParameter("@in_UploadFile", DbType.String, model.UploadFile),
                DBHelper.CreateInDbParameter("@in_Type", DbType.Int32, model.Type),
                DBHelper.CreateInDbParameter("@in_Remark", DbType.String, model.Remark)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }

        public int Update(d_KitWork model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE d_KitWork SET ");
            strSql.Append("KitID=@in_KitID,");
            strSql.Append("TotolMonthID=@in_TotolMonthID,");
            strSql.Append("WorkName=@in_WorkName,");
            strSql.Append("SendTime=@in_SendTime,");
            strSql.Append("PlanTime=@in_PlanTime,");
            strSql.Append("FinishTime=@in_FinishTime,");
            strSql.Append("BeginTime=@in_BeginTime,");
            strSql.Append("ArterID=@in_ArterID,");
            strSql.Append("PeopleNum=@in_PeopleNum,");
            strSql.Append("State=@in_State,");
            strSql.Append("ProofBeginTime=@in_ProofBeginTime,");
            strSql.Append("ProofEndTime=@in_ProofEndTime,");
            strSql.Append("ProofState=@in_ProofState,");
            strSql.Append("ProofTime=@in_ProofTime,");
            strSql.Append("IsCooperate=@in_IsCooperate,");
            strSql.Append("UploadFile=@in_UploadFile,");
            strSql.Append("Type=@in_Type,");
            strSql.Append("Remark=@in_Remark");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_KitID", DbType.Int32, model.KitID),
                DBHelper.CreateInDbParameter("@in_TotolMonthID", DbType.Int32, model.TotolMonthID),
                DBHelper.CreateInDbParameter("@in_WorkName", DbType.String, model.WorkName),
                DBHelper.CreateInDbParameter("@in_SendTime", DbType.DateTime, model.SendTime),
                DBHelper.CreateInDbParameter("@in_PlanTime", DbType.DateTime, model.PlanTime),
                DBHelper.CreateInDbParameter("@in_BeginTime", DbType.DateTime, model.BeginTime),
                DBHelper.CreateInDbParameter("@in_FinishTime", DbType.DateTime, model.FinishTime),
                DBHelper.CreateInDbParameter("@in_ArterID", DbType.Int32, model.ArterID),
                DBHelper.CreateInDbParameter("@in_PeopleNum", DbType.Int32, model.PeopleNum),
                DBHelper.CreateInDbParameter("@in_State", DbType.Int32, model.State),
                DBHelper.CreateInDbParameter("@in_ProofState", DbType.Int32, model.ProofState),
                DBHelper.CreateInDbParameter("@in_ProofBeginTime", DbType.DateTime, model.ProofBeginTime),
                DBHelper.CreateInDbParameter("@in_ProofEndTime", DbType.DateTime, model.ProofEndTime),
                DBHelper.CreateInDbParameter("@in_ProofTime", DbType.DateTime, model.ProofTime),
                DBHelper.CreateInDbParameter("@in_IsCooperate", DbType.Boolean, model.IsCooperate),
                DBHelper.CreateInDbParameter("@in_UploadFile", DbType.String, model.UploadFile),
                DBHelper.CreateInDbParameter("@in_Type", DbType.Int32, model.Type),
                DBHelper.CreateInDbParameter("@in_Remark", DbType.String, model.Remark),
                DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }
        public int Update(List<string> ids, string uploadFile)
        {
            StringBuilder strSql = new StringBuilder();
            if (ids.Count > 0)
            {
                foreach (string id in ids)
                    strSql.AppendFormat("UPDATE d_KitWork SET  State={1},UploadFile='{2}' WHERE ID={0};\r\n"
                        , id, (int)KitPhotoState.Uploaded, uploadFile);
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }
        public int Update(List<string> ids, int totolID)
        {
            StringBuilder strSql = new StringBuilder();
            if (ids.Count > 0)
            {
                foreach (string id in ids)
                    strSql.AppendFormat("UPDATE d_KitWork SET State={2},totolMonthID={3} WHERE ID={0} and State={1};\r\n"
                        , id, (int)KitPhotoState.Uploaded, (int)KitPhotoState.MonthEnd, totolID);
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }
        public int Update(List<string> ids, string fid, string workname)
        {
            StringBuilder strSql = new StringBuilder();
            if (ids.Count > 0)
            {
                foreach (string id in ids)
                {
                    strSql.AppendFormat("UPDATE d_KitPhoto SET KitWorkID={1} WHERE KitWorkID={0};\r\n", id, fid);
                    strSql.AppendFormat("UPDATE d_KitQuestion SET KitWorkID={1} WHERE KitWorkID={0};\r\n", id, fid);
                    strSql.AppendFormat("delete d_KitWork WHERE ID={0};\r\n", id);
                }
                strSql.AppendFormat("UPDATE d_KitWork SET workname='{1}' WHERE ID={0};\r\n", fid, workname);
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
                {
                    strSql.AppendFormat("delete d_KitQuestion WHERE KitWorkID={0};\r\n", id);
                    strSql.AppendFormat("delete d_KitPhoto WHERE KitWorkID={0};\r\n", id);
                    strSql.AppendFormat("delete d_KitWork WHERE ID={0};\r\n", id);
                }
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }

        public d_KitWork GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_KitWork ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            d_KitWork item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public d_KitWork GetItemByFinish(int kitworkid, int userID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT top 1 * FROM d_KitWork ");
            strSql.Append(" WHERE ArterID=@userID and id!=@kitworkid");
            strSql.Append(" order by FinishTime desc");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@userID", DbType.Int32, userID),
				DBHelper.CreateInDbParameter("@kitworkid", DbType.Int32, kitworkid)};
            d_KitWork item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }

        public DataTable GetList(int pageIndex, int pageSize, ref int records, string keyword, string custom, string arter, string userID, string arterID,
            string state, string proofState, string sendBeginTime, string sendEndTime, string finishBeginTime, string finishEndTime)
        {
            string query = string.Empty;
            List<DbParameter> para = new List<DbParameter>();
            para.Add(DBHelper.CreateInDbParameter("@PageIndex", DbType.Int32, pageIndex + 1));
            para.Add(DBHelper.CreateInDbParameter("@PageSize", DbType.Int32, pageIndex + pageSize));
            query += string.Empty;
            if (!string.IsNullOrEmpty(keyword))
                query += " and " + DBHelper.GetSearchWord(keyword, "workname");
            if (!string.IsNullOrEmpty(userID))
                query += string.Format(" and UserID in ({0}) ", userID);
            if (!string.IsNullOrEmpty(arterID))
                query += string.Format(" and ArterID in ({0}) ", arterID);
            if (!string.IsNullOrEmpty(custom))
            {
                query += " and (Custom like '%'+@custom+'%' or CustomCode like '%'+@custom+'%') ";
                para.Add(DBHelper.CreateInDbParameter("@custom", DbType.String, custom));
            }
            if (!string.IsNullOrEmpty(arter))
            {
                query += " and (arter like '%'+@arter+'%' or ArterCode like '%'+@arter+'%') ";
                para.Add(DBHelper.CreateInDbParameter("@arter", DbType.String, arter));
            }
            if (!string.IsNullOrEmpty(state))
            {
                string tempquery = string.Empty;
                foreach (string item in state.Split(','))
                    tempquery += string.Format("or state='{0}' ", item);
                query += string.Format("and ({0})", tempquery.Substring(2));
            }
            if (!string.IsNullOrEmpty(proofState))
            {
                string tempquery = string.Empty;
                foreach (string item in proofState.Split(','))
                    tempquery += string.Format("or proofState='{0}' ", item);
                query += string.Format("and ({0})", tempquery.Substring(2));
            }
            if (!string.IsNullOrEmpty(sendBeginTime) && !string.IsNullOrEmpty(sendEndTime))
            {
                query += " AND SendTime BETWEEN @sendBeginTime AND @sendEndTime ";
                para.Add(DBHelper.CreateInDbParameter("@sendBeginTime", DbType.DateTime, sendBeginTime));
                para.Add(DBHelper.CreateInDbParameter("@sendEndTime", DbType.DateTime, Convert.ToDateTime(sendEndTime).AddDays(1)));
            }
            if (!string.IsNullOrEmpty(finishBeginTime) && !string.IsNullOrEmpty(finishEndTime))
            {
                query += " AND FinishTime BETWEEN @finishBeginTime AND @finishEndTime ";
                para.Add(DBHelper.CreateInDbParameter("@finishBeginTime", DbType.DateTime, finishBeginTime));
                para.Add(DBHelper.CreateInDbParameter("@finishEndTime", DbType.DateTime, Convert.ToDateTime(finishEndTime).AddDays(1)));
            }
            DbParameter[] cmdParms = para.ToArray();
            string sql = @"select *{0} from view_d_KitWork where 1=1{1}";
            string strSql = string.Format(@"select count(1) from ({0}) as temptable", string.Format(sql, "", query));
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql, cmdParms);
            DataTable dt = new DataTable();
            if (obj != null && !obj.Equals(0))
            {
                records = int.Parse(obj.ToString());
                strSql = string.Format(@"SELECT * FROM ({0}) as temptable WHERE rowNum BETWEEN @PageIndex and @PageSize",
                                        string.Format(sql, ",ROW_NUMBER() Over(order by ID desc) as rowNum", query));
                dt = DBHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), cmdParms).Tables[0];
            }
            return dt;
        }
        #region 私有
        private d_KitWork GetItem(d_KitWork model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new d_KitWork();
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
        private void GetModel(d_KitWork model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.WorkName = DBHelper.GetString(dr["WorkName"]);
            model.KitID = DBHelper.GetInt(dr["KitID"]);
            model.TotolMonthID = DBHelper.GetIntByNull(dr["TotolMonthID"]);
            model.SendTime = DBHelper.GetDateTimeByNull(dr["SendTime"]);
            model.PlanTime = DBHelper.GetDateTimeByNull(dr["PlanTime"]);
            model.BeginTime = DBHelper.GetDateTimeByNull(dr["BeginTime"]);
            model.FinishTime = DBHelper.GetDateTimeByNull(dr["FinishTime"]);
            model.ArterID = DBHelper.GetInt(dr["ArterID"]);
            model.PeopleNum = DBHelper.GetInt(dr["PeopleNum"]);
            model.State = DBHelper.GetInt(dr["State"]);
            model.ProofState = DBHelper.GetIntByNull(dr["ProofState"]);
            model.ProofBeginTime = DBHelper.GetDateTimeByNull(dr["ProofBeginTime"]);
            model.ProofEndTime = DBHelper.GetDateTimeByNull(dr["ProofEndTime"]);
            model.ProofTime = DBHelper.GetDateTimeByNull(dr["ProofTime"]);
            model.IsCooperate = DBHelper.GetBool(dr["IsCooperate"]);
            model.UploadFile = DBHelper.GetString(dr["UploadFile"]);
            model.Type = DBHelper.GetInt(dr["Type"]);
            model.Remark = DBHelper.GetString(dr["Remark"]);
        }
        private List<d_KitWork> GetItem(List<d_KitWork> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        d_KitWork model = new d_KitWork();
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
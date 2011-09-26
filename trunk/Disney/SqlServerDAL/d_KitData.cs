using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;

namespace SqlServerDAL
{
    public class d_KitData : DALHelper
    {
        public int Insert(d_Kit model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO d_Kit(");
            strSql.Append("Name,Code,UserID,State,CustomID,EndTime,CameraManID,CameraTime,");
            strSql.Append("KitTypeID,ClassTypeID,InsideMaterialID,Resolution,TemplateID,IsValid,Remark)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_Name,@in_Code,@in_UserID,@in_State,@in_CustomID,@in_EndTime,@in_CameraManID,");
            strSql.Append("@in_CameraTime,@in_KitTypeID,@in_ClassTypeID,@in_InsideMaterialID,");
            strSql.Append("@in_Resolution,@in_TemplateID,@in_IsValid,@in_Remark)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
                DBHelper.CreateInDbParameter("@in_Code", DbType.String, model.Code),
                DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
                DBHelper.CreateInDbParameter("@in_State", DbType.Int32, model.State),
                DBHelper.CreateInDbParameter("@in_CustomID", DbType.Int32, model.CustomID),
                DBHelper.CreateInDbParameter("@in_EndTime", DbType.DateTime, model.EndTime),
                DBHelper.CreateInDbParameter("@in_CameraManID", DbType.Int32, model.CameraManID),
                DBHelper.CreateInDbParameter("@in_CameraTime", DbType.DateTime, model.CameraTime),
                DBHelper.CreateInDbParameter("@in_KitTypeID", DbType.Int32, model.KitTypeID),
                DBHelper.CreateInDbParameter("@in_ClassTypeID", DbType.Int32, model.ClassTypeID),
                DBHelper.CreateInDbParameter("@in_InsideMaterialID", DbType.Int32, model.InsideMaterialID),
                DBHelper.CreateInDbParameter("@in_Resolution", DbType.String, model.Resolution),
                DBHelper.CreateInDbParameter("@in_TemplateID", DbType.Int32, model.TemplateID),
                DBHelper.CreateInDbParameter("@in_IsValid", DbType.Int32, model.IsValid),
                DBHelper.CreateInDbParameter("@in_Remark", DbType.String, model.Remark)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }

        public int Update(d_Kit model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE d_Kit SET ");
            strSql.Append("Name=@in_Name,");
            strSql.Append("Code=@in_Code,");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("State=@in_State,");
            strSql.Append("CustomID=@in_CustomID,");
            strSql.Append("EndTime=@in_EndTime,");
            strSql.Append("CameraManID=@in_CameraManID,");
            strSql.Append("CameraTime=@in_CameraTime,");
            strSql.Append("KitTypeID=@in_KitTypeID,");
            strSql.Append("ClassTypeID=@in_ClassTypeID,");
            strSql.Append("InsideMaterialID=@in_InsideMaterialID,");
            strSql.Append("Resolution=@in_Resolution,");
            strSql.Append("TemplateID=@in_TemplateID,");
            strSql.Append("IsValid=@in_IsValid,");
            strSql.Append("Remark=@in_Remark");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
                DBHelper.CreateInDbParameter("@in_Code", DbType.String, model.Code),
                DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
                DBHelper.CreateInDbParameter("@in_State", DbType.Int32, model.State),
                DBHelper.CreateInDbParameter("@in_CustomID", DbType.Int32, model.CustomID),
                DBHelper.CreateInDbParameter("@in_EndTime", DbType.DateTime, model.EndTime),
                DBHelper.CreateInDbParameter("@in_CameraManID", DbType.Int32, model.CameraManID),
                DBHelper.CreateInDbParameter("@in_CameraTime", DbType.DateTime, model.CameraTime),
                DBHelper.CreateInDbParameter("@in_KitTypeID", DbType.Int32, model.KitTypeID),
                DBHelper.CreateInDbParameter("@in_ClassTypeID", DbType.Int32, model.ClassTypeID),
                DBHelper.CreateInDbParameter("@in_InsideMaterialID", DbType.Int32, model.InsideMaterialID),
                DBHelper.CreateInDbParameter("@in_Resolution", DbType.String, model.Resolution),
                DBHelper.CreateInDbParameter("@in_TemplateID", DbType.Int32, model.TemplateID),
                DBHelper.CreateInDbParameter("@in_IsValid", DbType.Int32, model.IsValid),
                DBHelper.CreateInDbParameter("@in_Remark", DbType.String, model.Remark),
                DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        public int Delete(List<string> ids)
        {
            StringBuilder strSql = new StringBuilder();
            if (ids.Count > 0)
            {
                foreach (string id in ids)
                    strSql.AppendFormat("update d_Kit set IsValid=0 WHERE ID={0};\r\n", id);
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }

        public d_Kit GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_Kit ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            d_Kit item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }

        public d_Kit GetItem(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_Kit ");
            strSql.Append(" WHERE Code=@Code");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@Code", DbType.String, ID)};
            d_Kit item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public DataTable GetList(int pageIndex, int pageSize, ref int records, string keyword,
             string custom, string userID, string state, string beginTime, string endTime)
        {
            string query = string.Empty;
            List<DbParameter> para = new List<DbParameter>();
            para.Add(DBHelper.CreateInDbParameter("@PageIndex", DbType.Int32, pageIndex + 1));
            para.Add(DBHelper.CreateInDbParameter("@PageSize", DbType.Int32, pageIndex + pageSize));
            if (!string.IsNullOrEmpty(keyword))
            {
                query += " and (name like '%'+@keyword+'%' or code like '%'+@keyword+'%') ";
                para.Add(DBHelper.CreateInDbParameter("@keyword", DbType.String, keyword));
            }
            if (!string.IsNullOrEmpty(userID))
                query += string.Format(" and UserID in ({0}) ", userID);
            if (!string.IsNullOrEmpty(custom))
            {
                query += " and (Custom like '%'+@custom+'%' or CustomCode like '%'+@custom+'%') ";
                para.Add(DBHelper.CreateInDbParameter("@custom", DbType.String, custom));
            }
            if (!string.IsNullOrEmpty(state))
            {
                string tempquery = string.Empty;
                foreach (string item in state.Split(','))
                    tempquery += string.Format("or state='{0}' ", item);
                query += string.Format("and ({0})", tempquery.Substring(2));
            }
            if (!string.IsNullOrEmpty(beginTime) && !string.IsNullOrEmpty(endTime))
            {
                query += " AND EndTime BETWEEN @beginTime AND @endTime ";
                para.Add(DBHelper.CreateInDbParameter("@beginTime", DbType.DateTime, beginTime));
                para.Add(DBHelper.CreateInDbParameter("@endTime", DbType.DateTime, Convert.ToDateTime(endTime).AddDays(1)));
            }
            DbParameter[] cmdParms = para.ToArray();
            string sql = @"select *{0} from view_d_Kit where 1=1{1}";
            string strSql = string.Format(@"select count(1) from ({0}) as temptable", string.Format(sql, "", query));
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql, cmdParms);
            DataTable dt = new DataTable();
            if (obj != null && !obj.Equals(0))
            {
                records = int.Parse(obj.ToString());
                strSql = string.Format(@"SELECT * FROM ({0}) as temptable WHERE rowNum BETWEEN @PageIndex and @PageSize",
                                        string.Format(sql, ",ROW_NUMBER() Over(order by EndTime desc,ID desc) as rowNum", query));
                dt = DBHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), cmdParms).Tables[0];
            }
            return dt;
        }
        #region 私有
        private d_Kit GetItem(d_Kit model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new d_Kit();
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
        private void GetModel(d_Kit model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.Name = DBHelper.GetString(dr["Name"]);
            model.Code = DBHelper.GetString(dr["Code"]);
            model.UserID = DBHelper.GetInt(dr["UserID"]);
            model.State = DBHelper.GetInt(dr["State"]);
            model.CustomID = DBHelper.GetInt(dr["CustomID"]);
            model.EndTime = DBHelper.GetDateTime(dr["EndTime"]);
            model.CameraManID = DBHelper.GetIntByNull(dr["CameraManID"]);
            model.CameraTime = DBHelper.GetDateTimeByNull(dr["CameraTime"]);
            model.KitTypeID = DBHelper.GetInt(dr["KitTypeID"]);
            model.ClassTypeID = DBHelper.GetInt(dr["ClassTypeID"]);
            model.InsideMaterialID = DBHelper.GetInt(dr["InsideMaterialID"]);
            model.Resolution = DBHelper.GetString(dr["Resolution"]);
            model.TemplateID = DBHelper.GetIntByNull(dr["TemplateID"]);
            model.IsValid = DBHelper.GetBool(dr["IsValid"]);
            model.Remark = DBHelper.GetString(dr["Remark"]);
        }
        private List<d_Kit> GetItem(List<d_Kit> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        d_Kit model = new d_Kit();
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
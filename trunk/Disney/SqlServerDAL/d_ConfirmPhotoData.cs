using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;

namespace SqlServerDAL
{
    public class d_ConfirmPhotoData : DALHelper
    {
        public int Insert(d_ConfirmPhoto model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO d_ConfirmPhoto(");
            strSql.Append("UserID,KitWorkID,ConfirmTime,ConfirmMan,Tel,Remark)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_UserID,@in_KitWorkID,@in_ConfirmTime,@in_ConfirmMan,@in_Tel,@in_Remark)");
            strSql.Append(";select count(1) from d_ConfirmPhoto where KitWorkID=@in_KitWorkID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
                DBHelper.CreateInDbParameter("@in_KitWorkID", DbType.Int32, model.KitWorkID),
                DBHelper.CreateInDbParameter("@in_ConfirmTime", DbType.DateTime, model.ConfirmTime),
                DBHelper.CreateInDbParameter("@in_ConfirmMan", DbType.String, model.ConfirmMan),
                DBHelper.CreateInDbParameter("@in_Tel", DbType.String, model.Tel),
                DBHelper.CreateInDbParameter("@in_Remark", DbType.String, model.Remark)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }

        public int Update(d_ConfirmPhoto model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE d_ConfirmPhoto SET ");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("KitWorkID=@in_KitWorkID,");
            strSql.Append("ConfirmTime=@in_ConfirmTime,");
            strSql.Append("ConfirmMan=@in_ConfirmMan,");
            strSql.Append("Tel=@in_Tel,");
            strSql.Append("Remark=@in_Remark");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
                DBHelper.CreateInDbParameter("@in_KitWorkID", DbType.Int32, model.KitWorkID),
                DBHelper.CreateInDbParameter("@in_ConfirmTime", DbType.DateTime, model.ConfirmTime),
                DBHelper.CreateInDbParameter("@in_ConfirmMan", DbType.String, model.ConfirmMan),
                DBHelper.CreateInDbParameter("@in_Tel", DbType.String, model.Tel),
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
                    strSql.AppendFormat("DELETE FROM d_ConfirmPhoto WHERE ID={0};\r\n", id);
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }

        public d_ConfirmPhoto GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_ConfirmPhoto ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            d_ConfirmPhoto item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public d_ConfirmPhoto GetItem(int kitWorkID, int kitClassID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_ConfirmPhoto ");
            strSql.Append(" WHERE kitWorkID=@kitWorkID and kitClassID=@kitClassID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@kitWorkID", DbType.Int32, kitWorkID),
				DBHelper.CreateInDbParameter("@kitClassID", DbType.Int32, kitClassID)};
            d_ConfirmPhoto item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public DataTable GetList(int pageIndex, int pageSize, ref int records, string kitName, string userID)
        {
            string query = string.Empty;
            List<DbParameter> para = new List<DbParameter>();
            para.Add(DBHelper.CreateInDbParameter("@PageIndex", DbType.Int32, pageIndex + 1));
            para.Add(DBHelper.CreateInDbParameter("@PageSize", DbType.Int32, pageIndex + pageSize));
            query += string.Empty;
            if (!string.IsNullOrEmpty(kitName))
            {
                query += " and (name like '%'+@kitName+'%' or code like '%'+@kitName+'%') ";
                para.Add(DBHelper.CreateInDbParameter("@kitName", DbType.String, kitName));
            }
            if (!string.IsNullOrEmpty(userID))
                query += string.Format(" and cp.UserID in ({0}) ", userID);
            DbParameter[] cmdParms = para.ToArray();
            string sql = @"select cp.ID,cp.UserID,cp.KitWorkID,ConfirmTime,ConfirmMan,Tel,cp.Remark
                            ,kw.Name,kw.Code,kw.KitID,kw.WorkName,kw.Custom,kw.KitTypeID,kw.KitType,kw.ClassTypeID,kw.ClassType,kw.InsideMaterialID,kw.InsideMaterial
                            {0} from d_ConfirmPhoto as cp
                            left join view_d_KitWork as kw on kw.ID=cp.KitWorkID 
                            where 1=1{1}";
            string strSql = string.Format(@"select count(1) from ({0}) as temptable", string.Format(sql, "", query));
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql, cmdParms);
            DataTable dt = new DataTable();
            if (obj != null && !obj.Equals(0))
            {
                records = int.Parse(obj.ToString());
                strSql = string.Format(@"SELECT * FROM ({0}) as temptable WHERE rowNum BETWEEN @PageIndex and @PageSize",
                                        string.Format(sql, ",ROW_NUMBER() Over(order by ConfirmTime desc,cp.ID desc) as rowNum", query));
                dt = DBHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), cmdParms).Tables[0];
            }
            return dt;
        }
        #region 私有
        private d_ConfirmPhoto GetItem(d_ConfirmPhoto model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new d_ConfirmPhoto();
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
        private void GetModel(d_ConfirmPhoto model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.UserID = DBHelper.GetInt(dr["UserID"]);
            model.KitWorkID = DBHelper.GetInt(dr["KitWorkID"]);
            model.ConfirmTime = DBHelper.GetDateTime(dr["ConfirmTime"]);
            model.ConfirmMan = DBHelper.GetString(dr["ConfirmMan"]);
            model.Tel = DBHelper.GetString(dr["Tel"]);
            model.Remark = DBHelper.GetString(dr["Remark"]);
        }
        private List<d_ConfirmPhoto> GetItem(List<d_ConfirmPhoto> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        d_ConfirmPhoto model = new d_ConfirmPhoto();
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
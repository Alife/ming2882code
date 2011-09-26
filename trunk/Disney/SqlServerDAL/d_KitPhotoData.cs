using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;
using Models.Enums;

namespace SqlServerDAL
{
    public class d_KitPhotoData : DALHelper
    {
        public int Insert(d_KitPhoto model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO d_KitPhoto(");
            strSql.Append("ArterID,KitWorkID,KitPhotoTypeID,PeopleNum,PhotoNum,TeacherNum,ArtistPrice,Amount,Amt,Remark)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_ArterID,@in_KitWorkID,@in_KitPhotoTypeID,@in_PeopleNum,@in_PhotoNum,@in_TeacherNum,@in_ArtistPrice,@in_Amount,@in_Amt,@in_Remark)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_ArterID", DbType.Int32, model.ArterID),
                DBHelper.CreateInDbParameter("@in_KitWorkID", DbType.Int32, model.KitWorkID),
                DBHelper.CreateInDbParameter("@in_KitPhotoTypeID", DbType.Int32, model.KitPhotoTypeID),
                DBHelper.CreateInDbParameter("@in_PeopleNum", DbType.Int32, model.PeopleNum),
                DBHelper.CreateInDbParameter("@in_PhotoNum", DbType.Int32, model.PhotoNum),
                DBHelper.CreateInDbParameter("@in_TeacherNum", DbType.Int32, model.TeacherNum),
                DBHelper.CreateInDbParameter("@in_ArtistPrice", DbType.Double, model.ArtistPrice),
                DBHelper.CreateInDbParameter("@in_Amount", DbType.Double, model.Amount),
                DBHelper.CreateInDbParameter("@in_Amt", DbType.Double, model.Amt),
                DBHelper.CreateInDbParameter("@in_Remark", DbType.String, model.Remark)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }

        public int Update(d_KitPhoto model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE d_KitPhoto SET ");
            strSql.Append("ArterID=@in_ArterID,");
            strSql.Append("KitWorkID=@in_KitWorkID,");
            strSql.Append("KitPhotoTypeID=@in_KitPhotoTypeID,");
            strSql.Append("PeopleNum=@in_PeopleNum,");
            strSql.Append("PhotoNum=@in_PhotoNum,");
            strSql.Append("TeacherNum=@in_TeacherNum,");
            strSql.Append("ArtistPrice=@in_ArtistPrice,");
            strSql.Append("Amount=@in_Amount,");
            strSql.Append("Amt=@in_Amt,");
            strSql.Append("Remark=@in_Remark");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_ArterID", DbType.Int32, model.ArterID),
                DBHelper.CreateInDbParameter("@in_KitWorkID", DbType.Int32, model.KitWorkID),
                DBHelper.CreateInDbParameter("@in_KitPhotoTypeID", DbType.Int32, model.KitPhotoTypeID),
                DBHelper.CreateInDbParameter("@in_PeopleNum", DbType.Int32, model.PeopleNum),
                DBHelper.CreateInDbParameter("@in_PhotoNum", DbType.Int32, model.PhotoNum),
                DBHelper.CreateInDbParameter("@in_TeacherNum", DbType.Int32, model.TeacherNum),
                DBHelper.CreateInDbParameter("@in_ArtistPrice", DbType.Double, model.ArtistPrice),
                DBHelper.CreateInDbParameter("@in_Amount", DbType.Double, model.Amount),
                DBHelper.CreateInDbParameter("@in_Amt", DbType.Double, model.Amt),
                DBHelper.CreateInDbParameter("@in_Remark", DbType.String, model.Remark),
                DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }
        public int Update(int kitWorkID, int arterID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("Update d_KitPhoto set ArterID={1} WHERE KitWorkID={0};\r\n", kitWorkID, arterID);
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }

        public int Delete(List<string> ids)
        {
            StringBuilder strSql = new StringBuilder();
            if (ids.Count > 0)
            {
                foreach (string id in ids)
                    strSql.AppendFormat("delete d_KitPhoto WHERE ID={0};\r\n", id);
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }

        public d_KitPhoto GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_KitPhoto ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            d_KitPhoto item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public List<d_KitPhoto> GetList(int kitWorkID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_KitPhoto ");
            strSql.Append(" WHERE kitWorkID=@kitWorkID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@kitWorkID", DbType.Int32, kitWorkID)};
            List<d_KitPhoto> list = new List<d_KitPhoto>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                GetItem(list, dr);
            return list;
        }
        public int GetCount(int kitID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from d_KitPhoto as kp ");
            strSql.Append("left join d_KitWork as kw on kw.ID=kp.KitWorkID ");
            strSql.Append("left join d_Kit as k on k.ID=kw.KitID ");
            strSql.AppendFormat("where kw.KitID={0} group by KitPhotoTypeID", kitID);
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), null);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
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
                            from d_KitPhoto as kp 
                            inner join d_KitWork as kw on kw.ID=kp.KitWorkID 
                            inner join t_User as u on u.ID=kp.ArterID 
                            inner join d_KitPhotoType as kpt on kpt.ID=kp.KitPhotoTypeID {1}";
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
        private d_KitPhoto GetItem(d_KitPhoto model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new d_KitPhoto();
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
        private void GetModel(d_KitPhoto model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.ArterID = DBHelper.GetInt(dr["ArterID"]);
            model.KitWorkID = DBHelper.GetInt(dr["KitWorkID"]);
            model.KitPhotoTypeID = DBHelper.GetInt(dr["KitPhotoTypeID"]);
            model.PeopleNum = DBHelper.GetInt(dr["PeopleNum"]);
            model.PhotoNum = DBHelper.GetInt(dr["PhotoNum"]);
            model.TeacherNum = DBHelper.GetInt(dr["TeacherNum"]);
            model.ArtistPrice = DBHelper.GetDecimal(dr["ArtistPrice"]);
            model.Amount = DBHelper.GetDecimal(dr["Amount"]);
            model.Amt = DBHelper.GetDecimal(dr["Amt"]);
            model.Remark = DBHelper.GetString(dr["Remark"]);
        }
        private List<d_KitPhoto> GetItem(List<d_KitPhoto> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        d_KitPhoto model = new d_KitPhoto();
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
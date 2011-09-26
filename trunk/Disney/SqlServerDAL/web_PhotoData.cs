using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;

namespace SqlServerDAL
{
    public class web_PhotoData : DALHelper
    {
        public int Insert(web_Photo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO web_Photo(");
            strSql.Append("PhotoTypeID,Name,FilePath,Remark,CreateTime)");
            strSql.Append(" VALUES (");
            strSql.Append("@PhotoTypeID,@Name,@FilePath,@Remark,@CreateTime)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@PhotoTypeID", DbType.Int32, model.PhotoTypeID),
                DBHelper.CreateInDbParameter("@Name", DbType.String, model.Name),
                DBHelper.CreateInDbParameter("@FilePath", DbType.String, model.FilePath),
                DBHelper.CreateInDbParameter("@Remark", DbType.String, model.Remark),
                DBHelper.CreateInDbParameter("@CreateTime", DbType.DateTime, model.CreateTime)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }

        public int Update(web_Photo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE web_Photo SET ");
            strSql.Append("PhotoTypeID=@PhotoTypeID,");
            strSql.Append("Name=@Name,");
            strSql.Append("FilePath=@FilePath,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("CreateTime=@CreateTime");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@PhotoTypeID", DbType.Int32, model.PhotoTypeID),
                DBHelper.CreateInDbParameter("@Name", DbType.String, model.Name),
                DBHelper.CreateInDbParameter("@FilePath", DbType.String, model.FilePath),
                DBHelper.CreateInDbParameter("@Remark", DbType.String, model.Remark),
                DBHelper.CreateInDbParameter("@CreateTime", DbType.DateTime, model.CreateTime),
                DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        public int Delete(List<string> ids)
        {
            StringBuilder strSql = new StringBuilder();
            if (ids.Count > 0)
            {
                foreach (string id in ids)
                    strSql.AppendFormat("DELETE FROM web_Photo WHERE ID={0};\r\n", id);
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }

        public web_Photo GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM web_Photo ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            web_Photo item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public List<web_Photo> GetList(int photoType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM web_Photo ");
            strSql.AppendFormat(" where photoType={0} Order by CreateTime desc", photoType);
            List<web_Photo> list = new List<web_Photo>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
                GetItem(list, dr);
            return list;
        }
        #region 私有
        private web_Photo GetItem(web_Photo model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new web_Photo();
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
        private void GetModel(web_Photo model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.Name = DBHelper.GetString(dr["Name"]); ;
            model.FilePath = DBHelper.GetString(dr["FilePath"]);
            model.Remark = DBHelper.GetString(dr["Remark"]);
            model.PhotoTypeID = DBHelper.GetInt(dr["PhotoTypeID"]);
            model.CreateTime = DBHelper.GetDateTime(dr["CreateTime"]);
        }
        private List<web_Photo> GetItem(List<web_Photo> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        web_Photo model = new web_Photo();
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;

namespace SqlServerDAL
{
    public class w_PhotoData : DALHelper
    {
        public int Insert(w_Photo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO w_Photo(");
            strSql.Append("CategoryID,Name,FilePath)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_CategoryID,@in_Name,@in_FilePath)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_CategoryID", DbType.Int32, model.CategoryID),
				DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
				DBHelper.CreateInDbParameter("@in_FilePath", DbType.String, model.FilePath)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }

        public int Update(w_Photo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE w_Photo SET ");
            strSql.Append("CategoryID=@in_CategoryID,");
            strSql.Append("Name=@in_Name,");
            strSql.Append("FilePath=@in_FilePath");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_CategoryID", DbType.Int32, model.CategoryID),
				DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
				DBHelper.CreateInDbParameter("@in_FilePath", DbType.String, model.FilePath),
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        public int Delete(List<string> ID)
        {
            string strSql = string.Empty;
            foreach (string item in ID)
                strSql += string.Format("DELETE FROM w_Photo WHERE ID={0};\r\n", item);
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }

        public w_Photo GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM w_Photo ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            w_Photo item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public w_PhotoList GetList(int pageIndex, int pageSize)
        {
            string query = string.Empty, order = string.Empty;
            List<DbParameter> para = new List<DbParameter>();
            para.Add(DBHelper.CreateInDbParameter("@PageIndex", DbType.Int32, pageIndex + 1));
            para.Add(DBHelper.CreateInDbParameter("@PageSize", DbType.Int32, pageIndex + pageSize));
            query += "";
            order = "order by ID desc ";
            DbParameter[] cmdParms = para.ToArray();
            w_PhotoList list = new w_PhotoList();
            string strSql = string.Format("select count(1) from w_Photo where 1=1 {0}", query);
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql, cmdParms);
            if (obj != null)
                list.records = int.Parse(obj.ToString());
            else
                return list;
            if (list.records == 0)
                return list;
            strSql = @"SELECT *
                       FROM 
                            (select *
                        ,ROW_NUMBER() Over({0}) as rowNum from w_Photo where 1=1 {1}) as temptable
                       WHERE rowNum BETWEEN @PageIndex and @PageSize";
            strSql = string.Format(strSql, order, query);
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
                GetItem(list.data, dr);
            return list;
        }
        #region 私有
        private w_Photo GetItem(w_Photo model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new w_Photo();
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
        private void GetModel(w_Photo model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.CategoryID = DBHelper.GetInt(dr["CategoryID"]);
            model.Name = DBHelper.GetString(dr["Name"]);
            model.FilePath = DBHelper.GetString(dr["FilePath"]);
        }
        private List<w_Photo> GetItem(List<w_Photo> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        w_Photo model = new w_Photo();
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

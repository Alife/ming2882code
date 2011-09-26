using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;

namespace SqlServerDAL
{
    public class w_PhotoCategoryData : DALHelper
    {
        public int Insert(w_PhotoCategory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO w_PhotoCategory(");
            strSql.Append("Name,Intro,ShootingTime,OrderID)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_Name,@in_Intro,@in_ShootingTime,@in_OrderID)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
                DBHelper.CreateInDbParameter("@in_Intro", DbType.String, model.Intro),
                DBHelper.CreateInDbParameter("@in_ShootingTime", DbType.DateTime, model.ShootingTime),
                DBHelper.CreateInDbParameter("@in_OrderID", DbType.Int32, model.OrderID)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }

        public int Update(w_PhotoCategory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE w_PhotoCategory SET ");
            strSql.Append("Name=@in_Name,");
            strSql.Append("Intro=@in_Intro,");
            strSql.Append("ShootingTime=@in_ShootingTime,");
            strSql.Append("OrderID=@in_OrderID");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
                DBHelper.CreateInDbParameter("@in_Intro", DbType.String, model.Intro),
                DBHelper.CreateInDbParameter("@in_ShootingTime", DbType.DateTime, model.ShootingTime),
                DBHelper.CreateInDbParameter("@in_OrderID", DbType.Int32, model.OrderID),
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        public int Delete(List<string> ID)
        {
            string strSql = string.Empty;
            foreach (string item in ID)
                strSql += string.Format("DELETE FROM w_PhotoCategory WHERE ID={0};\r\n", item);
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }

        public w_PhotoCategory GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM w_PhotoCategory ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            w_PhotoCategory item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public List<w_PhotoCategory> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM w_PhotoCategory ");
            strSql.Append(" Order by OrderID");
            List<w_PhotoCategory> list = new List<w_PhotoCategory>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
                GetItem(list, dr);
            return list;
        }
        #region 私有
        private w_PhotoCategory GetItem(w_PhotoCategory model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new w_PhotoCategory();
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
        private void GetModel(w_PhotoCategory model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.Name = DBHelper.GetString(dr["Name"]);
            model.Intro = DBHelper.GetString(dr["Intro"]);
            model.ShootingTime = DBHelper.GetDateTime(dr["ShootingTime"]);
            model.OrderID = DBHelper.GetInt(dr["OrderID"]);
        }
        private List<w_PhotoCategory> GetItem(List<w_PhotoCategory> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        w_PhotoCategory model = new w_PhotoCategory();
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

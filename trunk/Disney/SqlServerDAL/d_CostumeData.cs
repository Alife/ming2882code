using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;

namespace SqlServerDAL
{
    public class d_CostumeData : DALHelper
    {
        public int Insert(d_Costume model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO d_Costume(");
            strSql.Append("Name,Code,OrderID)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_Name,@in_Code,@in_OrderID)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
                DBHelper.CreateInDbParameter("@in_Code", DbType.String, model.Code),
                DBHelper.CreateInDbParameter("@in_OrderID", DbType.Int32, model.OrderID)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }

        public int Update(d_Costume model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE d_Costume SET ");
            strSql.Append("Name=@in_Name,");
            strSql.Append("Code=@in_Code,");
            strSql.Append("OrderID=@in_OrderID");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
                DBHelper.CreateInDbParameter("@in_Code", DbType.String, model.Code),
                DBHelper.CreateInDbParameter("@in_OrderID", DbType.Int32, model.OrderID),
                DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        public int Delete(List<string> ids)
        {
            StringBuilder strSql = new StringBuilder();
            if (ids.Count > 0)
            {
                foreach (string id in ids)
                {
                    strSql.AppendFormat("if not exists (select id from d_KitType where CoverID={0}) \r\n", id);
                    strSql.Append("begin \r\n");
                    strSql.AppendFormat("DELETE FROM d_Costume WHERE ID={0};\r\n", id);
                    strSql.Append("end");
                }
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }

        public d_Costume GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_Costume ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            d_Costume item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public List<d_Costume> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_Costume ");
            strSql.Append(" Order by OrderID");
            List<d_Costume> list = new List<d_Costume>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
                GetItem(list, dr);
            return list;
        }
        public List<d_Costume> GetList(int sex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_Costume where sex=@sex");
            strSql.Append(" Order by OrderID");
            List<d_Costume> list = new List<d_Costume>();
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@sex", DbType.Int32, sex)};
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                GetItem(list, dr);
            return list;
        }
        #region 私有
        private d_Costume GetItem(d_Costume model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new d_Costume();
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
        private void GetModel(d_Costume model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.Name = DBHelper.GetString(dr["Name"]);
            model.Code = DBHelper.GetString(dr["Code"]);
            model.OrderID = DBHelper.GetInt(dr["OrderID"]);
        }
        private List<d_Costume> GetItem(List<d_Costume> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        d_Costume model = new d_Costume();
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
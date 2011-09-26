using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;

namespace SqlServerDAL
{
    public class d_InsideMaterialData : DALHelper
    {
        public int Insert(d_InsideMaterial model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO d_InsideMaterial(");
            strSql.Append("Name,OrderID)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_Name,@in_OrderID)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
                DBHelper.CreateInDbParameter("@in_OrderID", DbType.Int32, model.OrderID)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }

        public int Update(d_InsideMaterial model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE d_InsideMaterial SET ");
            strSql.Append("Name=@in_Name,");
            strSql.Append("OrderID=@in_OrderID");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
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
                    strSql.AppendFormat("DELETE FROM d_InsideMaterial WHERE ID={0};\r\n", id);
                    strSql.Append("end");
                }
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }

        public d_InsideMaterial GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_InsideMaterial ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            d_InsideMaterial item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public List<d_InsideMaterial> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_InsideMaterial ");
            strSql.Append(" Order by OrderID");
            List<d_InsideMaterial> list = new List<d_InsideMaterial>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
                GetItem(list, dr);
            return list;
        }
        #region 私有
        private d_InsideMaterial GetItem(d_InsideMaterial model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new d_InsideMaterial();
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
        private void GetModel(d_InsideMaterial model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.Name = DBHelper.GetString(dr["Name"]);
            model.OrderID = DBHelper.GetInt(dr["OrderID"]);
        }
        private List<d_InsideMaterial> GetItem(List<d_InsideMaterial> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        d_InsideMaterial model = new d_InsideMaterial();
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
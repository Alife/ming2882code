using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;

namespace SqlServerDAL
{
    public class d_InsideTypeData : DALHelper
    {
        public int Insert(d_InsideType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO d_InsideType(");
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

        public int Update(d_InsideType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE d_InsideType SET ");
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
                    strSql.AppendFormat("DELETE FROM d_InsideType WHERE ID={0};\r\n", id);
                    strSql.Append("end");
                }
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }

        public d_InsideType GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_InsideType ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            d_InsideType item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public List<d_InsideType> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_InsideType ");
            strSql.Append(" Order by OrderID");
            List<d_InsideType> list = new List<d_InsideType>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
                GetItem(list, dr);
            return list;
        }
        #region 私有
        private d_InsideType GetItem(d_InsideType model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new d_InsideType();
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
        private void GetModel(d_InsideType model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.Name = DBHelper.GetString(dr["Name"]);
            model.OrderID = DBHelper.GetInt(dr["OrderID"]);
        }
        private List<d_InsideType> GetItem(List<d_InsideType> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        d_InsideType model = new d_InsideType();
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
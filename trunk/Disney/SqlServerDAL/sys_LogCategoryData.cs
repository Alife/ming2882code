using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Models;

namespace SqlServerDAL
{
    public class sys_LogCategoryData : DALHelper
    {
        public int Insert(sys_LogCategory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO sys_LogCategory(");
            strSql.Append("Name, Code, ParentID, IsHidden, OrderID)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_Name, @in_Code, @in_ParentID, @in_IsHidden, @in_OrderID)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
                DBHelper.CreateInDbParameter("@in_Code", DbType.String, model.Code),
                DBHelper.CreateInDbParameter("@in_ParentID", DbType.Int32, model.ParentID),
                DBHelper.CreateInDbParameter("@in_IsHidden", DbType.Boolean, model.IsHidden),
                DBHelper.CreateInDbParameter("@in_OrderID", DbType.Int32, model.OrderID)};

            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }

        public int Update(sys_LogCategory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE sys_LogCategory SET ");
            strSql.Append("Name=@in_Name,");
            strSql.Append("Code=@in_Code,");
            strSql.Append("ParentID=@in_ParentID,");
            strSql.Append("IsHidden=@in_IsHidden,");
            strSql.Append("OrderID=@in_OrderID");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
                DBHelper.CreateInDbParameter("@in_Code", DbType.String, model.Code),
                DBHelper.CreateInDbParameter("@in_ParentID", DbType.Int32, model.ParentID),
                DBHelper.CreateInDbParameter("@in_IsHidden", DbType.Boolean, model.IsHidden),
                DBHelper.CreateInDbParameter("@in_OrderID", DbType.Int32, model.OrderID),
                DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }
        public List<sys_LogCategory> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sys_LogCategory Order by OrderID,id");
            List<sys_LogCategory> list = new List<sys_LogCategory>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            list.Add(GetItem(new sys_LogCategory(), dr));
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
        }

        private sys_LogCategory GetItem(sys_LogCategory model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.Name = DBHelper.GetString(dr["Name"]);
            model.Code = DBHelper.GetString(dr["Code"]);
            model.ParentID = DBHelper.GetInt(dr["ParentID"]);
            model.IsHidden = DBHelper.GetBool(dr["IsHidden"]);
            model.OrderID = DBHelper.GetInt(dr["OrderID"]);
            return model;
        }

        public int Delete(List<string> ID)
        {
            string strSql = string.Empty;
            foreach (string item in ID)
                strSql += string.Format("DELETE FROM sys_LogCategory WHERE ID={0};\r\n", item);
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }

        public sys_LogCategory GetItem(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sys_LogCategory ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, id)};
            sys_LogCategory item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            item = GetItem(new sys_LogCategory(), dr);
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
                return item;
            }
        }
    }
}

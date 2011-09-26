using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Models;

namespace SqlServerDAL
{
    public class sys_LogOpData : DALHelper
    {
        public int Insert(sys_LogOp model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO sys_LogOp(");
            strSql.Append("CategoryID,OpName,OpCode,FormatLog,OrderID)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_CategoryID, @in_OpName, @in_OpCode, @in_FormatLog, @in_OrderID)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_CategoryID", DbType.Int32, model.CategoryID),
                DBHelper.CreateInDbParameter("@in_OpName", DbType.String, model.OpName),
                DBHelper.CreateInDbParameter("@in_OpCode", DbType.String, model.OpCode),
                DBHelper.CreateInDbParameter("@in_FormatLog", DbType.String, model.FormatLog),
                DBHelper.CreateInDbParameter("@in_OrderID", DbType.Int32, model.OrderID)};

            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }

        public int Update(sys_LogOp model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE sys_LogOp SET ");
            strSql.Append("CategoryID=@in_CategoryID,");
            strSql.Append("OpName=@in_OpName,");
            strSql.Append("OpCode=@in_OpCode,");
            strSql.Append("FormatLog=@in_FormatLog,");
            strSql.Append("OrderID=@in_OrderID");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_CategoryID", DbType.Int32, model.CategoryID),
                DBHelper.CreateInDbParameter("@in_OpName", DbType.String, model.OpName),
                DBHelper.CreateInDbParameter("@in_OpCode", DbType.String, model.OpCode),
                DBHelper.CreateInDbParameter("@in_FormatLog", DbType.String, model.FormatLog),
                DBHelper.CreateInDbParameter("@in_OrderID", DbType.Int32, model.OrderID),
                DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }
        public List<sys_LogOp> GetList(int categoryID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ID,CategoryID,OpName,OpCode,FormatLog,OrderID FROM sys_LogOp where CategoryID=@CategoryID Order by OrderID");
            DbParameter[] cmdParms = new DbParameter[] { DBHelper.CreateInDbParameter("@CategoryID", DbType.Int32, categoryID) };
            List<sys_LogOp> list = new List<sys_LogOp>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            list.Add(GetItem(new sys_LogOp(), dr));
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

        private sys_LogOp GetItem(sys_LogOp model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.CategoryID = DBHelper.GetInt(dr["CategoryID"]);
            model.OpName = DBHelper.GetString(dr["OpName"]);
            model.OpCode = DBHelper.GetString(dr["OpCode"]);
            model.FormatLog = DBHelper.GetString(dr["FormatLog"]);
            model.OrderID = DBHelper.GetInt(dr["OrderID"]);
            return model;
        }

        public int Delete(List<string> ID)
        {
            string strSql = string.Empty;
            foreach (string item in ID)
                strSql += string.Format("DELETE FROM sys_LogOp WHERE ID={0};\r\n", item);
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }

        public sys_LogOp GetItem(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sys_LogOp ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, id)};
            sys_LogOp item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            item = GetItem(new sys_LogOp(), dr);
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
        public sys_LogOp GetItem(string opcode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sys_LogOp ");
            strSql.Append(" WHERE opcode=@opcode");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@opcode", DbType.String, opcode)};
            sys_LogOp item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            item = GetItem(new sys_LogOp(), dr);
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

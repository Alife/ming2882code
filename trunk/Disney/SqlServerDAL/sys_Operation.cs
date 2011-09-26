using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;

namespace SqlServerDAL
{
    /// <summary>
    /// 功能操作
    /// </summary>
    public class sys_OperationData : DALHelper
    {
        public int Insert(sys_Operation model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO sys_Operation(");
            strSql.Append("Code,Operation,ApplicationID,OrderID,Icon)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_Code,@in_Operation,@in_ApplicationID,@in_OrderID,@in_Icon)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_Code", DbType.String, model.Code),
				DBHelper.CreateInDbParameter("@in_Operation", DbType.String, model.Operation),
				DBHelper.CreateInDbParameter("@in_ApplicationID", DbType.Int32, model.ApplicationID),
				DBHelper.CreateInDbParameter("@in_OrderID", DbType.Int32, model.OrderID),
				DBHelper.CreateInDbParameter("@in_Icon", DbType.String, model.Icon)};
            object obj2 = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj2 == null)
                return 0;
            return Convert.ToInt32(obj2);
        }

        public int Update(sys_Operation model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE sys_Operation SET ");
            strSql.Append("Code=@in_Code,");
            strSql.Append("Operation=@in_Operation,");
            strSql.Append("ApplicationID=@in_ApplicationID,");
            strSql.Append("OrderID=@in_OrderID,");
            strSql.Append("Icon=@in_Icon");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_Code", DbType.String, model.Code),
				DBHelper.CreateInDbParameter("@in_Operation", DbType.String, model.Operation),
				DBHelper.CreateInDbParameter("@in_ApplicationID", DbType.Int32, model.ApplicationID),
				DBHelper.CreateInDbParameter("@in_OrderID", DbType.Int32, model.OrderID),
				DBHelper.CreateInDbParameter("@in_Icon", DbType.String, model.Icon),
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        public int Update(List<string> ID, List<string> OrderID)
        {
            string strSql = string.Empty;
            int i = 0;
            foreach (string item in ID)
            {
                strSql += string.Format("update sys_Operation set orderid={1} WHERE ID={0};\r\n", item, OrderID[i]);
                i++;
            }
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }
        public int Delete(List<string> ID)
        {
            string strSql = string.Empty;
            foreach (string item in ID)
            {
                strSql += string.Format("DELETE FROM sys_Field WHERE OperationID={0};\r\n", item);
                strSql += string.Format("DELETE FROM sys_PermissionField WHERE PermissionID in (select id from sys_Permission where OperationID={0});\r\n", item);
                strSql += string.Format("DELETE FROM sys_Permission WHERE OperationID={0};\r\n", item);
                strSql += string.Format("DELETE FROM sys_Operation WHERE ID={0};\r\n", item);
            }
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }

        public sys_Operation GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sys_Operation ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            sys_Operation item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            item = GetItem(new sys_Operation(), dr);
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
        public List<sys_Operation> GetList(int applicationID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sys_Operation where ApplicationID=@in_ApplicationID order by orderid");
            List<sys_Operation> list = new List<sys_Operation>();
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ApplicationID", DbType.Int32, applicationID)};
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            list.Add(GetItem(new sys_Operation(), dr));
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
        public List<sys_Operation> GetList(int uid, string appCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct sys_Operation.* from sys_Operation ");
            strSql.Append("inner join sys_Application on sys_Operation.ApplicationID=sys_Application.ID ");
            strSql.Append("inner join sys_Permission on sys_Permission.OperationID=sys_Operation.ID ");
            strSql.Append("inner join sys_Role on sys_Role.ID=sys_Permission.RoleID ");
            strSql.Append("inner join sys_UserRole on sys_Role.ID=sys_UserRole.RoleID  ");
            strSql.Append("inner join t_User on sys_UserRole.UserID=t_User.ID  ");
            strSql.AppendFormat("where t_User.ID={0} and IsHidden=0 and sys_Application.Code='{1}' ", uid, appCode);
            List<sys_Operation> list = new List<sys_Operation>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            list.Add(GetItem(new sys_Operation(), dr));
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
            }
            return list;
        }

        private sys_Operation GetItem(sys_Operation model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.Code = DBHelper.GetString(dr["Code"]);
            model.Operation = DBHelper.GetString(dr["Operation"]);
            model.ApplicationID = DBHelper.GetInt(dr["ApplicationID"]);
            model.OrderID = DBHelper.GetInt(dr["OrderID"]);
            model.Icon = DALHelper.DBHelper.GetString(dr["Icon"]);
            return model;
        }
    }
}

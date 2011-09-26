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
    /// 角色操作
    /// </summary>
    public class sys_RoleData : DALHelper
    {
        public int Insert(sys_Role model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO sys_Role(");
            strSql.Append("Description,RoleName)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_Description,@in_RoleName)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_Description", DbType.String, model.Description),
				DBHelper.CreateInDbParameter("@in_RoleName", DbType.String, model.RoleName)};
            object obj2 = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj2 == null)
                return 0;
            return Convert.ToInt32(obj2);
        }

        public int Update(sys_Role model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE sys_Role SET ");
            strSql.Append("Description=@in_Description,");
            strSql.Append("RoleName=@in_RoleName");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_Description", DbType.String, model.Description),
				DBHelper.CreateInDbParameter("@in_RoleName", DbType.String, model.RoleName),
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        public int Delete(List<string> ID)
        {
            string strSql = string.Empty;
            foreach (string item in ID)
                strSql += string.Format("DELETE FROM sys_Role WHERE ID={0};\r\n", item);
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }

        public sys_Role GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sys_Role ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            sys_Role item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            item = GetItem(new sys_Role(), dr);
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
        public List<sys_Role> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sys_Role ");
            List<sys_Role> list = new List<sys_Role>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            list.Add(GetItem(new sys_Role(), dr));
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

        private sys_Role GetItem(sys_Role model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.Description = DBHelper.GetString(dr["Description"]);
            model.RoleName = DBHelper.GetString(dr["RoleName"]);
            return model;
        }
    }
}

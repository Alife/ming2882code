using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;

namespace SqlServerDAL
{
    public class t_UserTypeData : DALHelper
    {
        public int Insert(t_UserType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO t_UserType(");
            strSql.Append("Name,Type,RoleID)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_Name,@in_Type,@in_RoleID)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
                DBHelper.CreateInDbParameter("@in_RoleID", DbType.Int32, model.RoleID),
                DBHelper.CreateInDbParameter("@in_Type", DbType.Int32, model.Type)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }

        public int Update(t_UserType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE t_UserType SET ");
            strSql.Append("Name=@in_Name,");
            strSql.Append("RoleID=@in_RoleID,");
            strSql.Append("Type=@in_Type");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
                DBHelper.CreateInDbParameter("@in_Type", DbType.Int32, model.Type),
                DBHelper.CreateInDbParameter("@in_RoleID", DbType.Int32, model.RoleID),
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
                    strSql.AppendFormat("if not exists (select id from sys_Role where RoleID={0}) \r\n", id);
                    strSql.Append("begin \r\n");
                    strSql.AppendFormat("DELETE FROM t_UserType WHERE ID={0};\r\n", id);
                    strSql.Append("end");
                }
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }

        public t_UserType GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_UserType ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            t_UserType item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public List<t_UserType> GetList(string type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_UserType ");
            if (!string.IsNullOrEmpty(type))
            {
                string tempquery = string.Empty;
                foreach (string item in type.Split(','))
                    tempquery += string.Format("or type='{0}' ", item);
                strSql.Append("where " + tempquery.Substring(2));
            }
            List<t_UserType> list = new List<t_UserType>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
                GetItem(list, dr);
            return list;
        }
        #region 私有
        private t_UserType GetItem(t_UserType model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new t_UserType();
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
        private void GetModel(t_UserType model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.Name = DBHelper.GetString(dr["Name"]);
            model.Type = DBHelper.GetInt(dr["Type"]);
            model.RoleID = DBHelper.GetInt(dr["RoleID"]);
        }
        private List<t_UserType> GetItem(List<t_UserType> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        t_UserType model = new t_UserType();
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
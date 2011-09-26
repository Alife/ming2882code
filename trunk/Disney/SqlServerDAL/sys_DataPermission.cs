using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;
using Models.Enums;

namespace SqlServerDAL
{
    /// <summary>
    /// 数据权限
    /// </summary>
    public class sys_DataPermissionData : DALHelper
    {
        public int Insert(sys_DataPermission model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO sys_DataPermission(");
            strSql.Append("RoleID,Confine,ResourceID,ResourceType)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_RoleID,@in_Confine,@in_ResourceID,@in_ResourceType)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_RoleID", DbType.Int32, model.RoleID),
				DBHelper.CreateInDbParameter("@in_Confine", DbType.Int32, model.Confine),
				DBHelper.CreateInDbParameter("@in_ResourceID", DbType.Int32, model.ResourceID),
				DBHelper.CreateInDbParameter("@in_ResourceType", DbType.Int32, model.ResourceType)};
            object obj2 = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj2 == null)
                return 0;
            return Convert.ToInt32(obj2);
        }

        public int Update(sys_DataPermission model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE sys_DataPermission SET ");
            strSql.Append("RoleID=@in_RoleID,");
            strSql.Append("Confine=@in_Confine,");
            strSql.Append("ResourceID=@in_ResourceID,");
            strSql.Append("ResourceType=@in_ResourceType");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_RoleID", DbType.Int32, model.RoleID),
				DBHelper.CreateInDbParameter("@in_Confine", DbType.Int32, model.Confine),
				DBHelper.CreateInDbParameter("@in_ResourceID", DbType.Int32, model.ResourceID),
				DBHelper.CreateInDbParameter("@in_ResourceType", DbType.Int32, model.ResourceType),
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        public int Delete(List<string> ids)
        {
            StringBuilder strSql = new StringBuilder();
            if (ids.Count > 0)
            {
                foreach (string id in ids)
                    strSql.AppendFormat("DELETE FROM sys_DataPermission WHERE ID={0};\r\n", id);
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }

        public sys_DataPermission GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sys_DataPermission ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            sys_DataPermission item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            item = GetItem(new sys_DataPermission(), dr);
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
        public List<sys_DataPermission> GetList(int roleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT * FROM sys_DataPermission where RoleID={0}", roleID);
            List<sys_DataPermission> list = new List<sys_DataPermission>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            list.Add(GetItem(new sys_DataPermission(), dr));
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
        public string GetList(int uid, ResourceType type)
        {
            string str = string.Empty;
            List<sys_DataPermission> list = GetLists(uid, type);
            foreach (var item in list)
            {
                if (item.Confine == (int)Confine.Global)
                    str = string.Empty;
                else if (item.Confine == (int)Confine.Company || item.Confine == (int)Confine.Dept)
                {
                    t_UserData userData = new t_UserData();
                    var userList = userData.GetList(item.ResourceID, string.Empty, (Confine)item.Confine);
                    foreach (var user in userList)
                        str += user.ID.ToString() + ",";
                }
                else if (item.Confine == (int)Confine.Own)
                    str += item.ResourceID.ToString() + ",";
                else if (item.Confine == (int)Confine.Self)
                    str = uid.ToString();
            }
            str = str.TrimEnd(',');
            return str;
        }
        public List<sys_DataPermission> GetLists(int uid, ResourceType type)
        {
            string str = string.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct sys_DataPermission.* from sys_DataPermission ");
            strSql.Append("inner join sys_Role on sys_DataPermission.RoleID=sys_Role.ID ");
            strSql.Append("inner join sys_UserRole on sys_UserRole.RoleID=sys_Role.ID ");
            strSql.AppendFormat("where sys_DataPermission.ResourceType={0} and sys_UserRole.UserID={1} ", (int)type, uid);
            List<sys_DataPermission> list = new List<sys_DataPermission>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            list.Add(GetItem(new sys_DataPermission(), dr));
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
        public sys_DataPermission GetItem(int uid, ResourceType type)
        {
            string str = string.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct top 1 sys_DataPermission.* from sys_DataPermission ");
            strSql.Append("inner join sys_Role on sys_DataPermission.RoleID=sys_Role.ID ");
            strSql.Append("inner join sys_UserRole on sys_UserRole.RoleID=sys_Role.ID ");
            strSql.AppendFormat("where sys_DataPermission.ResourceType={0} and sys_UserRole.UserID={1} ", (int)type, uid);
            sys_DataPermission item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            item = GetItem(new sys_DataPermission(), dr);
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
            return item;
        }

        private sys_DataPermission GetItem(sys_DataPermission model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.RoleID = DBHelper.GetInt(dr["RoleID"]);
            model.Confine = DBHelper.GetInt(dr["Confine"]);
            model.ResourceID = DBHelper.GetIntByNull(dr["ResourceID"]);
            model.ResourceType = DBHelper.GetInt(dr["ResourceType"]);
            return model;
        }
    }
}

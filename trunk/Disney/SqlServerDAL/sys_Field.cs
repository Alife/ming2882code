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
    /// 功能权限可控字段
    /// </summary>
    public class sys_FieldData : DALHelper
    {
        public int Insert(sys_Field model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO sys_Field(");
            strSql.Append("FieldName,Field,OperationID)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_FieldName,@in_Field,@in_OperationID)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_FieldName", DbType.String, model.FieldName),
				DBHelper.CreateInDbParameter("@in_Field", DbType.String, model.Field),
				DBHelper.CreateInDbParameter("@in_OperationID", DbType.Int32, model.OperationID)};
            object obj2 = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj2 == null)
                return 0;
            return Convert.ToInt32(obj2);
        }

        public int Update(sys_Field model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE sys_Field SET ");
            strSql.Append("FieldName=@in_FieldName,");
            strSql.Append("Field=@in_Field,");
            strSql.Append("OperationID=@in_OperationID");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_FieldName", DbType.String, model.FieldName),
				DBHelper.CreateInDbParameter("@in_Field", DbType.String, model.Field),
				DBHelper.CreateInDbParameter("@in_OperationID", DbType.Int32, model.OperationID),
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        public int Delete(List<string> ID)
        {
            string strSql = string.Empty;
            foreach (string item in ID)
                strSql += string.Format("DELETE FROM sys_Field WHERE ID={0};\r\n", item);
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }

        public sys_Field GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sys_Field ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            sys_Field item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            item = GetItem(new sys_Field(), dr);
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
        public List<sys_Field> GetList(int operationID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT * FROM sys_Field where OperationID={0}", operationID);
            List<sys_Field> list = new List<sys_Field>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            list.Add(GetItem(new sys_Field(), dr));
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

        private sys_Field GetItem(sys_Field model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.FieldName = DBHelper.GetString(dr["FieldName"]);
            model.Field = DBHelper.GetString(dr["Field"]);
            model.OperationID = DBHelper.GetInt(dr["OperationID"]);
            return model;
        }
    }
}

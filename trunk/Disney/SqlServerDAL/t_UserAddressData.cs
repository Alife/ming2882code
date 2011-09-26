using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Models;
using DBUtility;

namespace SqlServerDAL
{
    public class t_UserAddressData : DALHelper
    {
        public int Insert(t_UserAddress model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_UserAddress set IsUse=0 where UserID=@in_UserID;");
            strSql.Append("INSERT INTO t_UserAddress(");
            strSql.Append("UserID,Person,Phone,Mobile,CountryID,Address,Zip,IsUse)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_UserID,@in_Person,@in_Phone,@in_Mobile,@in_CountryID,@in_Address,@in_Zip,@in_IsUse)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
				DBHelper.CreateInDbParameter("@in_Person", DbType.String, model.Person),
				DBHelper.CreateInDbParameter("@in_Phone", DbType.String, model.Phone),
				DBHelper.CreateInDbParameter("@in_Mobile", DbType.String, model.Mobile),
				DBHelper.CreateInDbParameter("@in_CountryID", DbType.Int32, model.CountryID),
				DBHelper.CreateInDbParameter("@in_Address", DbType.String, model.Address),
				DBHelper.CreateInDbParameter("@in_Zip", DbType.String, model.Zip),
				DBHelper.CreateInDbParameter("@in_IsUse", DbType.Boolean, model.IsUse)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }
        public int Insert(List<t_UserAddress> list)
        {
            List<CommandInfo> cmdList = new List<CommandInfo>();
            foreach (var model in list)
            {
                StringBuilder strSql = new StringBuilder();
                if (model.ID == 0)
                {
                    strSql.Append("INSERT INTO t_UserAddress(");
                    strSql.Append("UserID,Person,Phone,Mobile,CountryID,Address,Zip,IsUse)");
                    strSql.Append(" VALUES (");
                    strSql.Append("@in_UserID,@in_Person,@in_Phone,@in_Mobile,@in_CountryID,@in_Address,@in_Zip,@in_IsUse); \r\n");
                }
                else
                {
                    strSql.Append("UPDATE t_UserAddress SET ");
                    strSql.Append("UserID=@in_UserID,");
                    strSql.Append("Person=@in_Person,");
                    strSql.Append("Phone=@in_Phone,");
                    strSql.Append("Mobile=@in_Mobile,");
                    strSql.Append("CountryID=@in_CountryID,");
                    strSql.Append("Address=@in_Address,");
                    strSql.Append("Zip=@in_Zip,");
                    strSql.Append("IsUse=@in_IsUse");
                    strSql.Append(" WHERE ID=@in_ID; \r\n");
                }
                DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
				DBHelper.CreateInDbParameter("@in_Person", DbType.String, model.Person),
				DBHelper.CreateInDbParameter("@in_Phone", DbType.String, model.Phone),
				DBHelper.CreateInDbParameter("@in_Mobile", DbType.String, model.Mobile),
				DBHelper.CreateInDbParameter("@in_CountryID", DbType.Int32, model.CountryID),
				DBHelper.CreateInDbParameter("@in_Address", DbType.String, model.Address),
				DBHelper.CreateInDbParameter("@in_Zip", DbType.String, model.Zip),
				DBHelper.CreateInDbParameter("@in_IsUse", DbType.Boolean, model.IsUse),
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
                cmdList.Add(new CommandInfo(strSql.ToString(), cmdParms, EffentNextType.ExcuteEffectRows));
            }
            DbConnection conn = DBHelper.CreateConnection();
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            DbTransaction tran = conn.BeginTransaction();
            int revalue = DBHelper.ExecuteNonQuery(tran, CommandType.Text, cmdList);
            return revalue;
        }

        public int Update(t_UserAddress model)
        {
            StringBuilder strSql = new StringBuilder();
            if (model.IsUse)
                strSql.Append("update t_UserAddress set IsUse=0 where UserID=@in_UserID;");
            strSql.Append("UPDATE t_UserAddress SET ");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("Person=@in_Person,");
            strSql.Append("Phone=@in_Phone,");
            strSql.Append("Mobile=@in_Mobile,");
            strSql.Append("CountryID=@in_CountryID,");
            strSql.Append("Address=@in_Address,");
            strSql.Append("Zip=@in_Zip,");
            strSql.Append("IsUse=@in_IsUse");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_UserID", DbType.Int32, model.UserID),
				DBHelper.CreateInDbParameter("@in_Person", DbType.String, model.Person),
				DBHelper.CreateInDbParameter("@in_Phone", DbType.String, model.Phone),
				DBHelper.CreateInDbParameter("@in_Mobile", DbType.String, model.Mobile),
				DBHelper.CreateInDbParameter("@in_CountryID", DbType.Int32, model.CountryID),
				DBHelper.CreateInDbParameter("@in_Address", DbType.String, model.Address),
				DBHelper.CreateInDbParameter("@in_Zip", DbType.String, model.Zip),
				DBHelper.CreateInDbParameter("@in_IsUse", DbType.Boolean, model.IsUse),
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }
        public List<t_UserAddress> GetList(int uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_UserAddress where userid=" + uid);
            List<t_UserAddress> list = new List<t_UserAddress>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            list.Add(GetItem(new t_UserAddress(), dr));
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

        private t_UserAddress GetItem(t_UserAddress model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.UserID = DBHelper.GetInt(dr["UserID"]);
            model.Person = DBHelper.GetString(dr["Person"]);
            model.Phone = DBHelper.GetString(dr["Phone"]);
            model.Mobile = DBHelper.GetString(dr["Mobile"]);
            model.CountryID = DBHelper.GetIntByNull(dr["CountryID"]);
            model.Address = DBHelper.GetString(dr["Address"]);
            model.Zip = DBHelper.GetString(dr["Zip"]);
            model.IsUse = DBHelper.GetBool(dr["IsUse"]);
            return model;
        }

        public int Delete(List<string> ID)
        {
            string strSql = string.Empty;
            foreach (string item in ID)
                strSql += string.Format("DELETE FROM t_UserAddress WHERE ID={0};\r\n", item);
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }

        public t_UserAddress GetItem(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_UserAddress ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, id)};
            t_UserAddress item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            item = GetItem(new t_UserAddress(), dr);
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
        public t_UserAddress GetItemHas(int uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_UserAddress ");
            strSql.Append(" WHERE IsUse=1 and UserID=" + uid);
            t_UserAddress item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            item = GetItem(new t_UserAddress(), dr);
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

        public t_UserAddressList GetModelList(int uid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM t_UserAddress ");
            strSql.Append(" WHERE UserID=" + uid);
            t_UserAddressList item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
            {
                try
                {
                    if (dr != null)
                    {
                        item = new t_UserAddressList();
                        while (dr.Read())
                            item.data.Add(GetItem(new t_UserAddress(), dr));
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;
using DBUtility;

namespace SqlServerDAL
{
    public class d_KitChildData : DALHelper
    {
        public int Insert(d_KitChild model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO d_KitChild(");
            strSql.Append("Code,TrueName,KitClassID,Sex)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_Code,@in_TrueName,@in_KitClassID,@in_Sex)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_Code", DbType.String, model.Code),
                DBHelper.CreateInDbParameter("@in_TrueName", DbType.String, model.TrueName),
                DBHelper.CreateInDbParameter("@in_KitClassID", DbType.Int32, model.KitClassID),
                DBHelper.CreateInDbParameter("@in_Sex", DbType.Int32, model.Sex)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }
        public int Insert(List<d_KitChild> list)
        {
            int revalue = 0;
            if (list.Count > 0)
            {
                List<CommandInfo> cmdList = new List<CommandInfo>();
                foreach (var model in list)
                {
                    StringBuilder strSql = new StringBuilder();
                    if (model.ID == 0)
                    {
                        strSql.Append("INSERT INTO d_KitChild(");
                        strSql.Append("Code,TrueName,KitClassID,Sex)");
                        strSql.Append(" VALUES (");
                        strSql.Append("@in_Code,@in_TrueName,@in_KitClassID,@in_Sex)"); 
                        strSql.Append("\r\n");
                    }
                    else
                    {
                        strSql.Append("UPDATE d_KitChild SET ");
                        strSql.Append("Code=@in_Code,");
                        strSql.Append("TrueName=@in_TrueName,");
                        strSql.Append("KitClassID=@in_KitClassID,");
                        strSql.Append("Sex=@in_Sex");
                        strSql.Append(" WHERE ID=@in_ID");
                        strSql.Append("\r\n");
                    }
                    DbParameter[] cmdParms = new DbParameter[]{
                        DBHelper.CreateInDbParameter("@in_Code", DbType.String, model.Code),
                        DBHelper.CreateInDbParameter("@in_TrueName", DbType.String, model.TrueName),
                        DBHelper.CreateInDbParameter("@in_KitClassID", DbType.Int32, model.KitClassID),
                        DBHelper.CreateInDbParameter("@in_Sex", DbType.Int32, model.Sex),
                        DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
                    cmdList.Add(new CommandInfo(strSql.ToString(), cmdParms, EffentNextType.ExcuteEffectRows));
                }
                DbConnection conn = DBHelper.CreateConnection();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                DbTransaction tran = conn.BeginTransaction();
                revalue = DBHelper.ExecuteNonQuery(tran, CommandType.Text, cmdList);
            }
            return revalue;
        }

        public int Update(d_KitChild model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE d_KitChild SET ");
            strSql.Append("Code=@in_Code,");
            strSql.Append("TrueName=@in_TrueName,");
            strSql.Append("KitClassID=@in_KitClassID,");
            strSql.Append("Sex=@in_Sex");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_Code", DbType.String, model.Code),
                DBHelper.CreateInDbParameter("@in_TrueName", DbType.String, model.TrueName),
                DBHelper.CreateInDbParameter("@in_KitClassID", DbType.Int32, model.KitClassID),
                DBHelper.CreateInDbParameter("@in_Sex", DbType.Int32, model.Sex),
                DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, model.ID)};
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        public int Delete(List<string> ids)
        {
            StringBuilder strSql = new StringBuilder();
            if (ids.Count > 0)
            {
                foreach (string id in ids)
                    strSql.AppendFormat("DELETE FROM d_KitChild WHERE ID={0};\r\n", id);
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }

        public d_KitChild GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_KitChild ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            d_KitChild item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }

        public d_KitChild GetItem(int kitClassID, string code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_KitChild ");
            strSql.Append(" WHERE KitClassID=@KitClassID and Code=@Code");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@KitClassID", DbType.Int32, kitClassID),
				DBHelper.CreateInDbParameter("@Code", DbType.String, code)};
            d_KitChild item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public List<d_KitChild> GetList(int KitClassID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_KitChild ");
            strSql.Append(" WHERE KitClassID=@KitClassID");
            strSql.Append(" Order by ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@KitClassID", DbType.Int32, KitClassID)};
            List<d_KitChild> list = new List<d_KitChild>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                GetItem(list, dr);
            return list;
        }
        #region 私有
        private d_KitChild GetItem(d_KitChild model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new d_KitChild();
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
        private void GetModel(d_KitChild model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.Code = DBHelper.GetString(dr["Code"]);
            model.TrueName = DBHelper.GetString(dr["TrueName"]);
            model.KitClassID = DBHelper.GetInt(dr["KitClassID"]);
            model.Sex = DBHelper.GetIntByNull(dr["Sex"]);
        }
        private List<d_KitChild> GetItem(List<d_KitChild> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        d_KitChild model = new d_KitChild();
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;

namespace SqlServerDAL
{
    public class d_KitTypeData : DALHelper
    {
        public int Insert(d_KitType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO d_KitType(");
            strSql.Append("Name,OrderID,CoverID,InsideID,PageNum,PNum,CostumeNum,IsGown,ShootNum)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_Name,@in_OrderID,@in_CoverID,@in_InsideID,@in_PageNum,@in_PNum,@in_CostumeNum,@in_IsGown,@in_ShootNum)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
                DBHelper.CreateInDbParameter("@in_OrderID", DbType.Int32, model.OrderID),
                DBHelper.CreateInDbParameter("@in_CoverID", DbType.Int32, model.CoverID),
                DBHelper.CreateInDbParameter("@in_InsideID", DbType.Int32, model.InsideID),
                DBHelper.CreateInDbParameter("@in_PageNum", DbType.Int32, model.PageNum),
                DBHelper.CreateInDbParameter("@in_PNum", DbType.Int32, model.PNum),
                DBHelper.CreateInDbParameter("@in_CostumeNum", DbType.Int32, model.CostumeNum),
                DBHelper.CreateInDbParameter("@in_IsGown", DbType.Boolean, model.IsGown),
                DBHelper.CreateInDbParameter("@in_ShootNum", DbType.Int32, model.ShootNum)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }

        public int Update(d_KitType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE d_KitType SET ");
            strSql.Append("Name=@in_Name,");
            strSql.Append("OrderID=@in_OrderID,");
            strSql.Append("CoverID=@in_CoverID,");
            strSql.Append("InsideID=@in_InsideID,");
            strSql.Append("PageNum=@in_PageNum,");
            strSql.Append("PNum=@in_PNum,");
            strSql.Append("CostumeNum=@in_CostumeNum,");
            strSql.Append("IsGown=@in_IsGown,");
            strSql.Append("ShootNum=@in_ShootNum");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
                DBHelper.CreateInDbParameter("@in_OrderID", DbType.Int32, model.OrderID),
                DBHelper.CreateInDbParameter("@in_CoverID", DbType.Int32, model.CoverID),
                DBHelper.CreateInDbParameter("@in_InsideID", DbType.Int32, model.InsideID),
                DBHelper.CreateInDbParameter("@in_PageNum", DbType.Int32, model.PageNum),
                DBHelper.CreateInDbParameter("@in_PNum", DbType.Int32, model.PNum),
                DBHelper.CreateInDbParameter("@in_CostumeNum", DbType.Int32, model.CostumeNum),
                DBHelper.CreateInDbParameter("@in_IsGown", DbType.Boolean, model.IsGown),
                DBHelper.CreateInDbParameter("@in_ShootNum", DbType.Int32, model.ShootNum),
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
                    strSql.AppendFormat("if not exists (select id from d_Kit where KitTypeID={0}) \r\n", id);
                    strSql.Append("begin \r\n");
                    strSql.AppendFormat("DELETE FROM d_KitType WHERE ID={0};\r\n", id);
                    strSql.Append("end");
                }
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }

        public d_KitType GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_KitType ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            d_KitType item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public List<d_KitType> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_KitType ");
            strSql.Append(" Order by OrderID");
            List<d_KitType> list = new List<d_KitType>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
                GetItem(list, dr);
            return list;
        }
        public DataTable GetDataTable()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT d_KitType.*,d_InsideType.Name as InsideName,d_CoverType.Name as CoverName FROM d_KitType ");
            strSql.Append(" left join d_InsideType on d_InsideType.ID=d_KitType.InsideID");
            strSql.Append(" left join d_CoverType on d_CoverType.ID=d_KitType.CoverID");
            strSql.Append(" Order by OrderID");
            return DBHelper.ExecuteQuery(CommandType.Text, strSql.ToString(), null).Tables[0];
        }
        #region 私有
        private d_KitType GetItem(d_KitType model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new d_KitType();
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
        private void GetModel(d_KitType model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.Name = DBHelper.GetString(dr["Name"]);
            model.OrderID = DBHelper.GetInt(dr["OrderID"]);
            model.CoverID = DBHelper.GetInt(dr["CoverID"]);
            model.InsideID = DBHelper.GetInt(dr["InsideID"]);
            model.PageNum = DBHelper.GetInt(dr["PageNum"]);
            model.PNum = DBHelper.GetInt(dr["PNum"]);
            model.CostumeNum = DBHelper.GetInt(dr["CostumeNum"]);
            model.IsGown = DBHelper.GetBool(dr["IsGown"]);
            model.ShootNum = DBHelper.GetInt(dr["ShootNum"]);
        }
        private List<d_KitType> GetItem(List<d_KitType> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        d_KitType model = new d_KitType();
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
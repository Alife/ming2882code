using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;

namespace SqlServerDAL
{
    public class d_KitPhotoTypeData : DALHelper
    {
        public int Insert(d_KitPhotoType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO d_KitPhotoType(");
            strSql.Append("Name,Category,OrderID,Price,ArtPrice,Formula)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_Name,@in_Category,@in_OrderID,@in_Price,@in_ArtPrice,@in_Formula)");
            strSql.Append(";select @@IDENTITY");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
                DBHelper.CreateInDbParameter("@in_Category", DbType.Int32, model.Category),
                DBHelper.CreateInDbParameter("@in_OrderID", DbType.Int32, model.OrderID),
                DBHelper.CreateInDbParameter("@in_Price", DbType.Decimal, model.Price),
                DBHelper.CreateInDbParameter("@in_ArtPrice", DbType.Decimal, model.ArtPrice),
                DBHelper.CreateInDbParameter("@in_Formula", DbType.String, model.Formula)};
            object obj = DBHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), cmdParms);
            if (obj == null)
                return 0;
            return Convert.ToInt32(obj);
        }

        public int Update(d_KitPhotoType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE d_KitPhotoType SET ");
            strSql.Append("Name=@in_Name,");
            strSql.Append("Category=@in_Category,");
            strSql.Append("OrderID=@in_OrderID,");
            strSql.Append("Price=@in_Price,");
            strSql.Append("ArtPrice=@in_ArtPrice,");
            strSql.Append("Formula=@in_Formula");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
                DBHelper.CreateInDbParameter("@in_Category", DbType.Int32, model.Category),
                DBHelper.CreateInDbParameter("@in_OrderID", DbType.Int32, model.OrderID),
                DBHelper.CreateInDbParameter("@in_Price", DbType.Decimal, model.Price),
                DBHelper.CreateInDbParameter("@in_ArtPrice", DbType.Decimal, model.ArtPrice),
                DBHelper.CreateInDbParameter("@in_Formula", DbType.String, model.Formula),
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
                    strSql.AppendFormat("if not exists (select id from d_ArtistPrice where KitPhotoTypeID={0}) \r\n", id);
                    strSql.AppendFormat("and if not exists (select id from d_KitPhoto where KitPhotoTypeID={0}) \r\n", id);
                    strSql.AppendFormat("and if not exists (select id from d_ArtistMonth where KitPhotoTypeID={0}) \r\n", id);
                    strSql.Append("begin \r\n");
                    strSql.AppendFormat("DELETE FROM d_KitPhotoType WHERE ID={0};\r\n", id);
                    strSql.Append("end");
                }
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }

        public d_KitPhotoType GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_KitPhotoType ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            d_KitPhotoType item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        public List<d_KitPhotoType> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM d_KitPhotoType ");
            strSql.Append(" Order by OrderID");
            List<d_KitPhotoType> list = new List<d_KitPhotoType>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
                GetItem(list, dr);
            return list;
        }
        #region 私有
        private d_KitPhotoType GetItem(d_KitPhotoType model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new d_KitPhotoType();
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
        private void GetModel(d_KitPhotoType model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.Name = DBHelper.GetString(dr["Name"]);
            model.Category = DBHelper.GetInt(dr["Category"]);
            model.OrderID = DBHelper.GetInt(dr["OrderID"]);
            model.Price = DBHelper.GetDecimal(dr["Price"]);
            model.ArtPrice = DBHelper.GetDecimal(dr["ArtPrice"]);
            model.Formula = DBHelper.GetString(dr["Formula"]);
        }
        private List<d_KitPhotoType> GetItem(List<d_KitPhotoType> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        d_KitPhotoType model = new d_KitPhotoType();
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
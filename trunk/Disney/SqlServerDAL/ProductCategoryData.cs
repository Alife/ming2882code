namespace SqlServerDAL
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text;

    public class ProductCategoryData : DALHelper
    {
        public int Delete(List<string> ID)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string item in ID)
            {
                builder.AppendFormat("if not exists (select id from d_Product where CategoryID={0}) \r\n", item);
                builder.Append("begin \r\n");
                builder.AppendFormat("DELETE FROM d_ProductCategory WHERE ID={0}; \r\n", item);
                builder.Append("end \r\n");
            }
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }
        public int Exists(string _Code)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID from d_ProductCategory");
            builder.AppendFormat(" where Code='{0}' ", _Code);
            object obj2 = DALHelper.DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), null);
            if (obj2 != null)
            {
                return int.Parse(obj2.ToString());
            }
            return 0;
        }

        private ProductCategory GetItem(DbDataReader dr, ProductCategory item)
        {
            item.ID = DALHelper.DBHelper.GetInt(dr["ID"]);
            item.Code = DALHelper.DBHelper.GetString(dr["Code"]);
            item.ParentID = DALHelper.DBHelper.GetInt(dr["ParentID"]);
            item.Category = DALHelper.DBHelper.GetString(dr["Category"]);
            item.OrderID = DALHelper.DBHelper.GetInt(dr["OrderID"]);
            item.Path = DALHelper.DBHelper.GetInt(dr["Path"]);
            item.Description = DALHelper.DBHelper.GetString(dr["Description"]);
            item.MetaKeywords = DALHelper.DBHelper.GetString(dr["MetaKeywords"]);
            item.MetaDescription = DALHelper.DBHelper.GetString(dr["MetaDescription"]);
            return item;
        }

        public ProductCategory GetItem(string ID, int type)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ID,Code,Category,ParentID,OrderID,Path,MetaKeywords,MetaDescription,Description FROM d_ProductCategory");
            if (type == 0)
            {
                builder.Append(" WHERE ID=@in_ID");
            }
            else
            {
                builder.Append(" WHERE Code=@in_ID");
            }
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@in_ID", DbType.String, ID) };
            ProductCategory item = null;
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            item = new ProductCategory();
                            this.GetItem(reader, item);
                        }
                    }
                }
                finally
                {
                    if (reader  != null && !reader.IsClosed)
                    {
                        reader.Close();
                        reader.Dispose();
                    }
                }
                return item;
            }
        }

        public List<ProductCategory> GetList()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ID,Code,Category,ParentID,OrderID,Path,MetaKeywords,MetaDescription,Description FROM d_ProductCategory ORDER BY OrderID");
            List<ProductCategory> list = new List<ProductCategory>();
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), null))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            ProductCategory item = new ProductCategory();
                            list.Add(this.GetItem(reader, item));
                        }
                    }
                }
                finally
                {
                    if (reader  != null && !reader.IsClosed)
                    {
                        reader.Close();
                        reader.Dispose();
                    }
                }
                return list;
            }
        }

        public List<ProductCategory> GetListByChild(int parentid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ID,Code,Category,ParentID,OrderID,Path,MetaKeywords,MetaDescription,Description FROM d_ProductCategory");
            builder.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@in_ID", DbType.String, parentid) };
            List<ProductCategory> list = new List<ProductCategory>();
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            ProductCategory item = new ProductCategory();
                            list.Add(this.GetItem(reader, item));
                            list.AddRange(this.GetListByChild(item.ParentID));
                        }
                    }
                }
                finally
                {
                    if (reader  != null && !reader.IsClosed)
                    {
                        reader.Close();
                        reader.Dispose();
                    }
                }
                return list;
            }
        }

        public List<ProductCategory> GetList(int id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT ID,Code,Category,ParentID,OrderID,Path,MetaKeywords,MetaDescription,Description FROM d_ProductCategory");
            builder.Append(" WHERE ParentID=@in_ParentID ORDER BY OrderID");
            DbParameter[] cmdParms = new DbParameter[] { DALHelper.DBHelper.CreateInDbParameter("@in_ParentID", DbType.String, id) };
            List<ProductCategory> list = new List<ProductCategory>();
            using (DbDataReader reader = DALHelper.DBHelper.ExecuteReader(CommandType.Text, builder.ToString(), cmdParms))
            {
                try
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            list.Add(this.GetItem(reader, new ProductCategory()));
                        }
                    }
                }
                finally
                {
                    if (reader  != null && !reader.IsClosed)
                    {
                        reader.Close();
                        reader.Dispose();
                    }
                }
                return list;
            }
        }

        public int Insert(ProductCategory item)
        {
            string str = string.Empty;
            int num = 0;
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@Code", DbType.String, item.Code), 
                DALHelper.DBHelper.CreateInDbParameter("@Category", DbType.String, item.Category), 
                DALHelper.DBHelper.CreateInDbParameter("@ParentID", DbType.Int32, item.ParentID), 
                DALHelper.DBHelper.CreateInDbParameter("@OrderID", DbType.Int32, item.OrderID), 
                DALHelper.DBHelper.CreateInDbParameter("@Path", DbType.Int32, item.Path), 
                DALHelper.DBHelper.CreateInDbParameter("@MetaKeywords", DbType.String, item.MetaKeywords), 
                DALHelper.DBHelper.CreateInDbParameter("@MetaDescription", DbType.String, item.MetaDescription) , 
                DALHelper.DBHelper.CreateInDbParameter("@Description", DbType.String, item.Description) };
            str = "INSERT INTO d_ProductCategory(Code,Category,ParentID,OrderID,Path,MetaKeywords,MetaDescription,Description) \r\n  " +
                "VALUES(@Code,@Category,@ParentID,@OrderID,@Path,@MetaKeywords,@MetaDescription,@Description);select @@IDENTITY;";
            object obj2 = DALHelper.DBHelper.ExecuteScalar(CommandType.Text, str, cmdParms);
            if (obj2 != null)
            {
                num = int.Parse(obj2.ToString());
            }
            return num;
        }

        public int Update(ProductCategory item)
        {
            string str = string.Empty;
            DbParameter[] cmdParms = new DbParameter[] { 
                DALHelper.DBHelper.CreateInDbParameter("@ID", DbType.Int32, item.ID), 
                DALHelper.DBHelper.CreateInDbParameter("@Code", DbType.String, item.Code), 
                DALHelper.DBHelper.CreateInDbParameter("@Category", DbType.String, item.Category), 
                DALHelper.DBHelper.CreateInDbParameter("@ParentID", DbType.Int32, item.ParentID), 
                DALHelper.DBHelper.CreateInDbParameter("@OrderID", DbType.Int32, item.OrderID), 
                DALHelper.DBHelper.CreateInDbParameter("@Path", DbType.Int32, item.Path), 
                DALHelper.DBHelper.CreateInDbParameter("@MetaKeywords", DbType.String, item.MetaKeywords), 
                DALHelper.DBHelper.CreateInDbParameter("@MetaDescription", DbType.String, item.MetaDescription) , 
                DALHelper.DBHelper.CreateInDbParameter("@Description", DbType.String, item.Description) };
            str = "UPDATE d_ProductCategory SET Code=@Code,Category=@Category,ParentID=@ParentID,OrderID=@OrderID,Path=@Path,\r\n" +
                "MetaKeywords=@MetaKeywords,MetaDescription=@MetaDescription,Description=@Description\r\n " +
                "where ID=@ID";
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), cmdParms);
        }

        public int Update(List<ProductCategory> list)
        {
            string str = string.Empty;
            foreach (ProductCategory category in list)
            {
                str = str + string.Format("UPDATE d_ProductCategory SET OrderID={1} where ID={0};", category.ID, category.OrderID);
            }
            return DALHelper.DBHelper.ExecuteNonQuery(CommandType.Text, str.ToString(), null);
        }
    }
}

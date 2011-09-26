using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Models;

namespace SqlServerDAL
{
    public class d_DepartmentData : DALHelper
    {
        public int Insert(d_Department item)
        {
            StringBuilder strSql = new StringBuilder();
            DbConnection conn = DBHelper.CreateConnection();
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            DbTransaction tran = conn.BeginTransaction();
            object obj = null;
            strSql.Append("declare @ID int \r\n");
            strSql.Append("declare @myWidth int \r\n");
            strSql.Append("set @myWidth=0 \r\n");
            strSql.Append("declare @childen int \r\n");
            strSql.Append("set @childen=0 \r\n");
            strSql.Append("if (@ParentID = 0) \r\n");//增加同级
            strSql.Append("begin \r\n");
            strSql.Append("     if exists (SELECT top 1 ID FROM d_Department order by Rgt desc) \r\n");
            strSql.Append("     begin   \r\n");
            strSql.Append("         SELECT top 1 @myWidth=Rgt FROM d_Department order by Rgt desc;  \r\n");
            strSql.Append("     end   \r\n");
            strSql.Append("end \r\n");
            strSql.Append("else \r\n");
            strSql.Append("begin \r\n");
            strSql.Append("     SELECT @childen=(Rgt-lft-1)/2,@myWidth=Lft FROM d_Department where id=@ParentID; \r\n");//增加子级，如果childen不为0，@myWidth会被覆盖
            strSql.Append("     if (@childen > 0) \r\n");
            strSql.Append("         SELECT top 1 @myWidth=Rgt FROM d_Department WHERE parentid=@ParentID order by lft desc; \r\n");//父类第一子类之最后一项，增加同级
            strSql.Append("end \r\n");
            strSql.Append("update d_Department set lft=lft+2 where lft>@myWidth;\r\n");
            strSql.Append("update d_Department set rgt=rgt+2 where rgt>@myWidth;\r\n");
            strSql.Append("select @ID=isnull(max(id),0)+1 from d_Department;\r\n");
            strSql.Append("INSERT INTO d_Department (ID,ParentID,Name,Code,Lft,Rgt) VALUES (@ID,@ParentID,@Name,@Code,@myWidth+1,@myWidth+2);\r\n");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@ParentID", DbType.Int32, item.ParentID),
                DBHelper.CreateInDbParameter("@Name", DbType.String, item.Name),
                DBHelper.CreateInDbParameter("@Code", DbType.String, item.Code)};
            obj = DBHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), cmdParms);
            if (obj != null)
                return Convert.ToInt32(obj);
            return 0;
        }

        public int Update(d_Department item)
        {
            StringBuilder strSql = new StringBuilder();
            int revlue = 0;
            if (item.ParentID == -1)
            {
                strSql.Append("update d_Department set Name=@Name,Code=@Code where ID=@ID; \r\n");
                DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@ID", DbType.Int32, item.ID),
                DBHelper.CreateInDbParameter("@Name", DbType.String, item.Name),
                DBHelper.CreateInDbParameter("@Code", DbType.String, item.Code)};
                revlue = DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
            }
            else
            {
                DbConnection conn = DBHelper.CreateConnection();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                DbTransaction tran = conn.BeginTransaction();
                List<d_Department> list = GetList(item.ID, 2);
                strSql.Append("declare @myLeft int \r\n");
                strSql.Append("declare @myRight int \r\n");
                strSql.Append("declare @myWidth int \r\n");
                strSql.AppendFormat("SELECT @myLeft=Lft,@myRight=Rgt,@myWidth=rgt-lft+1 FROM d_Department WHERE ID={0}; \r\n", item.ID);
                strSql.Append("delete d_Department where lft BETWEEN @myLeft AND @myRight; \r\n");
                strSql.Append("update d_Department set Lft=Lft-@myWidth where Lft>@myRight; \r\n");
                strSql.Append("update d_Department set Rgt=Rgt-@myWidth where Rgt>@myRight; \r\n");
                strSql.Append("declare @childen int \r\n");
                strSql.Append("declare @ID int \r\n");
                strSql.Append("declare @ParentID int \r\n");
                int i = 0;
                foreach (d_Department titem in list)
                {
                    strSql.AppendFormat("set @ID = {0} \r\n", titem.ID);
                    if (i == 0)
                    {
                        strSql.AppendFormat("set @ParentID = {0} \r\n", item.ParentID);
                        titem.Name = item.Name;
                        titem.Code = item.Code;
                    }
                    else
                        strSql.AppendFormat("set @ParentID = {0} \r\n", titem.ParentID);
                    strSql.Append("set @myWidth=0 \r\n");
                    strSql.Append("if (@ParentID = 0) \r\n");//增加同级
                    strSql.Append("begin \r\n");
                    strSql.Append("     if exists (SELECT top 1 ID FROM d_Department order by Rgt desc) \r\n");
                    strSql.Append("     begin   \r\n");
                    strSql.Append("         SELECT top 1 @myWidth=Rgt FROM d_Department order by Rgt desc;  \r\n");
                    strSql.Append("     end   \r\n");
                    strSql.Append("end \r\n");
                    strSql.Append("else \r\n");
                    strSql.Append("begin \r\n");
                    strSql.Append("     set @childen=0 \r\n");
                    strSql.Append("     SELECT @childen=(Rgt-lft-1)/2,@myWidth=Lft FROM d_Department where id=@ParentID; \r\n");//增加子级，如果childen不为0，@myWidth会被覆盖
                    strSql.Append("     if (@childen > 0) \r\n");
                    strSql.Append("         SELECT top 1 @myWidth=Rgt FROM d_Department WHERE parentid=@ParentID order by lft desc; \r\n");//父类第一子类之最后一项，增加同级
                    strSql.Append("end \r\n");
                    strSql.Append("update d_Department set lft=lft+2 where lft>@myWidth; \r\n");
                    strSql.Append("update d_Department set rgt=rgt+2 where rgt>@myWidth; \r\n");

                    strSql.AppendFormat("INSERT INTO d_Department (ID,ParentID,Name,Code,Lft,Rgt) VALUES (@ID,@ParentID,'{0}','{1}',@myWidth+1,@myWidth+2); \r\n"
                        , titem.Name.Replace("'", ""), titem.Code.Replace("'", ""));
                    i++;
                }
                revlue = DBHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), null);
            }
            return revlue;
        }

        public int Delete(List<string> ids)
        {
            StringBuilder strSql = new StringBuilder();
            if (ids.Count > 0)
            {
                strSql.Append("declare @myLeft int \r\n");
                strSql.Append("declare @myRight int \r\n");
                strSql.Append("declare @myWidth int \r\n");
                foreach (string id in ids)
                {
                    strSql.AppendFormat("SELECT @myLeft=Lft,@myRight=Rgt,@myWidth=rgt-lft+1 FROM d_Department WHERE ID={0}; \r\n", id);
                    strSql.Append("delete d_Department where lft BETWEEN @myLeft AND @myRight; \r\n");
                    strSql.Append("update d_Department set Lft=Lft-@myWidth where Lft>@myRight; \r\n");
                    strSql.Append("update d_Department set Rgt=Rgt-@myWidth where Rgt>@myRight; \r\n");
                }
                return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            }
            return 0;
        }

        private const string osql = @"SELECT node.id,node.lft,node.rgt,node.name,node.code,node.parentid,COUNT(parent.name) AS path
                                    ,(case node.rgt when node.lft + 1 then 1 else 0 end) as IsLeaf,(node.Rgt-node.lft-1)/2 as children
                                    FROM d_Department AS node,
                                    d_Department AS parent
                                    WHERE node.lft BETWEEN parent.lft AND parent.rgt {0} 
                                    GROUP BY node.id,node.lft,node.rgt,node.name,node.code,node.parentid ";
        /// <summary>
        /// _id:0全部
        /// _type:1,所有子类,不包含自己;2包含自己的所有子类;3不包含自己所有父类4;包含自己所有父类
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_type"></param>
        /// <returns></returns>
        public List<d_Department> GetList(int _id, int _type)
        {
            StringBuilder strSql = new StringBuilder();
            if (_id > 0)
            {
                strSql.Append("declare @lft int \r\n");
                strSql.Append("declare @rgt int \r\n");
                strSql.AppendFormat("select @lft=Lft,@rgt=Rgt from d_Department where ID={0};\r\n", _id);
                switch (_type)
                {
                    case 1:
                        strSql.AppendFormat(osql, "and (node.lft>@lft AND node.Rgt<@rgt)");
                        break;
                    case 2:
                        strSql.AppendFormat(osql, "and (node.lft>=@lft AND node.Rgt<=@rgt)");
                        break;
                    case 3:
                        strSql.AppendFormat(osql, "and (node.lft<@lft AND node.Rgt>@rgt)");
                        break;
                    case 4:
                        strSql.AppendFormat(osql, "and (node.lft<=@lft AND node.Rgt>=@rgt)");
                        break;
                    default:
                        strSql.AppendFormat(osql, "and (node.lft>=@lft AND node.Rgt<=@rgt)");
                        break;
                }
            }
            else if (_type == 5)
                strSql.AppendFormat(osql, "and (node.Rgt-node.lft-1)/2!=0");
            else
                strSql.AppendFormat(osql, " ");
            strSql.Append(" order by node.lft ASC");
            List<d_Department> list = new List<d_Department>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
                GetItem(list, dr);
            return list;
        }
        public d_Department GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ID,ParentID,Name,Code,Lft,Rgt,1 as Path,(case rgt when lft + 1 then 1 else 0 end) as Isleaf,(Rgt-lft-1)/2 as children FROM d_Department ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            d_Department item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
                item = GetItem(item, dr);
            return item;
        }
        #region 私有
        private d_Department GetItem(d_Department model, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        model = new d_Department();
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
        private void GetModel(d_Department model, DbDataReader dr)
        {
            model.ID = DBHelper.GetInt(dr["ID"]);
            model.Lft = DBHelper.GetInt(dr["Lft"]);
            model.Rgt = DBHelper.GetInt(dr["Rgt"]);
            model.Name = DBHelper.GetString(dr["Name"]);
            model.Code = DBHelper.GetString(dr["Code"]);
            model.ParentID = DBHelper.GetInt(dr["ParentID"]);
            model.Path = DBHelper.GetInt(dr["Path"]);
            model.IsLeaf = DBHelper.GetBool(dr["IsLeaf"]);
            model.Children = DBHelper.GetInt(dr["Children"]);
        }
        private List<d_Department> GetItem(List<d_Department> list, DbDataReader dr)
        {
            try
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        d_Department model = new d_Department();
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

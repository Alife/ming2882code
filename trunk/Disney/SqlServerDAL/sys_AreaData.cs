using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Models;

namespace SqlServerDAL
{
    public class sys_AreaData : DALHelper
    {//(COUNT(parent.name)-1) AS path
        private const string osql = @"SELECT node.id,node.lft,node.rgt,node.name,node.code,node.pinyin,node.parentid,COUNT(parent.name) AS path
                                    ,(case node.rgt when node.lft + 1 then 1 else 0 end) as IsLeaf,(node.Rgt-node.lft-1)/2 as children
                                    FROM sys_area AS node,
                                    sys_area AS parent
                                    WHERE node.lft BETWEEN parent.lft AND parent.rgt {0} 
                                    GROUP BY node.id,node.lft,node.rgt,node.name,node.code,node.pinyin,node.parentid ";
        /// <summary>
        /// _id:0全部
        /// _type:1,所有子类,不包含自己;2包含自己的所有子类;3不包含自己所有父类4;包含自己所有父类
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_type"></param>
        /// <returns></returns>
        public List<sys_Area> GetList(int _id, int _type)
        {
            List<sys_Area> list = new List<sys_Area>();
            StringBuilder strSql = new StringBuilder();
            if (_id > 0)
            {
                strSql.Append("declare @lft int \r\n");
                strSql.Append("declare @rgt int \r\n");
                strSql.AppendFormat("select @lft=Lft,@rgt=Rgt from sys_Area where ID={0};\r\n", _id);
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
                        strSql.AppendFormat(osql, "and (node.lft>@lft AND node.Rgt<@rgt)");
                        break;
                }
            }
            else if (_type == 5)
                strSql.AppendFormat(osql, "and (node.Rgt-node.lft-1)/2!=0");
            else
                strSql.AppendFormat(osql, " ");
                //strSql.AppendFormat(osql, " and node.lft!=1 ", "order by node.lft ASC");
            strSql.Append(" order by node.lft ASC");
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            list.Add(GetItem(new sys_Area(), dr));
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

        public List<sys_Area> GetList(int parentID)
        {
            List<sys_Area> list = new List<sys_Area>();
            StringBuilder strSql = new StringBuilder();
            //strSql.AppendFormat(osql + " order by node.lft ASC", "and node.parentid=" + parentID);
            strSql.Append("SELECT ID,ParentID,Name,Code,pinyin,Lft,Rgt,1 as Path,(case rgt when lft + 1 then 1 else 0 end) as Isleaf,(Rgt-lft-1)/2 as children FROM sys_Area");
            strSql.AppendFormat(" where parentid={0}", parentID);
            strSql.Append(" order by Code");
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            list.Add(GetItem(new sys_Area(), dr));
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

        public sys_Area GetItem(int _id)
        {
            sys_Area item = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ID,ParentID,Name,Code,pinyin,Lft,Rgt,1 as Path,(case rgt when lft + 1 then 1 else 0 end) as Isleaf,(Rgt-lft-1)/2 as children FROM sys_Area ");
            strSql.Append(" WHERE ID=@in_ID");
            DbParameter[] cmdParms = {
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, _id)};
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            item = GetItem(new sys_Area(), dr);
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

        public int Insert(sys_Area item)
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
            strSql.Append("     if exists (SELECT top 1 ID FROM sys_Area order by Rgt desc) \r\n");
            strSql.Append("     begin   \r\n");
            strSql.Append("         SELECT top 1 @myWidth=Rgt FROM sys_Area order by Rgt desc;  \r\n");
            strSql.Append("     end   \r\n");
            strSql.Append("end \r\n");
            strSql.Append("else \r\n");
            strSql.Append("begin \r\n");
            strSql.Append("     SELECT @childen=(Rgt-lft-1)/2,@myWidth=Lft FROM sys_Area where id=@ParentID; \r\n");//增加子级，如果childen不为0，@myWidth会被覆盖
            strSql.Append("     if (@childen > 0) \r\n");
            strSql.Append("         SELECT top 1 @myWidth=Rgt FROM sys_area WHERE parentid=@ParentID order by lft desc; \r\n");//父类第一子类之最后一项，增加同级
            strSql.Append("end \r\n");
            strSql.Append("update sys_area set lft=lft+2 where lft>@myWidth;\r\n");
            strSql.Append("update sys_area set rgt=rgt+2 where rgt>@myWidth;\r\n");
            strSql.Append("select @ID=isnull(max(id),0)+1 from sys_area;\r\n");
            strSql.Append("INSERT INTO sys_area (ID,ParentID,Name,Code,Pinyin,Lft,Rgt) VALUES (@ID,@ParentID,@Name,@Code,@Pinyin,@myWidth+1,@myWidth+2);\r\n");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@ParentID", DbType.Int32, item.ParentID),
                DBHelper.CreateInDbParameter("@Name", DbType.String, item.Name),
                DBHelper.CreateInDbParameter("@Code", DbType.String, item.Code),
                DBHelper.CreateInDbParameter("@Pinyin", DbType.String, item.Pinyin)};
            obj = DBHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), cmdParms);
            if (obj != null)
                return Convert.ToInt32(obj);
            return 0;
        }
        public int Update(sys_Area item)
        {
            StringBuilder strSql = new StringBuilder();
            int revlue = 0;
            if (item.ParentID == -1)
            {
                strSql.Append("update sys_area set Name=@Name,Code=@Code,Pinyin=@Pinyin where ID=@ID; \r\n");
                DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@ID", DbType.Int32, item.ID),
                DBHelper.CreateInDbParameter("@Name", DbType.String, item.Name),
                DBHelper.CreateInDbParameter("@Code", DbType.String, item.Code),
                DBHelper.CreateInDbParameter("@Pinyin", DbType.String, item.Pinyin)};
                revlue = DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
            }
            else
            {
                DbConnection conn = DBHelper.CreateConnection();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                DbTransaction tran = conn.BeginTransaction();
                List<sys_Area> list = GetList(item.ID, 2);
                strSql.Append("declare @myLeft int \r\n");
                strSql.Append("declare @myRight int \r\n");
                strSql.Append("declare @myWidth int \r\n");
                strSql.AppendFormat("SELECT @myLeft=Lft,@myRight=Rgt,@myWidth=rgt-lft+1 FROM sys_Area WHERE ID={0}; \r\n", item.ID);
                strSql.Append("delete sys_area where lft BETWEEN @myLeft AND @myRight; \r\n");
                strSql.Append("update sys_area set Lft=Lft-@myWidth where Lft>@myRight; \r\n");
                strSql.Append("update sys_area set Rgt=Rgt-@myWidth where Rgt>@myRight; \r\n");
                strSql.Append("declare @childen int \r\n");
                strSql.Append("declare @ID int \r\n");
                strSql.Append("declare @ParentID int \r\n");
                int i = 0;
                foreach (sys_Area titem in list)
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
                    strSql.Append("     if exists (SELECT top 1 ID FROM sys_Area order by Rgt desc) \r\n");
                    strSql.Append("     begin   \r\n");
                    strSql.Append("         SELECT top 1 @myWidth=Rgt FROM sys_Area order by Rgt desc;  \r\n");
                    strSql.Append("     end   \r\n");
                    strSql.Append("end \r\n");
                    strSql.Append("else \r\n");
                    strSql.Append("begin \r\n");
                    strSql.Append("     set @childen=0 \r\n");
                    strSql.Append("     SELECT @childen=(Rgt-lft-1)/2,@myWidth=Lft FROM sys_Area where id=@ParentID; \r\n");//增加子级，如果childen不为0，@myWidth会被覆盖
                    strSql.Append("     if (@childen > 0) \r\n");
                    strSql.Append("         SELECT top 1 @myWidth=Rgt FROM sys_area WHERE parentid=@ParentID order by lft desc; \r\n");//父类第一子类之最后一项，增加同级
                    strSql.Append("end \r\n");
                    strSql.Append("update sys_area set lft=lft+2 where lft>@myWidth; \r\n");
                    strSql.Append("update sys_area set rgt=rgt+2 where rgt>@myWidth; \r\n");

                    strSql.AppendFormat("INSERT INTO sys_area (ID,ParentID,Name,Code,Pinyin,Lft,Rgt) VALUES (@ID,@ParentID,'{0}','{1}','{2}',@myWidth+1,@myWidth+2); \r\n"
                        , titem.Name.Replace("'", ""), titem.Code.Replace("'", ""), titem.Pinyin.Replace("'", ""));
                    i++;
                }
                revlue = DBHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), null);
            }
            return revlue;
        }

        public int Delete(List<string> ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("declare @myLeft int \r\n");
            strSql.Append("declare @myRight int \r\n");
            strSql.Append("declare @myWidth int \r\n");
            foreach (string id in ID)
            {
                strSql.AppendFormat("SELECT @myLeft=Lft,@myRight=Rgt,@myWidth=rgt-lft+1 FROM sys_Area WHERE ID={0}; \r\n", id);
                strSql.Append("delete sys_area where lft BETWEEN @myLeft AND @myRight; \r\n");
                strSql.Append("update sys_area set Lft=Lft-@myWidth where Lft>@myRight; \r\n");
                strSql.Append("update sys_area set Rgt=Rgt-@myWidth where Rgt>@myRight; \r\n");
            }
            return DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
        }

        public sys_Area GetItem(sys_Area item, DbDataReader dr)
        {
            item.ID = DBHelper.GetInt(dr["ID"]);
            item.Lft = DBHelper.GetInt(dr["Lft"]);
            item.Rgt = DBHelper.GetInt(dr["Rgt"]);
            item.Name = DBHelper.GetString(dr["Name"]);
            item.Code = DBHelper.GetString(dr["Code"]);
            item.Pinyin = DBHelper.GetString(dr["Pinyin"]);
            item.ParentID = DBHelper.GetInt(dr["ParentID"]);
            item.Path = DBHelper.GetInt(dr["Path"]);
            item.IsLeaf = DBHelper.GetBool(dr["IsLeaf"]);
            item.Children = DBHelper.GetInt(dr["Children"]);
            return item;
        }
    }
}

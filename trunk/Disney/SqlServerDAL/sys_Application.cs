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
    /// 功能模块
    /// </summary>
    public class sys_ApplicationData : DALHelper
    {
        public int Exists(string _Code)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ID from sys_Application");
            builder.AppendFormat(" where Code='{0}' ", _Code);
            object obj = DBHelper.ExecuteScalar(CommandType.Text, builder.ToString(), null);
            if (obj != null)
                return int.Parse(obj.ToString());
            return 0;
        }
        public int Insert(sys_Application item)
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
            strSql.Append("     if exists (SELECT top 1 ID FROM sys_Application order by Rgt desc) \r\n");
            strSql.Append("     begin   \r\n");
            strSql.Append("         SELECT top 1 @myWidth=Rgt FROM sys_Application order by Rgt desc;  \r\n");
            strSql.Append("     end   \r\n");
            strSql.Append("end \r\n");
            strSql.Append("else \r\n");
            strSql.Append("begin \r\n");
            strSql.Append("     SELECT @childen=(Rgt-lft-1)/2,@myWidth=Lft FROM sys_Application where id=@ParentID; \r\n");//增加子级，如果childen不为0，@myWidth会被覆盖
            strSql.Append("     if (@childen > 0) \r\n");
            strSql.Append("         SELECT top 1 @myWidth=Rgt FROM sys_Application WHERE parentid=@ParentID order by lft desc; \r\n");//父类第一子类之最后一项，增加同级
            strSql.Append("end \r\n");
            strSql.Append("update sys_Application set lft=lft+2 where lft>@myWidth;\r\n");
            strSql.Append("update sys_Application set rgt=rgt+2 where rgt>@myWidth;\r\n");
            strSql.Append("select @ID=isnull(max(id),0)+1 from sys_Application;\r\n");
            strSql.Append("INSERT INTO sys_Application (ID,ParentID,Name,Code,Url,Description,IsHidden,Icon,Lft,Rgt) ");
            strSql.Append("VALUES (@ID,@ParentID,@Name,@Code,@Url,@Description,@IsHidden,@Icon,@myWidth+1,@myWidth+2);\r\n");
            DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@ParentID", DbType.Int32, item.ParentID),
                DBHelper.CreateInDbParameter("@Name", DbType.String, item.Name),
                DBHelper.CreateInDbParameter("@Code", DbType.String, item.Code),
                DBHelper.CreateInDbParameter("@Url", DbType.String, item.Url),
				DBHelper.CreateInDbParameter("@Description", DbType.String, item.Description),
				DBHelper.CreateInDbParameter("@IsHidden", DbType.Boolean, item.IsHidden),
				DBHelper.CreateInDbParameter("@Icon", DbType.String, item.Icon)};
            obj = DBHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), cmdParms);
            if (obj != null)
                return Convert.ToInt32(obj);
            return 0;
        }
        public int Update(sys_Application item)
        {
            StringBuilder strSql = new StringBuilder();
            int revlue = 0;
            if (item.ParentID == -1)
            {
                strSql.Append("update sys_Application set Name=@Name,Code=@Code,Url=@Url,Description=@Description,IsHidden=@IsHidden,Icon=@Icon where ID=@ID; \r\n");
                DbParameter[] cmdParms = new DbParameter[]{
                DBHelper.CreateInDbParameter("@ID", DbType.Int32, item.ID),
                DBHelper.CreateInDbParameter("@Name", DbType.String, item.Name),
                DBHelper.CreateInDbParameter("@Code", DbType.String, item.Code),
                DBHelper.CreateInDbParameter("@Url", DbType.String, item.Url),
				DBHelper.CreateInDbParameter("@Description", DbType.String, item.Description),
				DBHelper.CreateInDbParameter("@IsHidden", DbType.Boolean, item.IsHidden),
				DBHelper.CreateInDbParameter("@Icon", DbType.String, item.Icon)};
                revlue = DBHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
            }
            else
            {
                DbConnection conn = DBHelper.CreateConnection();
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                DbTransaction tran = conn.BeginTransaction();
                List<sys_Application> list = GetList(item.ID, 2);
                strSql.Append("declare @myLeft int \r\n");
                strSql.Append("declare @myRight int \r\n");
                strSql.Append("declare @myWidth int \r\n");
                strSql.AppendFormat("SELECT @myLeft=Lft,@myRight=Rgt,@myWidth=rgt-lft+1 FROM sys_Application WHERE ID={0}; \r\n", item.ID);
                strSql.Append("delete sys_Application where lft BETWEEN @myLeft AND @myRight; \r\n");
                strSql.Append("update sys_Application set Lft=Lft-@myWidth where Lft>@myRight; \r\n");
                strSql.Append("update sys_Application set Rgt=Rgt-@myWidth where Rgt>@myRight; \r\n");
                strSql.Append("declare @childen int \r\n");
                strSql.Append("declare @ID int \r\n");
                strSql.Append("declare @ParentID int \r\n");
                int i = 0;
                foreach (sys_Application titem in list)
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
                    strSql.Append("     if exists (SELECT top 1 ID FROM sys_Application order by Rgt desc) \r\n");
                    strSql.Append("     begin   \r\n");
                    strSql.Append("         SELECT top 1 @myWidth=Rgt FROM sys_Application order by Rgt desc;  \r\n");
                    strSql.Append("     end   \r\n");
                    strSql.Append("end \r\n");
                    strSql.Append("else \r\n");
                    strSql.Append("begin \r\n");
                    strSql.Append("     set @childen=0 \r\n");
                    strSql.Append("     SELECT @childen=(Rgt-lft-1)/2,@myWidth=Lft FROM sys_Application where id=@ParentID; \r\n");//增加子级，如果childen不为0，@myWidth会被覆盖
                    strSql.Append("     if (@childen > 0) \r\n");
                    strSql.Append("         SELECT top 1 @myWidth=Rgt FROM sys_Application WHERE parentid=@ParentID order by lft desc; \r\n");//父类第一子类之最后一项，增加同级
                    strSql.Append("end \r\n");
                    strSql.Append("update sys_Application set lft=lft+2 where lft>@myWidth; \r\n");
                    strSql.Append("update sys_Application set rgt=rgt+2 where rgt>@myWidth; \r\n");

                    strSql.AppendFormat("INSERT INTO sys_Application (ID,ParentID,Name,Code,Url,Description,IsHidden,Icon,Lft,Rgt) ");
                    strSql.AppendFormat("VALUES (@ID,@ParentID,'{0}','{1}','{2}','{3}','{4}','{5}',@myWidth+1,@myWidth+2); \r\n"
                        , titem.Name.Replace("'", ""), titem.Code.Replace("'", ""), titem.Url.Replace("'", ""), titem.Description.Replace("'", "")
                        , titem.IsHidden ? 1 : 0, titem.Icon.Replace("'", ""));
                    i++;
                }
                revlue = DBHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), null);
            }
            return revlue;
        }
        public int Delete(List<string> ID)
        {
            StringBuilder strSql = new StringBuilder();
            DbConnection conn = DBHelper.CreateConnection();
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            DbTransaction tran = conn.BeginTransaction();
            strSql.Append("declare @myLeft int \r\n");
            strSql.Append("declare @myRight int \r\n");
            strSql.Append("declare @myWidth int \r\n");
            foreach (string id in ID)
            {
                strSql.AppendFormat("SELECT @myLeft=Lft,@myRight=Rgt,@myWidth=rgt-lft+1 FROM sys_Application WHERE ID={0}; \r\n", id);
                string temp = "select id from sys_Application where lft BETWEEN @myLeft AND @myRight";
                strSql.AppendFormat("DELETE FROM sys_Field WHERE OperationID in ({0});\r\n", temp);
                strSql.AppendFormat("DELETE FROM sys_PermissionField WHERE PermissionID in "+
                    "(select a.id from sys_Permission as a left join sys_Operation as b on a.OperationID=b.id where b.ApplicationID in ({0}));\r\n", temp);
                strSql.AppendFormat("DELETE FROM sys_Permission WHERE OperationID in (select id from sys_Operation where ApplicationID in ({0}));\r\n", temp);
                strSql.AppendFormat("DELETE FROM sys_Operation WHERE ApplicationID in ({0});\r\n", temp);

                strSql.Append("delete sys_Application where lft BETWEEN @myLeft AND @myRight; \r\n");
                strSql.Append("update sys_Application set Lft=Lft-@myWidth where Lft>@myRight; \r\n");
                strSql.Append("update sys_Application set Rgt=Rgt-@myWidth where Rgt>@myRight; \r\n");
            }
            return DBHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), null);
        }
        public sys_Application GetItem(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ID,ParentID,Name,Url,Code,Description,IsHidden,Icon,Lft,Rgt,1 as Path,(case rgt when lft + 1 then 1 else 0 end) as Isleaf,(Rgt-lft-1)/2 as children ");
            strSql.Append(" FROM sys_Application WHERE ID=@in_ID");
            DbParameter[] cmdParms = new DbParameter[]{
				DBHelper.CreateInDbParameter("@in_ID", DbType.Int32, ID)};
            sys_Application item = null;
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            item = GetItem(new sys_Application(), dr);
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
        private const string osql = @"SELECT node.id,node.lft,node.rgt,node.name,node.code,node.url,node.parentid,node.Description,node.IsHidden,node.Icon,COUNT(parent.name) AS path
                                    ,(case node.rgt when node.lft + 1 then 1 else 0 end) as IsLeaf,(node.Rgt-node.lft-1)/2 as children
                                    FROM sys_Application AS node,
                                    sys_Application AS parent
                                    WHERE node.lft BETWEEN parent.lft AND parent.rgt {0} 
                                    GROUP BY node.id,node.lft,node.rgt,node.name,node.code,node.url,node.parentid,node.Description,node.IsHidden,node.Icon ";
        /// <summary>
        /// _id:0全部
        /// _type:1,所有子类,不包含自己;2包含自己的所有子类;3不包含自己所有父类4;包含自己所有父类
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_type"></param>
        /// <returns></returns>
        public List<sys_Application> GetList(int _id, int _type)
        {
            List<sys_Application> list = new List<sys_Application>();
            StringBuilder strSql = new StringBuilder();
            if (_id > 0)
            {
                strSql.Append("declare @lft int \r\n");
                strSql.Append("declare @rgt int \r\n");
                strSql.AppendFormat("select @lft=Lft,@rgt=Rgt from sys_Application where ID={0};\r\n", _id);
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
            else
            {
                if (_type == 1)
                    strSql.AppendFormat(osql, " and node.parentid=0");
                else
                    strSql.AppendFormat(osql, " ");
            }
            strSql.Append(" order by node.lft ASC");
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            list.Add(GetItem(new sys_Application(), dr));
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
        public List<sys_Application> GetUserList(int uid, int parentID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct sys_Application.*,1 as Path,(case rgt when lft + 1 then 1 else 0 end) as Isleaf,(Rgt-lft-1)/2 as children from sys_Application ");
            strSql.Append("inner join sys_Operation on sys_Operation.ApplicationID=sys_Application.ID ");
            strSql.Append("inner join sys_Permission on sys_Permission.OperationID=sys_Operation.ID ");
            strSql.Append("inner join sys_Role on sys_Role.ID=sys_Permission.RoleID ");
            strSql.Append("inner join sys_UserRole on sys_Role.ID=sys_UserRole.RoleID  ");
            strSql.Append("inner join t_User on sys_UserRole.UserID=t_User.ID ");
            strSql.AppendFormat("where t_User.ID={0} and ParentID={1} and sys_Operation.Code='browse' and IsHidden=0 order by lft ", uid, parentID);
            List<sys_Application> list = new List<sys_Application>();
            using (DbDataReader dr = DBHelper.ExecuteReader(CommandType.Text, strSql.ToString(), null))
            {
                try
                {
                    if (dr != null)
                    {
                        while (dr.Read())
                            list.Add(GetItem(new sys_Application(), dr));
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
        private sys_Application GetItem(sys_Application item, DbDataReader dr)
        {
            item.ID = DBHelper.GetInt(dr["ID"]);
            item.Code = DBHelper.GetString(dr["Code"]);
            item.Name = DBHelper.GetString(dr["Name"]);
            item.Url = DBHelper.GetString(dr["Url"]);
            item.Description = DBHelper.GetString(dr["Description"]);
            item.IsHidden = DBHelper.GetBool(dr["IsHidden"]);
            item.Icon = DBHelper.GetString(dr["Icon"]);
            item.ParentID = DBHelper.GetInt(dr["ParentID"]);
            item.Lft = DBHelper.GetInt(dr["Lft"]);
            item.Rgt = DBHelper.GetInt(dr["Rgt"]);
            item.Path = DBHelper.GetInt(dr["Path"]);
            item.IsLeaf = DBHelper.GetBool(dr["IsLeaf"]);
            item.Children = DBHelper.GetInt(dr["Children"]);
            return item;
        }
    }
}

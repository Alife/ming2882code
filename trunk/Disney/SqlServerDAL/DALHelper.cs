//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2009 Socansoft.com 版权所有
// 创建描述: SocanCode代码生成器 V4.3.3.6 自动创建于 2009-9-23 18:48:20
//
// 功能描述: 
//
// 修改标识: 
// 修改描述: 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using DBUtility;

namespace SqlServerDAL
{
    public class DALHelper
    {
        protected static DBHelper DBHelper = GetHelper("DB");

        /// <summary>
        /// 从Web.config从读取数据库的连接以及数据库类型
        /// </summary>
        private static DBHelper GetHelper(string connectionStringName)
        {
            DBHelper dbHelper = new DBHelper();

            // 从Web.config中读取数据库类型
            string providerName = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;
            switch (providerName)
            {
                case "System.Data.OracleClient":
                    dbHelper.DatabaseType = DBHelper.DatabaseTypes.Oracle;
                    break;
                case "MySql.Data.MySqlClient":
                    dbHelper.DatabaseType = DBHelper.DatabaseTypes.MySql;
                    break;
                case "System.Data.OleDb":
                    dbHelper.DatabaseType = DBHelper.DatabaseTypes.OleDb;
                    break;
                case "System.Data.SqlClient":
                default:
                    dbHelper.DatabaseType = DBHelper.DatabaseTypes.Sql;
                    break;
            }

            // 从Web.config中读取数据库连接
            switch (dbHelper.DatabaseType)
            {
                case DBHelper.DatabaseTypes.OleDb:
                    dbHelper.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=true;Data Source="
                        + HttpContext.Current.Request.PhysicalApplicationPath
                        + System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
                    break;
                default:
                    dbHelper.ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
                    break;
            }

            return dbHelper;
        }
    }
}

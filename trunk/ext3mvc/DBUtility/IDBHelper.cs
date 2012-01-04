using System;
using System.Data.Common;
using System.Data;
using System.Collections.Generic;

namespace DBUtility
{
    interface IDBHelper
    {
        /// <summary>
        /// 执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, List<CommandInfo> cmdList);
        /// <summary>
        /// 执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        int ExecuteNonQuery(DbConnection connectionString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms);
        /// <summary>
        /// 执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms);

        /// <summary>
        /// 在事务中执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] cmdParms);

        /// <summary>
        /// 在事务中执行查询，返回DataSet
        /// </summary>
        DataSet ExecuteQuery(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] cmdParms);

        /// <summary>
        /// 执行查询，返回DataSet
        /// </summary>
        DataSet ExecuteQuery(string connectionString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms);

        /// <summary>
        /// 执行查询，返回DataReader
        /// </summary>
        DbDataReader ExecuteReader(DbConnection connectionString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms);
        /// <summary>
        /// 在事务中执行查询，返回DataReader
        /// </summary>
        DbDataReader ExecuteReader(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] cmdParms);

        /// <summary>
        /// 执行查询，返回DataReader
        /// </summary>
        DbDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms);

        /// <summary>
        /// 在事务中执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        object ExecuteScalar(DbConnection connectionString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms);
        /// <summary>
        /// 在事务中执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        object ExecuteScalar(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] cmdParms);

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms);

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="tblName">表名</param>
        /// <param name="fldName">字段名</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="fldSort">排序字段</param>
        /// <param name="fldDir">升序{False}/降序(True)</param>
        /// <param name="condition">条件(不需要where)</param>
        DbDataReader GetPageList(string connectionString, string tblName, int pageSize, int pageIndex, string fldSort, bool fldDir, string condition);

        /// <summary>
        /// 得到数据条数
        /// </summary>
        /// <param name="tblName">表名</param>
        /// <param name="condition">条件(不需要where)</param>
        /// <returns>数据条数</returns>
        int GetCount(string connectionString, string tblName, string condition);
    }
}

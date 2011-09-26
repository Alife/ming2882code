using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Data.OracleClient;
using System.Collections.Generic;

namespace DBUtility
{
    /// <summary>
    /// 数据库操作基类(for Oracle)
    /// </summary>
    internal class OracleHelper : IDBHelper
    {
        /// <summary>
        /// 获取分页SQL
        /// </summary>
        /// <param name="tblName">表名</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="fldSort">排序字段（最后一个不需要填写正序还是倒序，例如：id asc, name）</param>
        /// <param name="fldDir">最后一个排序字段的正序或倒序（true为倒序，false为正序）</param>
        /// <param name="condition">条件</param>
        /// <returns>返回用于分页的SQL语句</returns>
        private string GetPagerSQL(string tblName, int pageSize, int pageIndex, string fldSort, bool fldDir, string condition)
        {
            string strDir = fldDir ? " ASC" : " DESC";

            StringBuilder strSql = new StringBuilder("select X.* from(");
            strSql.AppendFormat(" select row_number() over(order by {0} {1}) as ROW_NO, * from {2}", fldSort, strDir, tblName);
            if (!string.IsNullOrEmpty(condition))
            {
                strSql.AppendFormat(" where {0}) X", condition);
            }
            else
            {
                strSql.AppendFormat(") X");
            }

            strSql.AppendFormat(" where X.ROW_NO <= {0} and X.ROW_NO >= {1}", pageSize * pageIndex, pageSize * (pageIndex - 1) + 1);

            return strSql.ToString();
        }

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
        public DbDataReader GetPageList(string connectionString, string tblName, int pageSize,
            int pageIndex, string fldSort, bool fldDir, string condition)
        {
            string sql = GetPagerSQL(tblName, pageSize, pageIndex, fldSort, fldDir, condition);
            return ExecuteReader(connectionString, CommandType.Text, sql, null);
        }

        /// <summary>
        /// 得到数据条数
        /// </summary>
        public int GetCount(string connectionString, string tblName, string condition)
        {
            StringBuilder sql = new StringBuilder("select count(*) from " + tblName);
            if (!string.IsNullOrEmpty(condition))
                sql.Append(" where " + condition);

            object count = ExecuteScalar(connectionString, CommandType.Text, sql.ToString(), null);
            return int.Parse(count.ToString());
        }

        /// <summary>
        /// 执行查询，返回DataSet
        /// </summary>
        public DataSet ExecuteQuery(string connectionString, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                    using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                        return ds;
                    }
                }
            }
        }

        /// <summary>
        /// 在事务中执行查询，返回DataSet
        /// </summary>
        public DataSet ExecuteQuery(DbTransaction trans, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "ds");
            cmd.Parameters.Clear();
            return ds;
        }
        public int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, List<CommandInfo> cmdList)
        {
            int num = 0;
            string cmdText = string.Empty;
            DbParameter[] cmdParms = null;
            OracleCommand cmd = new OracleCommand();
            try
            {
                foreach (CommandInfo info in cmdList)
                {
                    cmdText = info.CommandText;
                    cmdParms = (DbParameter[])info.Parameters;
                    PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
                    if ((info.EffentNextType == EffentNextType.WhenHaveContine) || (info.EffentNextType == EffentNextType.WhenNoHaveContine))
                    {
                        if (info.CommandText.ToLower().IndexOf("count(") == -1)
                        {
                            trans.Rollback();
                            return 0;
                        }
                        object obj = cmd.ExecuteScalar();
                        bool flag = false;
                        if ((obj == null) && (obj == DBNull.Value))
                            flag = false;
                        flag = Convert.ToInt32(obj) > 0;
                        if (!((info.EffentNextType != EffentNextType.WhenHaveContine) || flag))
                        {
                            trans.Rollback();
                            return 0;
                        }
                        if ((info.EffentNextType == EffentNextType.WhenNoHaveContine) && flag)
                        {
                            trans.Rollback();
                            return 0;
                        }
                        continue;
                    }
                    int num2 = cmd.ExecuteNonQuery();
                    num += num2;
                    if ((info.EffentNextType == EffentNextType.ExcuteEffectRows) && (num2 == 0))
                    {
                        trans.Rollback();
                        return 0;
                    }
                    cmd.Parameters.Clear();
                }
                trans.Commit();
                return num;
            }
            finally
            {
                trans.Connection.Close();
            }
            return num;
        }
        /// <summary>
        /// 执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        public int ExecuteNonQuery(DbConnection connectionString, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();

            using (connectionString)
            {
                PrepareCommand(cmd, connectionString, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        public int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 在事务中执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        public int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行查询，返回DataReader
        /// </summary>
        public DbDataReader ExecuteReader(DbConnection connectionString, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();

            try
            {
                PrepareCommand(cmd, connectionString, null, cmdType, cmdText, cmdParms);
                OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                connectionString.Close();
                throw;
            }
        }
        /// <summary>
        /// 执行查询，返回DataReader
        /// </summary>
        public DbDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(connectionString);

            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 在事务中执行查询，返回DataReader
        /// </summary>
        public DbDataReader ExecuteReader(DbTransaction trans, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return rdr;
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        public object ExecuteScalar(DbConnection connectionString, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();

            using (connectionString)
            {
                PrepareCommand(cmd, connectionString, null, cmdType, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        public object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 在事务中执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        public object ExecuteScalar(DbTransaction trans, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 生成要执行的命令
        /// </summary>
        private static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType,
            string cmdText, DbParameter[] cmdParms)
        {
            // 如果存在参数，则表示用户是用参数形式的SQL语句，可以替换
            if (cmdParms != null && cmdParms.Length > 0)
                cmdText = cmdText.Replace("@", ":").Replace("?", ":");

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                {
                    parm.ParameterName = parm.ParameterName.Replace("@", ":").Replace("?", ":");
                    cmd.Parameters.Add(parm);
                }
            }
        }
    }
}

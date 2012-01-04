using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DBUtility
{
    /// <summary>
    /// 数据库操作基类(for Sql2000/2005)
    /// </summary>
    internal class SqlHelper : IDBHelper
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

            if (pageIndex == 1)
            {
                return "select top " + pageSize.ToString() + " * from " + tblName.ToString()
                    + ((string.IsNullOrEmpty(condition)) ? string.Empty : (" where " + condition))
                    + " order by " + fldSort.ToString() + strDir;
            }
            else
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("select top {0} * from {1} ", pageSize, tblName);
                strSql.AppendFormat(" where {1} not in (select top {0} {1} from {2} ", pageSize * (pageIndex - 1),
                    (fldSort.Substring(fldSort.LastIndexOf(',') + 1, fldSort.Length - fldSort.LastIndexOf(',') - 1)), tblName);
                if (!string.IsNullOrEmpty(condition))
                {
                    strSql.AppendFormat(" where {0} order by {1}{2}) and {0}", condition, fldSort, strDir);
                }
                else
                {
                    strSql.AppendFormat(" order by {0}{1}) ", fldSort, strDir);
                }
                strSql.AppendFormat(" order by {0}{1}", fldSort, strDir);
                return strSql.ToString();
            }
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

        #region 无乎没用
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
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
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
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "ds");
            cmd.Parameters.Clear();
            return ds;
        }
        #endregion
        public int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, List<CommandInfo> cmdList)
        {
            int num = 0;
            string cmdText = string.Empty;
            DbParameter[] cmdParms = null;
            SqlCommand cmd = new SqlCommand();
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
            catch (SqlException exception)
            {
                trans.Rollback();
                WriteErrLog(cmdText, exception.Message, cmdParms);
            }
            catch (Exception exception2)
            {
                trans.Rollback();
                WriteErrLog(cmdText, exception2.Message, cmdParms);
            }
            finally
            {
                cmd.Dispose();
                cmd.Connection.Close();
            }
            return num;
        }
        /// <summary>
        /// 执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        public int ExecuteNonQuery(DbConnection connectionString, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            int val = 0;
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connectionString, null, cmdType, cmdText, cmdParms);
                val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                WriteErrLog(cmdText, e.Message, cmdParms);
            }
            catch (Exception e)
            {
                WriteErrLog(cmdText, e.Message, cmdParms);
            }
            return val;
        }

        /// <summary>
        /// 执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        public int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            int val = 0;
            SqlCommand cmd = new SqlCommand();
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, cmdParms);
                val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                WriteErrLog(cmdText, e.Message, cmdParms);
            }
            catch (Exception e)
            {
                WriteErrLog(cmdText, e.Message, cmdParms);
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
            return val;
        }

        /// <summary>
        /// 在事务中执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        public int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            int val = 0;
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
                val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                trans.Commit();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                trans.Rollback();
                WriteErrLog(cmdText, e.Message, cmdParms);
            }
            catch (Exception e)
            {
                trans.Rollback();
                WriteErrLog(cmdText, e.Message, cmdParms);
            }
            finally
            {
                cmd.Dispose();
                cmd.Connection.Close();
            }
            return val;
        }

        /// <summary>
        /// 执行查询，返回DataReader
        /// </summary>
        public DbDataReader ExecuteReader(DbConnection connectionString, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            SqlDataReader rdr = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connectionString, null, cmdType, cmdText, cmdParms);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                WriteErrLog(cmdText, e.Message, cmdParms);
            }
            catch (Exception e)
            {
                WriteErrLog(cmdText, e.Message, cmdParms);
            }
            return rdr;
        }
        /// <summary>
        /// 执行查询，返回DataReader
        /// </summary>
        public DbDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            SqlDataReader rdr = null;
            SqlCommand cmd = new SqlCommand();
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, cmdParms);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                cmd.Dispose();
                connection.Close();
                WriteErrLog(cmdText, e.Message, cmdParms);
            }
            catch (Exception e)
            {
                cmd.Dispose();
                connection.Close();
                WriteErrLog(cmdText, e.Message, cmdParms);
            }
            return rdr;
        }

        /// <summary>
        /// 在事务中执行查询，返回DataReader
        /// </summary>
        public DbDataReader ExecuteReader(DbTransaction trans, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            SqlDataReader rdr = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                trans.Commit();
                return rdr;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                trans.Rollback();
                cmd.Dispose();
                cmd.Connection.Close();
                WriteErrLog(cmdText, e.Message, cmdParms);
            }
            catch (Exception e)
            {
                trans.Rollback();
                cmd.Dispose();
                cmd.Connection.Close();
                WriteErrLog(cmdText, e.Message, cmdParms);
            }
            return rdr;
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        public object ExecuteScalar(DbConnection connectionString, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            object val = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connectionString, null, cmdType, cmdText, cmdParms);
                val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                WriteErrLog(cmdText, e.Message, cmdParms);
            }
            catch (Exception e)
            {
                WriteErrLog(cmdText, e.Message, cmdParms);
            }
            if (Object.Equals(val, null) || Object.Equals(val, System.DBNull.Value))
                return null;
            return val;
        }
        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        public object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            object val = null;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, cmdParms);
                val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                WriteErrLog(cmdText, e.Message, cmdParms);
            }
            catch (Exception e)
            {
                WriteErrLog(cmdText, e.Message, cmdParms);
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
            if (Object.Equals(val, null) || Object.Equals(val, System.DBNull.Value))
                return null;
            return val;
        }

        /// <summary>
        /// 在事务中执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        public object ExecuteScalar(DbTransaction trans, CommandType cmdType, string cmdText,
            params DbParameter[] cmdParms)
        {
            object val = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
                val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                trans.Commit();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                trans.Rollback();
                WriteErrLog(cmdText, e.Message, cmdParms);
            }
            catch (Exception e)
            {
                trans.Rollback();
                WriteErrLog(cmdText, e.Message, cmdParms);
            }
            finally
            {
                cmd.Dispose();
                cmd.Connection.Close();
            }
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
                cmdText = cmdText.Replace("?", "@").Replace(":", "@");

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (DbParameter parm in cmdParms)
                {
                    parm.ParameterName = parm.ParameterName.Replace("?", "@").Replace(":", "@");
                    cmd.Parameters.Add(parm);
                }
            }
        }
        /// <summary>
        /// 写执行SQL错误信息日志
        /// </summary>
        /// <param name="sql">执行的SQL语句</param>
        /// <param name="message">错误信息</param>
        private static void WriteErrLog(string sql, string message, params IDataParameter[] parameters)
        {
            string path = "/log/" + DateTime.Now.ToShortDateString() + "/";
            if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(path));
            path = System.Web.HttpContext.Current.Server.MapPath(path + "log.txt");
            System.IO.StreamWriter sw = null;
            try
            {
                if (!System.IO.File.Exists(path))
                    sw = System.IO.File.CreateText(path);
                else
                    sw = System.IO.File.AppendText(path);
                sw.WriteLine("[" + DateTime.Now + "]");
                sw.WriteLine("sql=" + sql);
                if (parameters != null)
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        sw.WriteLine("parameter=" + parameter.ParameterName + ":" + parameter.SqlValue);
                    }
                }
                sw.WriteLine("message=" + message);
            }
            catch
            {
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }
    }
}
using System;
using System.Data;
using System.Data.Common;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DBUtility
{
    /// <summary>
    /// 数据库操作
    /// </summary>
    public class DBHelper
    {
        /// <summary>
        /// 枚举：数据库类型
        /// </summary>
        public enum DatabaseTypes
        {
            Sql, MySql, Oracle, OleDb, SQLite
        }

        private DatabaseTypes _databaseType;
        private string _connectionString;
        private IDBHelper _iDBHelper;

        public DBHelper()
        { }

        public DBHelper(DatabaseTypes databaseType, string connectionString)
        {
            DatabaseType = databaseType;
            this._connectionString = connectionString;
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DatabaseTypes DatabaseType
        {
            get
            {
                return _databaseType;
            }
            set
            {
                _databaseType = value;

                switch (value)
                {
                    case DatabaseTypes.OleDb:
                        _iDBHelper = new OleDbHelper();
                        break;
                    case DatabaseTypes.MySql:
                        _iDBHelper = new MySqlHelper();
                        break;
                    case DatabaseTypes.Oracle:
                        _iDBHelper = new OracleHelper();
                        break;
                    case DatabaseTypes.Sql:
                    default:
                        _iDBHelper = new SqlHelper();
                        break;
                }
            }
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        public DbConnection CreateConnection()
        {
            switch (_databaseType)
            {
                case DatabaseTypes.MySql:
                    return new MySqlConnection(_connectionString);
                case DatabaseTypes.Oracle:
                    return new OracleConnection(_connectionString);
                case DatabaseTypes.OleDb:
                    return new OleDbConnection(_connectionString);
                case DatabaseTypes.Sql:
                default:
                    return new SqlConnection(_connectionString);
            }
        }

        #region === 创造DbParameter的实例 ===

        /// <summary>
        /// 创造输入DbParameter的实例
        /// </summary>
        public DbParameter CreateInDbParameter(string paraName, DbType dbType, int size, object value)
        {
            return CreateDbParameter(paraName, dbType, size, value, ParameterDirection.Input);
        }

        /// <summary>
        /// 创造输入DbParameter的实例
        /// </summary>
        public DbParameter CreateInDbParameter(string paraName, DbType dbType, object value)
        {
            return CreateDbParameter(paraName, dbType, 0, value, ParameterDirection.Input);
        }

        /// <summary>
        /// 创造输出DbParameter的实例
        /// </summary>        
        public DbParameter CreateOutDbParameter(string paraName, DbType dbType, int size)
        {
            return CreateDbParameter(paraName, dbType, size, null, ParameterDirection.Output);
        }

        /// <summary>
        /// 创造输出DbParameter的实例
        /// </summary>        
        public DbParameter CreateOutDbParameter(string paraName, DbType dbType)
        {
            return CreateDbParameter(paraName, dbType, 0, null, ParameterDirection.Output);
        }

        /// <summary>
        /// 创造返回DbParameter的实例
        /// </summary>        
        public DbParameter CreateReturnDbParameter(string paraName, DbType dbType, int size)
        {
            return CreateDbParameter(paraName, dbType, size, null, ParameterDirection.ReturnValue);
        }

        /// <summary>
        /// 创造返回DbParameter的实例
        /// </summary>        
        public DbParameter CreateReturnDbParameter(string paraName, DbType dbType)
        {
            return CreateDbParameter(paraName, dbType, 0, null, ParameterDirection.ReturnValue);
        }

        /// <summary>
        /// 创造DbParameter的实例
        /// </summary>
        public DbParameter CreateDbParameter(string paraName, DbType dbType, int size, object value, ParameterDirection direction)
        {
            DbParameter para;
            switch (_databaseType)
            {
                case DatabaseTypes.MySql:
                    para = new MySqlParameter();
                    break;
                case DatabaseTypes.Oracle:
                    para = new OracleParameter();
                    break;
                case DatabaseTypes.OleDb:
                    para = new OleDbParameter();
                    break;
                case DatabaseTypes.Sql:
                default:
                    para = new SqlParameter();
                    break;
            }
            para.ParameterName = paraName;

            if (size != 0)
                para.Size = size;

            para.DbType = dbType;
            if (value == null)
            {
                para.IsNullable = true;
                para.Value = DBNull.Value;
            }
            else
                para.Value = value;

            para.Direction = direction;

            return para;
        }

        #endregion

        #region === 数据库执行方法 ===

        public int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, List<CommandInfo> cmdList)
        {
            return _iDBHelper.ExecuteNonQuery(trans, cmdType, cmdList);
        }
        /// <summary>
        /// 执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        public int ExecuteNonQuery(DbConnection connectionString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return _iDBHelper.ExecuteNonQuery(connectionString, cmdType, cmdText, cmdParms);
        }
        /// <summary>
        /// 执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        public int ExecuteNonQuery(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return _iDBHelper.ExecuteNonQuery(_connectionString, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 在事务中执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        public int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return _iDBHelper.ExecuteNonQuery(trans, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 在事务中执行查询，返回DataSet
        /// </summary>
        public DataSet ExecuteQuery(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return _iDBHelper.ExecuteQuery(trans, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行查询，返回DataSet
        /// </summary>
        public DataSet ExecuteQuery(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return _iDBHelper.ExecuteQuery(_connectionString, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 在事务中执行查询，返回DataReader
        /// </summary>
        public DbDataReader ExecuteReader(DbConnection connectionString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return _iDBHelper.ExecuteReader(connectionString, cmdType, cmdText, cmdParms);
        }
        /// <summary>
        /// 在事务中执行查询，返回DataReader
        /// </summary>
        public DbDataReader ExecuteReader(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return _iDBHelper.ExecuteReader(trans, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行查询，返回DataReader
        /// </summary>
        public DbDataReader ExecuteReader(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return _iDBHelper.ExecuteReader(_connectionString, cmdType, cmdText, cmdParms);
        }
        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        public object ExecuteScalar(DbConnection connectionString, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return _iDBHelper.ExecuteScalar(connectionString, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 在事务中执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        public object ExecuteScalar(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return _iDBHelper.ExecuteScalar(trans, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        public object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return _iDBHelper.ExecuteScalar(_connectionString, cmdType, cmdText, cmdParms);
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
        public DbDataReader GetPageList(string tblName, int pageSize, int pageIndex, string fldSort, bool fldDir, string condition)
        {
            return _iDBHelper.GetPageList(_connectionString, tblName, pageSize, pageIndex, fldSort, fldDir, condition);
        }

        /// <summary>
        /// 得到数据条数
        /// </summary>
        /// <param name="tblName">表名</param>
        /// <param name="condition">条件(不需要where)</param>
        /// <returns>数据条数</returns>
        public int GetCount(string tblName, string condition)
        {
            return _iDBHelper.GetCount(_connectionString, tblName, condition);
        }

        #endregion

        #region === 由Object取值 ===

        /// <summary>
        /// 取得Int16值
        /// </summary>
        public Int16 GetInt16(object obj)
        {
            if (obj != DBNull.Value && obj != null)
            {
                return Convert.ToInt16(obj);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 取得UInt16值
        /// </summary>
        public UInt16 GetUInt16(object obj)
        {
            if (obj != DBNull.Value && obj != null)
            {
                return Convert.ToUInt16(obj);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 取得Int值
        /// </summary>
        public int GetInt(object obj)
        {
            if (obj != null && obj != DBNull.Value)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                return 0;
            }
        }
        public int? GetIntByNull(object obj)
        {
            if (obj != DBNull.Value && obj != null)
                return Convert.ToInt32(obj);
            return null;
        }

        /// <summary>
        /// 取得UInt值
        /// </summary>
        public uint GetUInt(object obj)
        {
            if (obj != DBNull.Value && obj != null)
            {
                return Convert.ToUInt32(obj);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 取得UInt64值
        /// </summary>
        public ulong GetULong(object obj)
        {
            if (obj != DBNull.Value && obj != null)
            {
                return Convert.ToUInt64(obj);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 取得byte值
        /// </summary>
        public Byte GetByte(object obj)
        {
            if (obj != DBNull.Value && obj != null)
            {
                return Convert.ToByte(obj);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 取得sbyte值
        /// </summary>
        public sbyte GetSByte(object obj)
        {
            if (obj != DBNull.Value && obj != null)
            {
                return Convert.ToSByte(obj);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获得Long值
        /// </summary>
        public long GetLong(object obj)
        {
            if (obj != DBNull.Value && obj != null)
            {
                return Convert.ToInt64(obj);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 取得Decimal值
        /// </summary>
        public decimal GetDecimal(object obj)
        {
            if (obj != DBNull.Value && obj != null)
            {
                return Convert.ToDecimal(obj);
            }
            else
            {
                return 0;
            }
        }

        public decimal? GetDecimalByNull(object obj)
        {
            if (obj != DBNull.Value && obj != null)
            {
                decimal tmp;
                if (decimal.TryParse(obj.ToString(), out tmp))
                    return tmp;
            }
            return null;
        }
        /// <summary>
        /// 取得float值
        /// </summary>
        public float GetFloat(object obj)
        {
            if (obj != DBNull.Value && obj != null)
            {
                return Convert.ToSingle(obj);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 取得double值
        /// </summary>
        public double GetDouble(object obj)
        {
            if (obj != DBNull.Value && obj != null)
            {
                return Convert.ToDouble(obj);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 取得Guid值
        /// </summary>
        public Guid GetGuid(object obj)
        {
            if (obj != DBNull.Value && obj != null)
            {
                return new Guid(obj.ToString());
            }
            else
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// 取得DateTime值
        /// </summary>
        public DateTime GetDateTime(object obj)
        {
            if (obj != DBNull.Value && obj != null)
            {
                DateTime tmp;
                if (DateTime.TryParse(obj.ToString(), out tmp))
                {
                    return tmp;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
            else
            {
                return DateTime.MinValue;
            }
        }
        public DateTime? GetDateTimeByNull(object obj)
        {
            if (obj != DBNull.Value && obj != null)
            {
                DateTime tmp;
                if (DateTime.TryParse(obj.ToString(), out tmp))
                    return tmp;
            }
            return null;
        }

        /// <summary>
        /// 取得bool值
        /// </summary>
        public bool GetBool(object obj)
        {
            if (obj != DBNull.Value && obj != null)
            {
                return Convert.ToBoolean(obj);
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 取得byte[]
        /// </summary>
        public Byte[] GetBinary(object obj)
        {
            if (obj != DBNull.Value && obj != null)
            {
                return (Byte[])obj;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 取得string值
        /// </summary>
        public string GetString(object obj)
        {
            if (obj != DBNull.Value && obj != null)
            {
                return obj.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        public string GetSearchWord(string keyword, string word)
        {
            string strWordSql = string.Empty;
            keyword = ReSpace(keyword).Trim();
            Regex r = new Regex(" ");
            string[] strarry = r.Split(keyword);
            foreach (string item in strarry)
                strWordSql += string.Format("{1} like '%{0}%' and ", item, word);
            strWordSql = strWordSql.Trim().TrimEnd(new char[] { 'a', 'n', 'd' }).Trim();
            strWordSql = string.Format("({0})", strWordSql);
            return strWordSql;
        }
        public string ReSpace(string Re)
        {
            Re = Re.Replace("　", " ");
            try { Re = Regex.Replace(Re, "\\s{2,}", " "); }
            catch { }
            return Re;
        }
    }
}

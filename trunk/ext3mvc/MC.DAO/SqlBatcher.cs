using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace MC.DAO
{
    /// <summary>
    /// 为 Sql Server 提供批量处理操作。
    /// </summary>
    public class SqlBatcher
    {
        private MethodInfo m_AddToBatch;
        private MethodInfo m_ClearBatch;
        private MethodInfo m_InitializeBatching;
        private MethodInfo m_ExecuteBatch;
        private SqlDataAdapter m_Adapter;
        private bool _Started;

        /// <summary>
        /// 构造一个新的 SqlBatcher。
        /// </summary>
        public SqlBatcher()
        {
            Type type = typeof(SqlDataAdapter);
            m_AddToBatch = type.GetMethod("AddToBatch", BindingFlags.NonPublic | BindingFlags.Instance);
            m_ClearBatch = type.GetMethod("ClearBatch", BindingFlags.NonPublic | BindingFlags.Instance);
            m_InitializeBatching = type.GetMethod("InitializeBatching", BindingFlags.NonPublic | BindingFlags.Instance);
            m_ExecuteBatch = type.GetMethod("ExecuteBatch", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        /// <summary>
        /// 获得批处理是否正在批处理状态。
        /// </summary>
        public bool Started
        {
            get { return _Started; }
        }

        /// <summary>
        /// 开始批处理。
        /// </summary>
        /// <param name="connection">连接。</param>
        public void StartBatch(SqlConnection connection, Model.EntityState state)
        {
            if (_Started) return;
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            m_Adapter = new SqlDataAdapter();
            if (state == Model.EntityState.Added)
                m_Adapter.InsertCommand = command;
            else if (state == Model.EntityState.Modified)
                m_Adapter.UpdateCommand = command;
            else if (state == Model.EntityState.Deleted)
                m_Adapter.DeleteCommand = command;
            m_InitializeBatching.Invoke(m_Adapter, null);
            _Started = true;
        }

        /// <summary>
        /// 添加批命令。
        /// </summary>
        /// <param name="command">命令</param>
        public void AddToBatch(IDbCommand command)
        {
            if (!_Started) throw new InvalidOperationException();
            m_AddToBatch.Invoke(m_Adapter, new object[1] { command });
        }

        /// <summary>
        /// 执行批处理。
        /// </summary>
        /// <returns>影响的数据行数。</returns>
        public int ExecuteBatch()
        {
            if (!_Started) throw new InvalidOperationException();
            return (int)m_ExecuteBatch.Invoke(m_Adapter, null);
        }

        /// <summary>
        /// 结束批处理。
        /// </summary>
        public void EndBatch()
        {
            if (_Started)
            {
                ClearBatch();
                m_Adapter.Dispose();
                m_Adapter = null;
                _Started = false;
            }
        }

        /// <summary>
        /// 清空保存的批命令。
        /// </summary>
        public void ClearBatch()
        {
            if (!_Started) throw new InvalidOperationException();
            m_ClearBatch.Invoke(m_Adapter, null);
        }
    }
}

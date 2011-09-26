//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2010 hi-p.cn 版权所有
// 创建描述: 自动创建于 2010-5-25 14:36:05
//
// 功能描述: 
//
// 修改标识: 
// 修改描述: 
//------------------------------------------------------------------------------

using System;

namespace Models
{
	/// <summary>
	/// 实体类 sys_Permission，
	/// </summary>
	[Serializable]
	public class sys_Permission : ICloneable
	{
		public sys_Permission()
		{ }

		/// <summary>
		/// 构造函数 sys_Permission
		/// </summary>
		/// <param name="iD">角色操作权限ID</param>
		/// <param name="operationID">功能操作ID</param>
		/// <param name="roleID">角色ID</param>
		public sys_Permission(int iD, int operationID, int roleID)
		{
			_iD = iD;
			_operationID = operationID;
			_roleID = roleID;
		}

		#region 实体属性

		private int _iD;
		private int _operationID;
		private int _roleID;

		/// <summary>
		/// 角色操作权限ID
		/// </summary>
		public int ID
		{
			set { _iD = value; }
			get { return _iD; }
		}

		/// <summary>
		/// 功能操作ID
		/// </summary>
		public int OperationID
		{
			set { _operationID = value; }
			get { return _operationID; }
		}

		/// <summary>
		/// 角色ID
		/// </summary>
		public int RoleID
		{
			set { _roleID = value; }
			get { return _roleID; }
		}

		#endregion

		#region ICloneable 成员

		public object Clone()
		{
			return this.MemberwiseClone();
		}

		#endregion
	}
}

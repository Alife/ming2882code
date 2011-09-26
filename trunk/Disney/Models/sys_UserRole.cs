//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2010 hi-p.cn 版权所有
// 创建描述: 自动创建于 2010-5-25 14:36:22
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
	/// 实体类 sys_UserRole，
	/// </summary>
	[Serializable]
	public class sys_UserRole : ICloneable
	{
		public sys_UserRole()
		{ }

		/// <summary>
		/// 构造函数 sys_UserRole
		/// </summary>
		/// <param name="iD">用户角色ID</param>
		/// <param name="roleID">角色ID</param>
		/// <param name="userID">UserID</param>
		public sys_UserRole(int iD, int roleID, int userID)
		{
			_iD = iD;
			_roleID = roleID;
			_userID = userID;
		}

		#region 实体属性

		private int _iD;
		private int _roleID;
		private int _userID;

		/// <summary>
		/// 用户角色ID
		/// </summary>
		public int ID
		{
			set { _iD = value; }
			get { return _iD; }
		}

		/// <summary>
		/// 角色ID
		/// </summary>
		public int RoleID
		{
			set { _roleID = value; }
			get { return _roleID; }
		}

		/// <summary>
		/// UserID
		/// </summary>
		public int UserID
		{
			set { _userID = value; }
			get { return _userID; }
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

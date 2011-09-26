//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2010 hi-p.cn 版权所有
// 创建描述: 自动创建于 2010-5-25 14:36:12
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
	/// 实体类 sys_PermissionField，
	/// </summary>
	[Serializable]
	public class sys_PermissionField : ICloneable
	{
		public sys_PermissionField()
		{ }

		/// <summary>
		/// 构造函数 sys_PermissionField
		/// </summary>
		/// <param name="iD">角色权限字段ID</param>
		/// <param name="fieldID">功能权限可控字段ID</param>
		/// <param name="permissionID">角色操作权限ID</param>
		public sys_PermissionField(int iD, int fieldID, int permissionID)
		{
			_iD = iD;
			_fieldID = fieldID;
			_permissionID = permissionID;
		}

		#region 实体属性

		private int _iD;
		private int _fieldID;
		private int _permissionID;

		/// <summary>
		/// 角色权限字段ID
		/// </summary>
		public int ID
		{
			set { _iD = value; }
			get { return _iD; }
		}

		/// <summary>
		/// 功能权限可控字段ID
		/// </summary>
		public int FieldID
		{
			set { _fieldID = value; }
			get { return _fieldID; }
		}

		/// <summary>
		/// 角色操作权限ID
		/// </summary>
		public int PermissionID
		{
			set { _permissionID = value; }
			get { return _permissionID; }
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

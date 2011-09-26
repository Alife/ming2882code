//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2010 hi-p.cn 版权所有
// 创建描述: 自动创建于 2010-5-25 14:35:51
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
	/// 实体类 sys_Operation，
	/// </summary>
	[Serializable]
	public class sys_Operation : ICloneable
	{
		public sys_Operation()
		{ }

		#region 实体属性

        private int _iD;
        private string _code;
		private string _operation;
		private int _applicationID;
        private int _orderID;
        private string _icon;

		/// <summary>
		/// 功能操作ID
		/// </summary>
		public int ID
		{
			set { _iD = value; }
			get { return _iD; }
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }

		/// <summary>
		/// 操作方式
		/// </summary>
		public string Operation
		{
			set { _operation = value; }
			get { return _operation; }
		}

		/// <summary>
		/// 功能模块ID
		/// </summary>
		public int ApplicationID
		{
			set { _applicationID = value; }
			get { return _applicationID; }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderID
        {
            set { _orderID = value; }
            get { return _orderID; }
        }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon
        {
            set { _icon = value; }
            get { return _icon; }
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

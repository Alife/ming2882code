//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2010 hi-p.cn 版权所有
// 创建描述: 自动创建于 2010-5-25 14:35:57
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
	/// 实体类 sys_Page，
	/// </summary>
	[Serializable]
	public class sys_Page : ICloneable
	{
		public sys_Page()
		{ }

		#region 实体属性

		private int _iD;
		private string _name;
		private string _code;
		private string _content;
		private int _parentID;
		private string _url;
		private int _orderID;
        private int _path;
        private bool _ishide;

		/// <summary>
		/// ID
		/// </summary>
		public int ID
		{
			set { _iD = value; }
			get { return _iD; }
		}

		/// <summary>
		/// Name
		/// </summary>
		public string Name
		{
			set { _name = value; }
			get { return _name; }
		}

		/// <summary>
		/// Code
		/// </summary>
		public string Code
		{
			set { _code = value; }
			get { return _code; }
		}

		/// <summary>
		/// Content
		/// </summary>
		public string Content
		{
			set { _content = value; }
			get { return _content; }
		}

		/// <summary>
		/// ParentID
		/// </summary>
		public int ParentID
		{
			set { _parentID = value; }
			get { return _parentID; }
		}

		/// <summary>
		/// Url
		/// </summary>
		public string Url
		{
			set { _url = value; }
			get { return _url; }
		}

		/// <summary>
		/// OrderID
		/// </summary>
		public int OrderID
		{
			set { _orderID = value; }
			get { return _orderID; }
		}

        public int Path
        {
            set { _path = value; }
            get { return _path; }
        }
        public bool IsHide
        {
            set { _ishide = value; }
            get { return _ishide; }
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

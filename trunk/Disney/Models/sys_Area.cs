//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2010 hi-p.cn 版权所有
// 创建描述: 自动创建于 2010-5-25 14:34:06
//
// 功能描述: 
//
// 修改标识: 
// 修改描述: 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Models
{
	/// <summary>
	/// 实体类 d_Area，
	/// </summary>
	[Serializable]
	public class sys_Area : ICloneable
	{
		public sys_Area()
		{ }

		#region 实体属性

		private int _iD;
		private string _name;
        private string _code;
        private string _pinyin;
        private int _lft;
        private int _rgt;
        private int _parentid;
        private int _path;
        private bool _isleaf;
        private int _children;

		public int ID
		{
			set { _iD = value; }
			get { return _iD; }
		}
		public string Name
		{
			set { _name = value; }
			get { return _name; }
		}
		public string Code
		{
			set { _code = value; }
            get { return _code; }
		}
        public string Pinyin
		{
			set { _pinyin = value; }
            get { return _pinyin; }
		}
        public int Lft
		{
            set { _lft = value; }
            get { return _lft; }
        }
        public int Rgt
        {
            set { _rgt = value; }
            get { return _rgt; }
        }
        public int ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 第几级
        /// </summary>
        public int Path
        {
            get { return _path; }
            set { _path = value; }
        }
        /// <summary>
        /// 是否最后一级
        /// </summary>
        public bool IsLeaf
        {
            get { return _isleaf; }
            set { _isleaf = value; }
        }
        public int Children
        {
            get { return _children; }
            set { _children = value; }
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

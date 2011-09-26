using System;
using System.Collections.Generic;

namespace Models
{
	[Serializable]
	public class sys_Application
	{
		public sys_Application()
		{ }

		#region 实体属性

		private int _iD;
		private string _code;
        private string _name;
        private string _url;
		private string _description;
        private bool _isHidden;
        private string _icon;
		private int _parentID;
        private int _lft;
        private int _rgt;

		/// <summary>
		/// ID
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
		/// 名称
		/// </summary>
		public string Name
		{
			set { _name = value; }
			get { return _name; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }

		/// <summary>
		/// 父类ID
		/// </summary>
		public int ParentID
		{
			set { _parentID = value; }
			get { return _parentID; }
		}

		/// <summary>
		/// 介绍
		/// </summary>
		public string Description
		{
			set { _description = value; }
			get { return _description; }
		}

		/// <summary>
        /// 左值
		/// </summary>
        public int Lft
		{
			set { _lft = value; }
            get { return _lft; }
		}

		/// <summary>
        /// 右值
		/// </summary>
        public int Rgt
		{
			set { _rgt = value; }
            get { return _rgt; }
		}
        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHidden
        {
            set { _isHidden = value; }
            get { return _isHidden; }
        }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon
        {
            set { _icon = value; }
            get { return _icon; }
        }
        private int _path;
        private bool _isleaf;
        private int _children;
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
	}
}

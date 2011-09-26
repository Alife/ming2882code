//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2010 hi-p.cn 版权所有
// 创建描述: 自动创建于 2010-5-25 14:32:28
//
// 功能描述: 
//
// 修改标识: 
// 修改描述: 
//------------------------------------------------------------------------------

using System;
using System.Collections;

namespace Models
{
	/// <summary>
	/// 实体类 article，
	/// </summary>
	[Serializable]
	public class Article : ICloneable
	{
		public Article()
		{ }

		#region 实体属性

		private int _iD;
		private int _categoryID;
		private int? _userID;
		private string _title;
		private string _content;
		private DateTime _createTime;
		private string _tags;
		private string _source;
		private int _hits;
		private int _writerID;
		private string _writer;
		private string _titleStyle;
		private string _url;
		private int _elite;
		private int _top;
		private bool _isComment;

		/// <summary>
		/// ID
		/// </summary>
		public int ID
		{
			set { _iD = value; }
			get { return _iD; }
		}

		/// <summary>
		/// CategoryID
		/// </summary>
		public int CategoryID
		{
			set { _categoryID = value; }
			get { return _categoryID; }
		}

		/// <summary>
		/// UserID
		/// </summary>
		public int? UserID
		{
			set { _userID = value; }
			get { return _userID; }
		}

		/// <summary>
		/// 标题
		/// </summary>
		public string Title
		{
			set { _title = value; }
			get { return _title; }
		}

		/// <summary>
		/// 内容
		/// </summary>
		public string Content
		{
			set { _content = value; }
			get { return _content; }
		}

		/// <summary>
		/// 发布时间
		/// </summary>
		public DateTime CreateTime
		{
			set { _createTime = value; }
			get { return _createTime; }
		}

		/// <summary>
		/// Tags
		/// </summary>
		public string Tags
		{
			set { _tags = value; }
			get { return _tags; }
		}

		/// <summary>
		/// 来源
		/// </summary>
		public string Source
		{
			set { _source = value; }
			get { return _source; }
		}

		/// <summary>
		/// 点击率
		/// </summary>
		public int Hits
		{
			set { _hits = value; }
			get { return _hits; }
		}

		/// <summary>
		/// 作者ID
		/// </summary>
		public int WriterID
		{
			set { _writerID = value; }
			get { return _writerID; }
		}

		/// <summary>
		/// 作者
		/// </summary>
		public string Writer
		{
			set { _writer = value; }
			get { return _writer; }
		}

		/// <summary>
		/// 标题样式:数组:color|size|i|b|u
		/// </summary>
		public string TitleStyle
		{
			set { _titleStyle = value; }
			get { return _titleStyle; }
		}

		/// <summary>
		/// 外连
		/// </summary>
		public string Url
		{
			set { _url = value; }
			get { return _url; }
		}

		/// <summary>
		/// 精华
		/// </summary>
		public int Elite
		{
			set { _elite = value; }
			get { return _elite; }
		}

		/// <summary>
		/// 置顶
		/// </summary>
		public int Top
		{
			set { _top = value; }
			get { return _top; }
		}

		/// <summary>
		/// 是否评论
		/// </summary>
		public bool IsComment
		{
			set { _isComment = value; }
			get { return _isComment; }
		}

		#endregion

		#region ICloneable 成员

		public object Clone()
		{
			return this.MemberwiseClone();
		}

		#endregion
    }
    public class ArticleList : CollectionBase
    {
        private int _recordCount;

        public int Add(Article value)
        {
            return base.List.Add(value);
        }

        public Article this[int index]
        {
            get { return (Article)base.List[index]; }
            set { base.List[index] = value; }
        }

        public int RecordNumber
        {
            get { return this._recordCount; }
            set { this._recordCount = value; }
        }
    }
}

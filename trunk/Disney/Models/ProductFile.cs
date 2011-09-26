using System;
using System.Collections;

namespace Models
{
	[Serializable]
	public class ProductFile : ICloneable
	{
        public ProductFile()
		{ }

		#region 实体属性

		private int _iD;
		private int _productID;
		private string _filePath;
		private string _fileType;
		private string _fileName;
		private decimal _fileSize;
		private bool _isTop;

		/// <summary>
		/// ID
		/// </summary>
		public int ID
		{
			set { _iD = value; }
			get { return _iD; }
		}

		/// <summary>
        /// ProductID
		/// </summary>
		public int ProductID
		{
            set { _productID = value; }
            get { return _productID; }
		}

		/// <summary>
		/// FilePath
		/// </summary>
		public string FilePath
		{
			set { _filePath = value; }
			get { return _filePath; }
		}

		/// <summary>
		/// FileType
		/// </summary>
		public string FileType
		{
			set { _fileType = value; }
			get { return _fileType; }
		}

		/// <summary>
		/// FileName
		/// </summary>
		public string FileName
		{
			set { _fileName = value; }
			get { return _fileName; }
		}

		/// <summary>
		/// FileSize
		/// </summary>
		public decimal FileSize
		{
			set { _fileSize = value; }
			get { return _fileSize; }
		}

		/// <summary>
		/// IsTop
		/// </summary>
		public bool IsTop
		{
			set { _isTop = value; }
			get { return _isTop; }
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

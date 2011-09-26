using System;
using System.Collections;

namespace Models
{
	[Serializable]
    public class web_Photo : ICloneable
	{
		public web_Photo()
		{ }

		#region 实体属性
        public int ID { get; set; }
        public int PhotoTypeID { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public int FileSize { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
		#endregion

		#region ICloneable 成员

		public object Clone()
		{
			return this.MemberwiseClone();
		}

		#endregion
    }
    public class web_PhotoList : CollectionBase
    {
        private int _recordCount;

        public int Add(web_Photo value)
        {
            return base.List.Add(value);
        }

        public web_Photo this[int index]
        {
            get { return (web_Photo)base.List[index]; }
            set { base.List[index] = value; }
        }

        public int RecordNumber
        {
            get { return this._recordCount; }
            set { this._recordCount = value; }
        }
    }
}

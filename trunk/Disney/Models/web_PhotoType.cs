using System;
using System.Collections;

namespace Models
{
	[Serializable]
    public class web_PhotoType : ICloneable
	{
		public web_PhotoType()
		{ }

		#region 实体属性
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int ParentID { get; set; }
        public int OrderID { get; set; }
		#endregion

		#region ICloneable 成员

		public object Clone()
		{
			return this.MemberwiseClone();
		}

		#endregion
    }
    public class web_PhotoTypeList : CollectionBase
    {
        private int _recordCount;

        public int Add(web_PhotoType value)
        {
            return base.List.Add(value);
        }

        public web_PhotoType this[int index]
        {
            get { return (web_PhotoType)base.List[index]; }
            set { base.List[index] = value; }
        }

        public int RecordNumber
        {
            get { return this._recordCount; }
            set { this._recordCount = value; }
        }
    }
}

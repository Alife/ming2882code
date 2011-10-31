using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace MC.Model
{
    public class QueryInfo
    {
        IDictionary _Orderby = new Hashtable();
        string _MappingName = null;
        IDictionary _Parameters = null;

        public QueryInfo() { }
        public QueryInfo(string sMappingName)
        {
            MappingName = sMappingName;
        }
        /// <summary>
        /// mapping中的ID
        /// </summary>
        public string XmlID { get; set; }
        /// <summary>
        /// mapping中分页的ID
        /// </summary>
        public string XmlPageCountID { get; set; }
        /// <summary>
        /// mapping中采用#value#
        /// </summary>
        public object MapQueryValue { get; set; }
        /// <summary>
        /// hashtable的key为的orderby字段,value=null为正序，value=desc为反序
        /// </summary>
        public IDictionary Orderby
        {
            set { _Orderby = value; }
            get
            {
                if (_Orderby == null)
                    _Orderby = new Hashtable();
                return _Orderby;
            }
        }
        /// <summary>
        /// 操作mapping对应的表名
        /// </summary>
        public string MappingName
        {
            set { _MappingName = value; }
            get { return _MappingName; }
        }

        /// <summary>
        /// 参数
        /// </summary>
        public IDictionary Parameters
        {
            set { _Parameters = value; }
            get
            {
                if (_Parameters == null)
                    _Parameters = new Hashtable();
                return _Parameters;
            }
        }

        public void Clear()
        {
            _Parameters = null;
        }
    }
}

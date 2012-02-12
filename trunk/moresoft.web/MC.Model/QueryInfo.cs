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
        /// mapping�е�ID
        /// </summary>
        public string XmlID { get; set; }
        /// <summary>
        /// mapping�з�ҳ��ID
        /// </summary>
        public string XmlPageCountID { get; set; }
        /// <summary>
        /// mapping�в���#value#
        /// </summary>
        public object MapQueryValue { get; set; }
        /// <summary>
        /// hashtable��keyΪ��orderby�ֶ�,value=nullΪ����value=descΪ����
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
        /// ����mapping��Ӧ�ı���
        /// </summary>
        public string MappingName
        {
            set { _MappingName = value; }
            get { return _MappingName; }
        }

        /// <summary>
        /// ����
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

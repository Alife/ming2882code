using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class PageObject
    {
        private int _pageCount;
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }
        private object _obj;
        public object PageObj
        {
            get { return _obj; }
            set { _obj = value; }
        }
    }
}

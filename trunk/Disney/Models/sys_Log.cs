using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class sys_Log
    {
        #region 实体属性
        public int ID { get; set; }

        /// <summary>
        /// UserID
        /// </summary>
        public int UserID { get; set; }
        public string TrueName { get; set; }

        /// <summary>
        /// 操作IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime LogTime { get; set; }

        /// <summary>
        /// 操作ID
        /// </summary>
        public int OpID { get; set; }

        /// <summary>
        /// 操作对象ID
        /// </summary>
        public string ObjCode { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string Content { get; set; }
        #endregion
    }
    public class sys_LogList
    {
        public sys_LogList()
        {
            data = new List<sys_Log>();
        }
        public List<sys_Log> data
        {
            get;
            set;
        }

        public int records
        {
            get;
            set;
        }
    }
}

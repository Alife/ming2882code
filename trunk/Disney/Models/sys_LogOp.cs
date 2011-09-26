using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class sys_LogOp
    {
        #region 实体属性
        public int ID { get; set; }

        /// <summary>
        /// 日志分类ID
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// 操作名称
        /// </summary>
        public string OpName { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string OpCode { get; set; }

        /// <summary>
        /// 日志格式化内容：以string.format()来完成
        /// </summary>
        public string FormatLog { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderID { get; set; }

		#endregion
    }
}

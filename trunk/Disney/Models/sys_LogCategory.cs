using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class sys_LogCategory
    {
        #region 实体属性

        public int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderID { get; set; }

		#endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class d_Department
    {
        #region 实体属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父类ID
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// 左值
        /// </summary>
        public int Lft { get; set; }

        /// <summary>
        /// 右值
        /// </summary>
        public int Rgt { get; set; }
        /// <summary>
        /// 第几级
        /// </summary>
        public int Path { get; set; }
        /// <summary>
        /// 是否最后一级
        /// </summary>
        public bool IsLeaf { get; set; }
        public int Children { get; set; }
        #endregion
    }
}

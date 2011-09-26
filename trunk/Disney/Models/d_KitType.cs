using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class d_KitType
    {
        #region 实体属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// 封面尺寸ID
        /// </summary>
        public int CoverID { get; set; }

        /// <summary>
        /// 内页尺寸ID
        /// </summary>
        public int InsideID { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int PageNum { get; set; }

        /// <summary>
        /// P数
        /// </summary>
        public int PNum { get; set; }

        /// <summary>
        /// 服装套数
        /// </summary>
        public int CostumeNum { get; set; }

        /// <summary>
        /// 是否含学士服
        /// </summary>
        public bool IsGown { get; set; }

        /// <summary>
        /// 拍摄张数
        /// </summary>
        public int ShootNum { get; set; }
        #endregion
    }
}

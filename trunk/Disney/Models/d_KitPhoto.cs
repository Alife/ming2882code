using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class d_KitPhoto
    {
        #region 实体属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 美工ID
        /// </summary>
        public int ArterID { get; set; }

        /// <summary>
        /// 工作单ID
        /// </summary>
        public int KitWorkID { get; set; }

        /// <summary>
        /// 图类型ID
        /// </summary>
        public int KitPhotoTypeID { get; set; }

        /// <summary>
        /// 总人数量
        /// </summary>
        public int PeopleNum { get; set; }

        /// <summary>
        /// 图片数量
        /// </summary>
        public int PhotoNum { get; set; }

        /// <summary>
        /// 老师数量
        /// </summary>
        public int TeacherNum { get; set; }

        /// <summary>
        /// 美工价格
        /// </summary>
        public decimal ArtistPrice { get; set; }

        /// <summary>
        /// 美工价格
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal Amt { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class d_Kit
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
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 提交用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 状态,1.Stock采购单，2.Pass制成中，3.End完成
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomID { get; set; }

        /// <summary>
        /// 交货时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 摄影师ID
        /// </summary>
        public int? CameraManID { get; set; }

        /// <summary>
        /// 摄影交档时间
        /// </summary>
        public DateTime? CameraTime { get; set; }

        /// <summary>
        /// 套系类型ID
        /// </summary>
        public int KitTypeID { get; set; }

        /// <summary>
        /// 同学录套图ID
        /// </summary>
        public int ClassTypeID { get; set; }

        /// <summary>
        /// 封面材料ID
        /// </summary>
        public int InsideMaterialID { get; set; }

        /// <summary>
        /// 解析度
        /// </summary>
        public string Resolution { get; set; }

        /// <summary>
        /// 套图模板ID
        /// </summary>
        public int? TemplateID { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        #endregion
    }
}

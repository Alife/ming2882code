using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Models.Enums
{
    /// <summary>
    /// 机构级别 全局、公司、部门、 拥有(用户)、自己
    /// </summary>
    public enum Confine
    {
        /// <summary>
        ///全局
        /// </summary>
        [Description("全局")]
        Global = 1,
        /// <summary>
        ///公司
        /// </summary>
        [Description("公司")]
        Company = 2,
        /// <summary>
        ///部门
        /// </summary>
        [Description("部门")]
        Dept = 3,
        /// <summary>
        ///拥有
        /// </summary>
        [Description("拥有")]
        Own = 4,
        /// <summary>
        ///自己
        /// </summary>
        [Description("自己")]
        Self = 5
    }
    /// <summary>
    /// 资源类型:如美工图片价格ArtistPrice,校图价格ProofsPrice
    /// </summary>
    public enum ResourceType
    {
        /// <summary>
        /// 机构部门
        /// </summary>
        [Description("机构部门")]
        Dept = 1,
        /// <summary>
        /// 美工图片价格
        /// </summary>
        [Description("美工图片价格")]
        ArtistPrice = 2,
        /// <summary>
        /// 校图
        /// </summary>
        [Description("校图")]
        Proofs = 3,
        /// <summary>
        /// 校图价格
        /// </summary>
        [Description("校图价格")]
        ProofsPrice = 4,
        /// <summary>
        /// 单据查询
        /// </summary>
        [Description("单据查询")]
        OrderQuery = 5,
        /// <summary>
        /// 工作单查询
        /// </summary>
        [Description("工作单查询")]
        WorkOrderQuery = 6,
        /// <summary>
        /// 增补单据查询
        /// </summary>
        [Description("增补单据查询")]
        LeakOrderQuery = 7,
        /// <summary>
        /// 返工单据查询
        /// </summary>
        [Description("返工单据查询")]
        ReturnOrderQuery = 8,
        /// <summary>
        /// 制成单负责人查询
        /// </summary>
        [Description("制成单负责人查询")]
        CrankOrderManQuery = 9
    }
}

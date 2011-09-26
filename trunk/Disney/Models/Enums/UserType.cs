using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Models.Enums
{
    public enum UserType
    {
        /// <summary>
        ///超级管理员
        /// </summary>
        [Description("超级管理员")]
        Admin = 1,
        /// <summary>
        /// 美工
        /// </summary>
        [Description("美工")]
        Artist = 2,
        /// <summary>
        /// 摄影师
        /// </summary>
        [Description("摄影师")]
        Cameraman = 3,
        /// <summary>
        /// 普通会员
        /// </summary>
        [Description("普通会员")]
        Member = 4,
        /// <summary>
        /// Vip会员
        /// </summary>
        [Description("VIP会员")]
        VipMember = 5,
        /// <summary>
        /// 客户
        /// </summary>
        [Description("客户")]
        Custom = 6,
        /// <summary>
        /// 员工
        /// </summary>
        [Description("员工")]
        Staff = 7,
        /// <summary>
        /// 应聘人员
        /// </summary>
        [Description("应聘人员")]
        JobMember = 8
    }
}

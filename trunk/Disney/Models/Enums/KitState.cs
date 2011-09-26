using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Models.Enums
{
    /// <summary>
    /// 状态
    /// </summary>
    public enum KitState
    {
        /// <summary>
        ///采购单
        /// </summary>
        [Description("采购单")]
        Stock = 1,
        /// <summary>
        ///制程中
        /// </summary>
        [Description("制程中")]
        Process = 2,
        /// <summary>
        ///补件
        /// </summary>
        [Description("补件")]
        Leak = 3,
        /// <summary>
        ///完成
        /// </summary>
        [Description("完成")]
        End = 4
    }
    public enum KitPhotoState
    {
        /// <summary>
        ///制程
        /// </summary>
        [Description("制程")]
        Process = 1,
        /// <summary>
        ///完成
        /// </summary>
        [Description("完成")]
        End = 2,
        /// <summary>
        ///已传
        /// </summary>
        [Description("已传")]
        Uploaded = 3,
        /// <summary>
        ///结算
        /// </summary>
        [Description("结算")]
        MonthEnd = 4
    }
    /// <summary>
    /// 校图状态1未校图，2结束校图,3已处理,4已校图
    /// </summary>
    public enum KitProofState
    {
        /// <summary>
        /// 校图中
        /// </summary>
        [Description("校图中")]
        UnProof = 1,
        /// <summary>
        ///园所校图完成
        /// </summary>
        [Description("园所校图完成")]
        Finish = 2,
        /// <summary>
        ///已处理
        /// </summary>
        [Description("已处理")]
        Deal = 3,
        /// <summary>
        ///已校图
        /// </summary>
        [Description("已校图")]
        Proof = 4,
        /// <summary>
        ///不校图
        /// </summary>
        [Description("不校图")]
        NoProof = 5,
        /// <summary>
        ///美工修图
        /// </summary>
        [Description("美工修图")]
        StartDeal = 6
    }
    /// <summary>
    /// 件类型:1Normal正常件，2Leak补件，3有价返工件
    /// </summary>
    public enum KitTypeState
    {
        /// <summary>
        /// 正常件
        /// </summary>
        [Description("正常件")]
        Normal = 1,
        /// <summary>
        ///补件
        /// </summary>
        [Description("补件")]
        Leak = 2,
        /// <summary>
        ///有价返工件
        /// </summary>
        [Description("有价返工件")]
        Return = 3,
        /// <summary>
        ///分班件
        /// </summary>
        [Description("分班件")]
        Detach = 4,
        /// <summary>
        ///美工重计件
        /// </summary>
        [Description("美工重计件")]
        Recompute = 5
    }
    /// <summary>
    /// 解决状态：1待解决，2已处理，3已解决
    /// </summary>
    public enum KitQuestionState
    {
        /// <summary>
        /// 待解决
        /// </summary>
        [Description("待解决")]
        UnSolve = 1,
        /// <summary>
        ///已处理
        /// </summary>
        [Description("已处理")]
        Deal = 2,
        /// <summary>
        ///已解决
        /// </summary>
        [Description("已解决")]
        Solve = 3
    }
    /// <summary>
    /// 问题类型:1仅本张，2男生同服装，3女生同服装，4同服装
    /// </summary>
    public enum KitQuestionType
    {
        /// <summary>
        /// 仅本张
        /// </summary>
        [Description("仅本张")]
        Only = 1,
        /// <summary>
        ///男生同服装
        /// </summary>
        [Description("男生同服装")]
        Boy = 2,
        /// <summary>
        ///女生同服装
        /// </summary>
        [Description("女生同服装")]
        Gril = 3,
        /// <summary>
        ///同服装
        /// </summary>
        [Description("同服装")]
        All = 3
    }
    /// <summary>
    /// 结算状态：1未结，2已结,3美工已结
    /// </summary>
    public enum Balance
    {
        /// <summary>
        /// 未结
        /// </summary>
        [Description("未结")]
        Normal = 1,
        /// <summary>
        ///已结
        /// </summary>
        [Description("已结")]
        Bal = 2,
        /// <summary>
        ///美工已结
        /// </summary>
        [Description("美工已结")]
        ArtBal = 3
    }
    public enum KitOrder
    {
        None = 0,
        Class = 1
    }
}

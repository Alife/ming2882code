using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class d_KitWork
    {
        #region 实体属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 工作单名称
        /// </summary>
        public string WorkName { get; set; }
        /// <summary>
        /// 档ID
        /// </summary>
        public int KitID { get; set; }

        /// <summary>
        /// 月统计结算ID
        /// </summary>
        public int? TotolMonthID { get; set; }

        /// <summary>
        /// 人數
        /// </summary>
        public int PeopleNum { get; set; }

        /// <summary>
        /// 送件日期
        /// </summary>
        public DateTime? SendTime { get; set; }

        /// <summary>
        /// 预计完成日期
        /// </summary>
        public DateTime? PlanTime { get; set; }

        /// <summary>
        /// 开始制程日期
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 完成日期
        /// </summary>
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// 美工人员ID
        /// </summary>
        public int ArterID { get; set; }

        /// <summary>
        /// 状态,1.Pass制成中，2.Pass制成通过,3.NoPass制成不通过，4.End完成，5.MonthEnd月结
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 校图状态1校图中,2完成校图，3已处理,4已校图，5不校图
        /// </summary>
        public int? ProofState { get; set; }
        /// <summary>
        /// 校图开始时间
        /// </summary>
        public DateTime? ProofBeginTime { get; set; }
        /// <summary>
        /// 校图结束时间
        /// </summary>
        public DateTime? ProofEndTime { get; set; }
        /// <summary>
        /// 刷新校图时间
        /// </summary>
        public DateTime? ProofTime { get; set; }
        /// <summary>
        /// 是否合作完成
        /// </summary>
        public bool IsCooperate { get; set; }

        /// <summary>
        /// 上傳文件夾
        /// </summary>
        public string UploadFile { get; set; }

        /// <summary>
        /// 件类型:1Normal正常件，2Leak补件，3有价返工件
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        #endregion
    }
}

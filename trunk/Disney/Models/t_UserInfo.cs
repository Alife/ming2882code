
using System;
using System.Collections.Generic;

namespace Models
{
    /// <summary>
    /// 实体类 t_UserInfo
    /// </summary>
    [Serializable]
    public class t_UserInfo
    {
        public t_UserInfo()
        { }
        #region 实体属性
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// UserID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 是否结婚
        /// </summary>
        public bool IsMarry { get; set; }

        /// <summary>
        /// 籍贯
        /// </summary>
        public int? BirthPlace { get; set; }

        /// <summary>
        /// 政治面貌
        /// </summary>
        public int? PoliticsStatus { get; set; }

        /// <summary>
        /// 毕业学校
        /// </summary>
        public string College { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public string Speciality { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public int? Education { get; set; }

        /// <summary>
        /// 参加工作时间
        /// </summary>
        public string JobTime { get; set; }

        /// <summary>
        /// 到职时间
        /// </summary>
        public string OnDutyTime { get; set; }

        /// <summary>
        /// 离职时间
        /// </summary>
        public string DimissionTime { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string Duty { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public int? Nation { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IDCard { get; set; }

        /// <summary>
        /// 通信址址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// 是否訂閱
        /// </summary>
        public bool IsEmail { get; set; }

        /// <summary>
        /// 简历路径
        /// </summary>
        public string Resume { get; set; }
        #endregion
    }
    public class t_UserInfoList
    {
        public List<t_UserInfo> data
        {
            get;
            set;
        }

        public int records
        {
            get;
            set;
        }
    }
}

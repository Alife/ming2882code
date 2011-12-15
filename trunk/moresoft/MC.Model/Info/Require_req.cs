using System;
using System.Collections;
using System.Collections.Generic;

namespace MC.Model
{
    /// <summary>
    /// Require_req
    /// </summary>
    [Serializable]
    public partial class Require_req : Entity
    {
        #region method
        /// <summary>
        /// Table Name
        /// </summary>
        public override string GetTableName()
        {
            return "Require_req";
        }
        /// <summary>
        /// Keys
        /// </summary>
        public override string[] GetKeyCols()
        {
            return new string[] { "ID_req" };
        }
        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        public int? ID_req { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string TrueName_req { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Tel_req { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile_req { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        public string Company_req { get; set; }

        /// <summary>
        /// 行业
        /// </summary>
        public string Industry_req { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark_req { get; set; }
        #endregion
    }
}
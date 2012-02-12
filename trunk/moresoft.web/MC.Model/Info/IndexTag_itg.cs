using System;
using System.Collections;
using System.Collections.Generic;

namespace MC.Model
{
    /// <summary>
    /// IndexTag_itg
    /// </summary>
    [Serializable]
    public partial class IndexTag_itg : Entity
    {
        #region method
        /// <summary>
        /// Table Name
        /// </summary>
        public override string GetTableName()
        {
            return "IndexTag_itg";
        }
        /// <summary>
        /// Keys
        /// </summary>
        public override string[] GetKeyCols()
        {
            return new string[] { "ID_itg" };
        }
        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        public int? ID_itg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name_itg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Sort_itg { get; set; }
        #endregion
    }
}
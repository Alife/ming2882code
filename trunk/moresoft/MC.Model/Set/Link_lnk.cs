using System;
using System.Collections;
using System.Collections.Generic;

namespace MC.Model
{
    /// <summary>
    /// Link_lnk
    /// </summary>
    [Serializable]
    public partial class Link_lnk : Entity
    {
        #region method
        /// <summary>
        /// Table Name
        /// </summary>
        public override string GetTableName()
        {
            return "Link_lnk";
        }
        /// <summary>
        /// Keys
        /// </summary>
        public override string[] GetKeyCols()
        {
            return new string[] { "ID_lnk" };
        }
        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        public int? ID_lnk { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name_lnk { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Url_lnk { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Sort_lnk { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsHide_lnk { get; set; }
        #endregion
    }
}
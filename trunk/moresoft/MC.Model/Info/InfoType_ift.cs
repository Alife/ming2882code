using System;
using System.Collections;
using System.Collections.Generic;

namespace MC.Model
{
    /// <summary>
    /// InfoType_ift
    /// </summary>
    [Serializable]
    public partial class InfoType_ift : Entity
    {
        #region method
        /// <summary>
        /// Table Name
        /// </summary>
        public override string GetTableName()
        {
            return "InfoType_ift";
        }
        /// <summary>
        /// Keys
        /// </summary>
        public override string[] GetKeyCols()
        {
            return new string[] { "ID_ift" };
        }
        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        public int? ID_ift { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name_ift { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Code_ift { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Url_ift { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Sort_ift { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Parent_ift { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Path_ift { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsHide_ift { get; set; }
        #endregion
    }
}
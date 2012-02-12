using System;
using System.Collections;
using System.Collections.Generic;

namespace MC.Model
{
    /// <summary>
    /// Setting_set
    /// </summary>
    [Serializable]
    public partial class Setting_set : Entity
    {
        #region method
        /// <summary>
        /// Table Name
        /// </summary>
        public override string GetTableName()
        {
            return "Setting_set";
        }
        /// <summary>
        /// Keys
        /// </summary>
        public override string[] GetKeyCols()
        {
            return new string[] { "ID_set" };
        }
        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        public int? ID_set { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WebName_set { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WebUrl_set { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title_set { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Keywords_set { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Author_set { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ICP_set { get; set; }
        #endregion
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace MC.Model
{
    /// <summary>
    /// Keywords_key
    /// </summary>
    [Serializable]
    public partial class Keywords_key : Entity
    {
        #region method
        /// <summary>
        /// Table Name
        /// </summary>
        public override string GetTableName()
        {
            return "Keywords_key";
        }
        /// <summary>
        /// Keys
        /// </summary>
        public override string[] GetKeyCols()
        {
            return new string[] { "ID_key" };
        }
        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        public int ID_key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name_key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Url_key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Num_key { get; set; }
        #endregion
    }
}
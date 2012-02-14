using System;
using System.Collections;
using System.Collections.Generic;

namespace MC.Model
{
    /// <summary>
    /// Page_pag
    /// </summary>
    [Serializable]
    public partial class Page_pag : Entity
    {
        #region method
        /// <summary>
        /// Table Name
        /// </summary>
        public override string GetTableName()
        {
            return "Page_pag";
        }
        /// <summary>
        /// Keys
        /// </summary>
        public override string[] GetKeyCols()
        {
            return new string[] { "ID_pag" };
        }
        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        public int? ID_pag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name_pag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Code_pag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Content_pag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Sort_pag { get; set; }
        public int? Parent_pag { get; set; }
        public int? Path_pag { get; set; }
        public IList<Page_pag> children { get; set; }
        #endregion
    }
}
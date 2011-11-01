using System;
using System.Collections;
using System.Collections.Generic;

namespace MC.Model
{
    /// <summary>
    /// mc_User
    /// </summary>
    [Serializable]
    public partial class mc_User : Entity
    {
        #region method
        /// <summary>
        /// Table Name
        /// </summary>
        public override string GetTableName()
        {
            return "mc_User";
        }
        /// <summary>
        /// Keys
        /// </summary>
        public override string[] GetKeyCols()
        {
            return new string[] { "ID" };
        }
        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? DeptID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        #endregion
    }
    [Serializable]
    public class mc_UserList : EntityList
    {
    }
}
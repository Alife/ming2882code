﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace MC.Model
{
    /// <summary>
    /// User_usr
    /// </summary>
    [Serializable]
    public partial class User_usr : Entity
    {
        #region method
        /// <summary>
        /// Table Name
        /// </summary>
        public override string GetTableName()
        {
            return "User_usr";
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
        public int? ID_usr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName_usr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password_usr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? LoginNum_usr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email_usr { get; set; }
        #endregion
    }
}
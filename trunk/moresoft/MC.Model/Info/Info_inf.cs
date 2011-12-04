using System;
using System.Collections;
using System.Collections.Generic;

namespace MC.Model
{
    /// <summary>
    /// Info_inf
    /// </summary>
    [Serializable]
    public partial class Info_inf : Entity
    {
        #region method
        /// <summary>
        /// Table Name
        /// </summary>
        public override string GetTableName()
        {
            return "Info_inf";
        }
        /// <summary>
        /// Keys
        /// </summary>
        public override string[] GetKeyCols()
        {
            return new string[] { "ID_inf" };
        }
        #endregion

        #region properties

        /// <summary>
        /// 
        /// </summary>
        public int? ID_inf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? InfoTypeID_inf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title_inf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Content_inf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Hits_inf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TopType_inf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? IndexTagID_inf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime_inf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Author_inf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Keywords_inf { get; set; }
        private string _CreateTime_inf_Str;
        public string CreateTime_inf_Str
        {
            get { _CreateTime_inf_Str = CreateTime_inf.ToString("yyyy-MM-dd hh:mm:ss"); return _CreateTime_inf_Str; }
            set { _CreateTime_inf_Str = value; CreateTime_inf = DateTime.Parse(value); }
        }
        #endregion
    }
}
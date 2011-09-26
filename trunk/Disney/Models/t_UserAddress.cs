using System.Collections.Generic;
using System;

namespace Models
{
    /// <summary>
    /// 实体类 t_UserAddress
    /// </summary>
    [Serializable]
    public class t_UserAddress : ICloneable
    {
        public t_UserAddress()
        { }

        #region 实体属性

        private int _iD;
        private int _userID;
        private string _person;
        private string _phone;
        private string _mobile;
        private string _address;
        private string _zip;
        private int? _countryID;
        private bool _isUse;

        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            set { _iD = value; }
            get { return _iD; }
        }

        /// <summary>
        /// UserID
        /// </summary>
        public int UserID
        {
            set { _userID = value; }
            get { return _userID; }
        }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Person
        {
            set { _person = value; }
            get { return _person; }
        }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }

        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }

        /// <summary>
        /// 联系地址
        /// </summary>
        public string Zip
        {
            set { _zip = value; }
            get { return _zip; }
        }
        public int? CountryID
        {
            set { _countryID = value; }
            get { return _countryID; }
        }
        public bool IsUse
        {
            set { _isUse = value; }
            get { return _isUse; }
        }
        #endregion

        #region ICloneable 成员

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }

    public class t_UserAddressList
    {
        public t_UserAddressList()
        {
            data = new List<t_UserAddress>();
        }
        public List<t_UserAddress> data
        {
            get;
            set;
        }

        public int records
        {
            get;
            set;
        }
    } 
}

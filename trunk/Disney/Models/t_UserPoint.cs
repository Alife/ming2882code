
using System;
using System.Collections.Generic;

namespace Models
{
    /// <summary>
    /// 实体类 t_UserPoint
    /// </summary>
    [Serializable]
    public class t_UserPoint
    {
        public t_UserPoint()
        { }
        #region Model
        private int _iD;
        private int? _order_id;
        private DateTime _trd_dtm;
        private int _user_id;
        private decimal _trd_qty;
        private decimal _point;
        private int _daynum;
        private DateTime _valid_time;
        private bool _isvalid;
        private int _reason;
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            set { _iD = value; }
            get { return _iD; }
        }
        /// <summary>
        /// 订单ID
        /// </summary>
        public int? order_id
        {
            set { _order_id = value; }
            get { return _order_id; }
        }
        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime trd_dtm
        {
            set { _trd_dtm = value; }
            get { return _trd_dtm; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int user_id
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 交易金额直接=0
        /// </summary>
        public decimal trd_qty
        {
            set { _trd_qty = value; }
            get { return _trd_qty; }
        }
        /// <summary>
        /// 当前提成
        /// </summary>
        public decimal point
        {
            set { _point = value; }
            get { return _point; }
        }
        /// <summary>
        /// 有效天数
        /// </summary>
        public int daynum
        {
            set { _daynum = value; }
            get { return _daynum; }
        }
        /// <summary>
        /// 有效日期
        /// </summary>
        public DateTime valid_time
        {
            set { _valid_time = value; }
            get { return _valid_time; }
        }
        public bool isvalid
        {
            set { _isvalid = value; }
            get { return _isvalid; }
        }
        /// <summary>
        /// 生成原因
        /// </summary>
        public int reason
        {
            set { _reason = value; }
            get { return _reason; }
        }
        #endregion Model
    }
    public class t_UserPointList
    {
        public t_UserPointList()
        {
            data = new List<t_UserPoint>();
        }
        public List<t_UserPoint> data
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

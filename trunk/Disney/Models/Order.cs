using System;
using System.Collections;

namespace Models
{
    public class Order
    {
        private int id;
        private string ordercode;
        private int? userid;
        private DateTime ordertime;
        private int status;
        private decimal price;
        private string truename;
        private string mobile;
        private string tel;
        private int cityid;
        private string address;
        private string zip;
        private int shippingid;
        public Order() { }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderCode
        {
            get { return ordercode; }
            set { ordercode = value; }
        }
        public int? UserID
        {
            get { return userid; }
            set { userid = value; }
        }
        /// <summary>
        /// 订单时间
        /// </summary>
        public DateTime OrderTime
        {
            get { return ordertime; }
            set { ordertime = value; }
        }
        /// <summary>
        /// 订单状态：1下单，2支付，3发货，4完成
        /// </summary>
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }
        public string TrueName
        {
            get { return truename; }
            set { truename = value; }
        }
        public int CityID
        {
            get { return cityid; }
            set { cityid = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }
        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }
        public string Zip
        {
            get { return zip; }
            set { zip = value; }
        }
        public int ShippingID
        {
            get { return shippingid; }
            set { shippingid = value; }
        }
    }
    public class OrderLists : CollectionBase
    {
        private int _recordCount;

        public int Add(Order value)
        {
            return base.List.Add(value);
        }

        public Order this[int index]
        {
            get { return (Order)base.List[index]; }
            set { base.List[index] = value; }
        }

        public int RecordNumber
        {
            get { return this._recordCount; }
            set { this._recordCount = value; }
        }
    }
    public class OrderList
    {
        private int id;
        private int orderid;
        private int orderproductid;
        private int quantity;
        private decimal price;
        private DateTime ordertime;
        private OrderProduct orderProduct;
        public OrderList() { if (orderProduct == null)orderProduct = new OrderProduct(); }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public int OrderID
        {
            get { return orderid; }
            set { orderid = value; }
        }
        public int OrderProductID
        {
            get { return orderproductid; }
            set { orderproductid = value; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }
        public DateTime OrderTime
        {
            get { return ordertime; }
            set { ordertime = value; }
        }
        public OrderProduct OrderProduct
        {
            get { return orderProduct; }
            set { orderProduct = value; }
        }
    }
}

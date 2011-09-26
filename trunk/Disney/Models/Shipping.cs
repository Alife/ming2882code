using System;
using System.Collections;

namespace Models
{
    public class Shipping
    {
        private int id;
        private string name;
        private decimal price;
        public int orderid;
        public Shipping()
        { }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }
        public int OrderID
        {
            get { return orderid; }
            set { orderid = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class MessageBox
    {
        public MessageBox() { }
        public MessageBox(bool success, string data, int records)
        {
            this.success = success;
            this.data = data;
            this.records = records;
        }
        public MessageBox(bool success, string msg, string data, int records)
        {
            this.success = success;
            this.msg = msg;
            this.data = data;
            this.records = records;
        }
        public MessageBox(bool success, string msg, string data)
        {
            this.success = success;
            this.msg = msg;
            this.data = data;
        }
        public MessageBox(bool success, string msg)
        {
            this.success = success;
            this.msg = msg;
        }
        public string msg
        {
            get;
            set;
        }
        public string data
        {
            get;
            set;
        }
        public bool success
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

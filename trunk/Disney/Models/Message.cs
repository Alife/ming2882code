using System;
using System.Collections;

namespace Models
{
    public class Message
    {
        private int id;
        private string truename;
        private string address;
        private string mobile;
        private string tel;
        private string qq;
        private DateTime createtime;
        private string content;
        private string replay;
        private DateTime? replaytime;
        public Message() { }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string TrueName
        {
            get { return truename; }
            set { truename = value; }
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
        public string QQ
        {
            get { return qq; }
            set { qq = value; }
        }
        public DateTime CreateTime
        {
            get { return createtime; }
            set { createtime = value; }
        }
        public string Content
        {
            get { return content; }
            set { content = value; }
        }
        public string Replay
        {
            get { return replay; }
            set { replay = value; }
        }
        public DateTime? ReplayTime
        {
            get { return replaytime; }
            set { replaytime = value; }
        }
    }
    public class MessageList : CollectionBase
    {
        private int _recordCount;

        public int Add(Message value)
        {
            return base.List.Add(value);
        }

        public Message this[int index]
        {
            get { return (Message)base.List[index]; }
            set { base.List[index] = value; }
        }

        public int RecordNumber
        {
            get { return this._recordCount; }
            set { this._recordCount = value; }
        }
    }
}

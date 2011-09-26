using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class HintObject
    {
        public HintObject() { }
        public Hashtable _Items = new Hashtable();
        public ICollection Items
        {
            get { return _Items.Values; }
        }
        public void AddItem(string key, string value)
        {
            Hint item = GetItem(key);
            if (item == null)
            {
                item = new Hint();
                item.Key = key;
                item.Value = value;
                _Items.Add(key, item);
            }
            else
                _Items.Remove(key);
        }
        public Hint GetItem(string key)
        {
            return (Hint)_Items[key];
        }
        public void ClearCart()
        {
            _Items.Clear();
        }
    }
    [Serializable]
    public class Hint
    {
        public Hint() { }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}

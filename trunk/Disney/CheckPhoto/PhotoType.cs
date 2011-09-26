using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CheckPhoto
{
    public class PhotoType
    {
        public string id { get; set; }
        public string text { get; set; }
        public string spce { get; set; }
    }
    public class PhotoTypeBLL
    {
        private XmlDocument doc;
        private string xmlPath = string.Empty;
        public PhotoTypeBLL(string xmlFilePath)
        {
            xmlPath = xmlFilePath;
            doc = new XmlDocument();
            doc.Load(xmlFilePath);
        }
        public List<PhotoType> GetList()
        {
            List<PhotoType> list = new List<PhotoType>();
            XmlNodeList nodelist = doc.SelectSingleNode("//entity").ChildNodes;
            foreach (XmlNode item in nodelist)
            {
                PhotoType entity = new PhotoType();
                entity.id = GetNodeAttributeValue(item, "id");
                entity.text = GetNodeAttributeValue(item, "text");
                entity.spce = GetNodeAttributeValue(item, "spce");
                list.Add(entity);
            }
            return list;
        }
        private string GetNodeAttributeValue(XmlNode item, string attributeName)
        {
            if (item == null || item.Attributes[attributeName] == null || string.IsNullOrEmpty(item.Attributes[attributeName].Value))
                return string.Empty;
            return item.Attributes[attributeName].Value;
        }
    }
}

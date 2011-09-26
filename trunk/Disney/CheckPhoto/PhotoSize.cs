using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CheckPhoto
{
    public class PhotoSize
    {
        public string size { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
    public class PhotoSizeBLL
    {
        private XmlDocument doc;
        private string xmlPath = string.Empty;
        public PhotoSizeBLL(string xmlFilePath)
        {
            xmlPath = xmlFilePath;
            doc = new XmlDocument();
            doc.Load(xmlFilePath);
        }
        public List<PhotoSize> GetCoverList()
        {
            List<PhotoSize> list = new List<PhotoSize>();
            XmlNodeList nodelist = doc.SelectSingleNode("//cover").ChildNodes;
            foreach (XmlNode item in nodelist)
            {
                PhotoSize entity = new PhotoSize();
                entity.size = GetNodeAttributeValue(item, "size");
                entity.width = GetIntNodeAttributeValue(item, "width");
                entity.height = GetIntNodeAttributeValue(item, "height");
                list.Add(entity);
            }
            return list;
        }
        public List<PhotoSize> GetRobeList()
        {
            List<PhotoSize> list = new List<PhotoSize>();
            XmlNodeList nodelist = doc.SelectSingleNode("//robe").ChildNodes;
            foreach (XmlNode item in nodelist)
            {
                PhotoSize entity = new PhotoSize();
                entity.size = GetNodeAttributeValue(item, "size");
                entity.width = GetIntNodeAttributeValue(item, "width");
                entity.height = GetIntNodeAttributeValue(item, "height");
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
        private int GetIntNodeAttributeValue(XmlNode item, string attributeName)
        {
            if (item == null || item.Attributes[attributeName] == null || string.IsNullOrEmpty(item.Attributes[attributeName].Value))
                return 0;
            return int.Parse(item.Attributes[attributeName].Value);
        }
    }
}

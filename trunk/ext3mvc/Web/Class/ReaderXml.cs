using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;

namespace Web
{
    public class ReaderXml
    {
        public ReaderXml()
        {
            LoadXml();
        }
        #region Propertis
        public Dictionary<string, XmlNode> XmlDocs { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// 调用Grid的XML，参数不分大小写
        /// </summary>
        /// <param name="fileName">XML文件名称</param>
        /// <param name="code">功能编号</param>
        /// <returns></returns>
        public XPathDocument RenderGridXML(string fileName, string code)
        {
            string noteValue = string.Empty;
            XmlNodeList xmlNodeList = XmlDocs[fileName.ToLower()].ChildNodes;
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                XmlNodeList nodeList = xmlNode.ChildNodes;
                foreach (XmlNode node in nodeList)
                {
                    if (node.Attributes["Code"].Value.ToLower() == code.ToLower())
                        noteValue += node.OuterXml; 
                }
            }
            noteValue = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Page>{0}</Page>", noteValue);
            StringReader xmlReader = new StringReader(noteValue);
            return new XPathDocument(xmlReader); 
        }
        /// <summary>
        /// 加载所有XML，并缓存（缓存暂时没加）
        /// </summary>
        public void LoadXml()
        {
            string currentfolder = HttpContext.Current.Server.MapPath("/xml/");
            string[] arrfiles = Directory.GetFiles(currentfolder, "*.xml", SearchOption.AllDirectories);
            foreach (string fileItem in arrfiles)
            {
                FileInfo fileinfo = new FileInfo(fileItem);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(fileinfo.FullName);
                if (XmlDocs == null) XmlDocs = new Dictionary<string, XmlNode>();
                XmlDocs.Add(fileinfo.Name.Replace(fileinfo.Extension, string.Empty).ToLower(), xmldoc.SelectSingleNode("Entity"));
            }
        }
        #endregion
    }
}

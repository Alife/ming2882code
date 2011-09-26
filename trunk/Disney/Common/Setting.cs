using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Common
{
    public class Setting
    {
        public static Setting Instance = new Setting();
        private Dictionary<string, List<SettingEntity>> _setting;
        private XmlDocument _document;
        private XmlNode _xmlNode;

        public Setting()
        {
            if (_document == null)
            {
                _setting = new Dictionary<string, List<SettingEntity>>();
                _document = new XmlDocument();
                _document.Load(HttpContext.Current.Server.MapPath("~/Setting.config"));
                _xmlNode = _document.SelectSingleNode("entity");
            }
        }

        /// <summary>
        /// 查询下拉常用值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<SettingEntity> Get(string key)
        {
            List<SettingEntity> list = new List<SettingEntity>();
            if (!_setting.ContainsKey(key))
            {
                XmlNode xmlNode = _xmlNode.SelectSingleNode(string.Format("setting[@type=\"{0}\"]", key));
                XmlNodeList xmlNodeList = xmlNode.SelectNodes("add");
                foreach (XmlNode item in xmlNodeList)
                {
                    SettingEntity entity = new SettingEntity();
                    entity.Text = GetNodeAttributeValue(item, "text");
                    entity.Value = GetNodeAttributeValue(item, "value");
                    entity.Key = GetNodeAttributeValue(item, "key");
                    list.Add(entity);
                }
                _setting.Add(key, list);
            }
            else
            {
                list = _setting[key];
            }
            return list;
        }
        public string Get(string key, string value)
        {
            List<SettingEntity> list = Get(key);
            return list.FirstOrDefault(p => p.Value == value).Text;
        }
        /// <summary>
        /// 系统配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SettingEntity GetSetting(string key)
        {
            List<SettingEntity> list = Get("sitesetting");
            return list.Find(delegate(SettingEntity p)
            {
                return (p.Key == key);
            });
        }
        public void SetSetting(string key, string value)
        {
            XmlNode xmlNode = _xmlNode.SelectSingleNode(string.Format("setting[@type=\"{0}\"]", "sitesetting"));
            XmlNodeList xmlNodeList = xmlNode.SelectNodes(string.Format("add[@key=\"{0}\"]", key));
            foreach (XmlNode item in xmlNodeList)
                item.Attributes["value"].Value = value;
            _setting.Remove("sitesetting");
            _document.Save(HttpContext.Current.Server.MapPath("~/App_Data/Setting.xml"));
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

        private bool GetBoolNodeAttributeValue(XmlNode item, string attributeName)
        {
            if (item == null || item.Attributes[attributeName] == null || string.IsNullOrEmpty(item.Attributes[attributeName].Value))
                return false;
            return bool.Parse(item.Attributes[attributeName].Value.ToLower());
        }
    }

    public class SettingEntity
    {
        public SettingEntity() { }
        public string Text
        {
            get;
            set;
        }
        public string Key
        {
            get;
            set;
        }
        public string Value
        {
            get;
            set;
        }
    }
}

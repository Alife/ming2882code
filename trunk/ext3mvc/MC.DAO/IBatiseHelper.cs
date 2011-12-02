using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Reflection;
using System.IO;
using MC.Model;
using MyBatis.DataMapper;
using MyBatis.Common.Data;
using MyBatis.Common.Logging;
using MyBatis.Common.Resources;
using MyBatis.Common.Utilities;
using MyBatis.DataMapper.Configuration;
using MyBatis.DataMapper.Configuration.Interpreters.Config.Xml;
using MyBatis.DataMapper.Session;

namespace MC.DAO
{
    public class SqlBatisHelper : MyBatisHelper
    {
        public IDataMapper dataMapper { get; set; }
        public ISessionFactory sessionFactory { get; set; }
        public ISessionStore sessionStore { get; set; }
        public ConfigurationSetting configurationSetting { get; set; }
        public log4net.ILog _logger { get; set; }
        public string sPreFix { get; set; }

        public void LoadDataBase()
        {
            _logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            sPreFix = "MC.Model.";
            string uri = "assembly://MC.DAO/MC.DAO/sqlmap.config";
            try
            {
                IResource resource = ResourceLoaderRegistry.GetResource(uri);
                IConfigurationEngine engine = new DefaultConfigurationEngine();
                engine.RegisterInterpreter(new XmlConfigurationInterpreter(resource));
                IMapperFactory mapperFactory = engine.BuildMapperFactory();
                sessionFactory = engine.ModelStore.SessionFactory;
                dataMapper = ((IDataMapperAccessor)mapperFactory).DataMapper;
                sessionStore = ((IModelStoreAccessor)dataMapper).ModelStore.SessionStore;
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(ex.Message + "\r\n" + ex.StackTrace);
                try
                {
                    string user = System.Web.HttpContext.Current.User.Identity.Name;
                    if (string.IsNullOrEmpty(user)) user = "游客";
                    sb.Append("\r\n" + user + "----------------");
                }
                catch
                {
                    sb.Append("\r\n游客----------------");
                }
                _logger.Error(sb.ToString());
            }
        }
    }
    public interface MyBatisHelper
    {
        IDataMapper dataMapper { get; set; }
        ISessionFactory sessionFactory { get; set; }
        ISessionStore sessionStore { get; set; }
        ConfigurationSetting configurationSetting { get; set; }
        log4net.ILog _logger { get; set; }
        string sPreFix { get; set; }
        void LoadDataBase();
    }
}

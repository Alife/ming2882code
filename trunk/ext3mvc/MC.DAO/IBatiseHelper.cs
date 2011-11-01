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
    public class IBatiseHelper
    {
        protected static IDataMapper dataMapper = null;
        protected static ISessionFactory sessionFactory = null;
        protected ISessionStore sessionStore = null;
        protected ConfigurationSetting configurationSetting;
        protected readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public IBatiseHelper()
        {
            string uri = "assembly://DAO/DAO/sqlmap.config";
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

        protected const string sPreFix = "MC.Model.";

        protected void SetLoading<T>(IList<T> lis) where T : Entity, new()
        {
            foreach (Entity t in lis)
            {
                t.SetLoading();
            }
        }
    }
}

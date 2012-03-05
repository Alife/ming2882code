using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace Unity.Mvc3
{
    /// <summary>
    /// 应用程序启动的扩展
    /// </summary>
    public class PreApplicationStartCode
    {
        private static bool _isStarting;

        public static void PreStart()
        {
            if (!_isStarting)
            {
                _isStarting = true;

                DynamicModuleUtility.RegisterModule(typeof(RequestLifetimeHttpModule));
            }
        }
    }
}
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace Unity.Mvc3
{
    public static class UnityContainerExtensions
    {
        /// <summary>
        /// Registers all controllers in the calling assembly
        /// </summary>
        /// <param name="container"></param>
        public static void RegisterControllers(this IUnityContainer container)
        {
            var controllerTypes = (from t in Assembly.GetCallingAssembly().GetTypes()
                                   where typeof(IController).IsAssignableFrom(t) && !t.IsAbstract
                                   select t).ToList();

            controllerTypes.ForEach(t => container.RegisterType(t));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoolCode.Web;
using System.Web.UI;

namespace Web
{
    public static class PageExtensions
    {
        // controller.ControllerContext = new ControllerContext(HttpContext.Current, controller);
         //   controller.ValueProvider = new PrefixableValueProvider(controller.ControllerContext, PrefixType.Camel);

        public static void UpdateModel<T>(this Page page, T TModel) where T : class
        {
            Controller controller = new Controller();
            controller.UpdateModel(TModel);
        }
        public static bool TryUpdateModel<T>(this Page page, T TModel) where T : class
        {
            Controller controller = new Controller();
            return controller.TryUpdateModel(TModel);
        }
    }
}

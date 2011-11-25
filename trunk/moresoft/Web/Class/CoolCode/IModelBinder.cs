using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolCode.Web
{
    public interface IModelBinder
    {
        // Methods
        object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext);
    }

 

}

using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;


namespace CoolCode.Web
{

    // Though many of the properties on ControllerContext and its subclassed types are virtual, there are still sealed
    // properties (like ControllerContext.RequestContext, ActionExecutingContext.Result, etc.). If these properties
    // were virtual, a mocking framework might override them with incorrect behavior (property getters would return
    // null, property setters would be no-ops). By sealing these properties, we are forcing them to have the default
    // "get or store a value" semantics that they were intended to have.

    public class ControllerContext
    {

        private HttpContext  _httpContext; 

        // parameterless constructor used for mocking
        public ControllerContext()
        {
        }

        // copy constructor - allows for subclassed types to take an existing ControllerContext as a parameter
        // and we'll automatically set the appropriate properties
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors",
            Justification = "The virtual property setters are only to support mocking frameworks, in which case this constructor shouldn't be called anyway.")]
        protected ControllerContext(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            Controller = controllerContext.Controller;
            _httpContext  = controllerContext.HttpContext ;
        }

        public ControllerContext(HttpContext  httpContext, ControllerBase controller) 
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            _httpContext = httpContext;
            Controller = controller;
        }

        public virtual ControllerBase Controller
        {
            get;
            set;
        }

        public virtual HttpContext  HttpContext
        {
            get
            { 
                return _httpContext;
            }
            set
            {
                _httpContext = value;
            }
        }

       
 

    }
}

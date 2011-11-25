using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics.CodeAnalysis;


namespace CoolCode.Web
{
    public abstract class ControllerBase
    {
        private IDictionary<string, ValueProviderResult> _valueProvider;

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly",
            Justification = "This property is settable so that unit tests can provide mock implementations.")]
        public IDictionary<string, ValueProviderResult> ValueProvider
        {
            get
            {
                if (_valueProvider == null)
                {
                    _valueProvider = new ValueProviderDictionary(ControllerContext);
                }
                return _valueProvider;
            }
            set
            {
                _valueProvider = value;
            }
        }

        private ControllerContext _controllerContext;

        public ControllerContext ControllerContext
        {
            get
            {
                if (_controllerContext == null)
                {
                    _controllerContext = new ControllerContext(HttpContext.Current, this);
                }
                return _controllerContext;
            }
            set
            {
                _controllerContext = value;
            }
        }

 
    }
}

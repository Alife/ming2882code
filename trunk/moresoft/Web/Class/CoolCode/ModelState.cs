using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolCode.Web
{
    [Serializable]
    public class ModelState
    {

        private ModelErrorCollection _errors = new ModelErrorCollection();

        public ValueProviderResult Value
        {
            get;
            set;
        }

        public ModelErrorCollection Errors
        {
            get
            {
                return _errors;
            }
        }
    }
}

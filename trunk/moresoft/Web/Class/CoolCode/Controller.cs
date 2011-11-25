using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace CoolCode.Web
{
    public class Controller : ControllerBase, IDisposable
    {
        private ModelBinderDictionary _binders;

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly",
            Justification = "Property is settable so that the dictionary can be provided for unit testing purposes.")]
        protected internal ModelBinderDictionary Binders
        {
            get
            {
                if (_binders == null)
                {
                    _binders = ModelBinders.Binders;
                }
                return _binders;
            }
            set
            {
                _binders = value;
            }
        }

        private readonly ModelStateDictionary _modelState = new ModelStateDictionary();

        public ModelStateDictionary ModelState
        {
            get
            {
                return _modelState;
                //return ViewData.ModelState;
            }
        } 

        protected internal bool TryUpdateModel<TModel>(TModel model) where TModel : class
        {
            return TryUpdateModel(model, null, null, null, ValueProvider);
        }

        protected internal bool TryUpdateModel<TModel>(TModel model, string prefix) where TModel : class
        {
            return TryUpdateModel(model, prefix, null, null, ValueProvider);
        }

        protected internal bool TryUpdateModel<TModel>(TModel model, string[] includeProperties) where TModel : class
        {
            return TryUpdateModel(model, null, includeProperties, null, ValueProvider);
        }

        protected internal bool TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties) where TModel : class
        {
            return TryUpdateModel(model, prefix, includeProperties, null, ValueProvider);
        }

        protected internal bool TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties) where TModel : class
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties, ValueProvider);
        }

        protected internal bool TryUpdateModel<TModel>(TModel model, IDictionary<string, ValueProviderResult> valueProvider) where TModel : class
        {
            return TryUpdateModel(model, null, null, null, valueProvider);
        }

        protected internal bool TryUpdateModel<TModel>(TModel model, string prefix, IDictionary<string, ValueProviderResult> valueProvider) where TModel : class
        {
            return TryUpdateModel(model, prefix, null, null, valueProvider);
        }

        protected internal bool TryUpdateModel<TModel>(TModel model, string[] includeProperties, IDictionary<string, ValueProviderResult> valueProvider) where TModel : class
        {
            return TryUpdateModel(model, null, includeProperties, null, valueProvider);
        }

        protected internal bool TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, IDictionary<string, ValueProviderResult> valueProvider) where TModel : class
        {
            return TryUpdateModel(model, prefix, includeProperties, null, valueProvider);
        }

        protected internal bool TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties, IDictionary<string, ValueProviderResult> valueProvider) where TModel : class
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            if (valueProvider == null)
            {
                throw new ArgumentNullException("valueProvider");
            }

            Predicate<string> propertyFilter = propertyName => BindAttribute.IsPropertyAllowed(propertyName, includeProperties, excludeProperties);
            IModelBinder binder = Binders.GetBinder(typeof(TModel));

            ModelBindingContext bindingContext = new ModelBindingContext()
            {
                Model = model,
                ModelName = prefix,
                ModelState = ModelState,
                ModelType = typeof(TModel),
                PropertyFilter = propertyFilter,
                ValueProvider = valueProvider
            };
            binder.BindModel(ControllerContext, bindingContext);
            return ModelState.IsValid;
        }

        protected internal void UpdateModel<TModel>(TModel model) where TModel : class
        {
            UpdateModel(model, null, null, null, ValueProvider);
        }

        protected internal void UpdateModel<TModel>(TModel model, string prefix) where TModel : class
        {
            UpdateModel(model, prefix, null, null, ValueProvider);
        }

        protected internal void UpdateModel<TModel>(TModel model, string[] includeProperties) where TModel : class
        {
            UpdateModel(model, null, includeProperties, null, ValueProvider);
        }

        protected internal void UpdateModel<TModel>(TModel model, string prefix, string[] includeProperties) where TModel : class
        {
            UpdateModel(model, prefix, includeProperties, null, ValueProvider);
        }

        protected internal void UpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties) where TModel : class
        {
            UpdateModel(model, prefix, includeProperties, excludeProperties, ValueProvider);
        }

        protected internal void UpdateModel<TModel>(TModel model, IDictionary<string, ValueProviderResult> valueProvider) where TModel : class
        {
            UpdateModel(model, null, null, null, valueProvider);
        }

        protected internal void UpdateModel<TModel>(TModel model, string prefix, IDictionary<string, ValueProviderResult> valueProvider) where TModel : class
        {
            UpdateModel(model, prefix, null, null, valueProvider);
        }

        protected internal void UpdateModel<TModel>(TModel model, string[] includeProperties, IDictionary<string, ValueProviderResult> valueProvider) where TModel : class
        {
            UpdateModel(model, null, includeProperties, null, valueProvider);
        }

        protected internal void UpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, IDictionary<string, ValueProviderResult> valueProvider) where TModel : class
        {
            UpdateModel(model, prefix, includeProperties, null, valueProvider);
        }

        protected internal void UpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties, IDictionary<string, ValueProviderResult> valueProvider) where TModel : class
        {
            bool success = TryUpdateModel(model, prefix, includeProperties, excludeProperties, valueProvider);
            if (!success)
            {
                string message = String.Format(CultureInfo.CurrentUICulture, "MvcResources.Controller_UpdateModel_UpdateUnsuccessful",
                    typeof(TModel).FullName);
                throw new InvalidOperationException(message);
            }
        }


        // The default invoker will never match methods defined on the Controller type, so
        // the Dispose() method is not web-callable.  However, in general, since implicitly-
        // implemented interface methods are public, they are web-callable unless decorated with
        // [NonAction].
        [SuppressMessage("Microsoft.Security", "CA2123:OverrideLinkDemandsShouldBeIdenticalToBase")]
        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
         
    }
}

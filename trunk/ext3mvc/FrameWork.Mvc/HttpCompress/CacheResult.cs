using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Configuration;
using System.Reflection;
using System.Web.Routing;
using System.IO;
using System.Web.Caching;

namespace FrameWork.Mvc.HttpCompress
{
    public class CacheResult : ActionResult
    {
        private string _keyname;
        private string _version;
        private string _type;

        public CacheResult(string keyname, string version, string type)
        {
            this._keyname = keyname;
            this._version = version;
            if (type.ToLower().Contains("css"))
            {
                this._type = @"text/css";
            }

            if (type.ToLower().Contains("javascript"))
            {
                this._type = @"text/javascript";
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            ScriptCombiner myCombiner = new ScriptCombiner
                (this._keyname, this._version, this._type);

            myCombiner.ProcessRequest(context.HttpContext);
        }
    }

    public static class CacheControllerExtensions
    {
        public static CacheResult RenderCacheResult
            (string keyname, string version, string type)
        {
            return new CacheResult(keyname, version, type);
        }
    }

    public class CacheController : Controller
    {
        #region Constructor Definitions
        public CacheController()
            : base()
        {
        }
        #endregion

        #region Method Definitions
        #region public
        public CacheResult CacheContent(string key, string version, string type)
        {
            return CacheControllerExtensions.RenderCacheResult(key, version, type);
        }

        //public CacheResult ClearCache()
        //{
        //    //LOGIC TO CLEAR OUT CACHE
        //}
        #endregion
        #endregion

    } // End class CacheController
}
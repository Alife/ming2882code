using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace FrameWork.Mvc.HttpCompress
{
    public static class CompressControllerExtensions
    {
        public static CompressResult RenderCompressResult
            (string keyname, string version, string type)
        {
            return new CompressResult(keyname, version, type);
        }
    }

    public class CompressController : Controller
    {
        #region Constructor Definitions
        public CompressController()
            : base()
        {
        }
        #endregion

        #region Method Definitions
        #region public
        public CompressResult CompressContent(string key, string version, string type)
        {
            return CompressControllerExtensions.RenderCompressResult(key, version, type);
        }

        #endregion
        #endregion

    }
}

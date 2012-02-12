using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Unity.Mvc3.Result;

namespace Unity.Mvc3.HttpCompress
{
    public static class CompressControllerExtensions
    {
        public static CompressResult RenderCompressResult
            (string keyname, string version, string type)
        {
            return new CompressResult(keyname, version, type);
        }
    }
    /// <summary>
    /// 批量压缩JS与CSS
    /// </summary>
    public class CompressController : Controller
    {
        #region Constructor Definitions
        public CompressController()
            : base()
        {
        }
        #endregion

        #region MVC Action压缩JS与CSS方法
        #region public
        public CompressResult CacheContent(string key, string version, string type)
        {
            return CompressControllerExtensions.RenderCompressResult(key, version, type);
        }

        #endregion
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.IO.Compression;

namespace Web
{
    public class CompressFilter : ActionFilterAttribute
    {
        #region = IsAvailable =
        private bool _isavailable = true;
        public bool IsAvailable
        {
            get { return _isavailable; }
            set { _isavailable = value; }
        }
        #endregion
        public CompressFilter() { }
        public CompressFilter(bool isAvailable)
        {
            _isavailable = isAvailable;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!IsAvailable)
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            if (filterContext.HttpContext.IsCustomErrorEnabled)
            {
                HttpRequestBase request = filterContext.HttpContext.Request;
                string acceptEncoding = request.Headers["Accept-Encoding"];
                if (string.IsNullOrEmpty(acceptEncoding)) return;
                acceptEncoding = acceptEncoding.ToUpperInvariant();
                HttpResponseBase response = filterContext.HttpContext.Response;
                if (acceptEncoding.Contains("GZIP"))
                {
                    response.AppendHeader("Content-encoding", "gzip");
                    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                }
                else if (acceptEncoding.Contains("DEFLATE"))
                {
                    response.AppendHeader("Content-encoding", "deflate");
                    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                }
            }
        }
    }
}

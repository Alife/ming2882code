using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.IO.Compression;
using Unity.Mvc3.HttpCompress;

namespace Unity.Mvc3.Filter
{
    /// <summary>
    /// 压缩View与Content页面
    /// </summary>
    public class CompressFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            string acceptEncoding = filterContext.HttpContext.Request.Headers["Accept-Encoding"];
            if (String.IsNullOrEmpty(acceptEncoding)) return;
            var response = filterContext.HttpContext.Response;
            acceptEncoding = acceptEncoding.ToUpperInvariant();
            if (acceptEncoding.Contains("GZIP"))
            {
                response.AppendHeader("Content-Encoding", "gzip");
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }
            else if (acceptEncoding.Contains("DEFLATE"))
            {
                response.AppendHeader("Content-Encoding", "deflate");
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            }
        }
    }
}

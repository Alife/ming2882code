using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Caching;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using MC.Mvc.Web.HttpCompress;
using MC.Mvc.Web.Helpers.Encoders;

namespace MC.Mvc.Web.Controllers
{
    /// <summary>
    /// 压缩图片和文件
    /// View与后台代码里使用
    /// SO:<img src="/Zip/Image?Path=/Public/Images/bird.png" alt="bird" /> 
    /// </summary>
    public class ZipController : Controller
    {
        IFileBase file;
        const int cacheForMins = 60;

        public ZipController() : this(null) { }
        public ZipController(IFileBase fileBase)
        {
            this.file = fileBase ?? new FileBase();
        }

        byte[] GetBytesFromCache(string key)
        {
            object fromCache = HttpContext.Cache.Get(key);
            if (fromCache == null) return null;

            try
            {
                return fromCache as byte[];
            }
            catch
            { 
                return null;
            }
        }

        public virtual void Image(string path)
        {
            var server = HttpContext.Server;
            string decodedPath = server.UrlDecode(path);
            string mappedPath = server.MapPath(decodedPath);
            byte[] imageBytes = GetBytesFromCache(mappedPath);

            if (imageBytes == null)
            {
                imageBytes = file.ReadAllBytes(mappedPath);
                HttpContext.Cache.Insert(
                    path,
                    imageBytes,
                    null, 
                    Cache.NoAbsoluteExpiration,
                    TimeSpan.FromMinutes(cacheForMins));
            }

            Response.AddFileDependency(mappedPath);
            Response.ContentType = "image/jpeg";
            Response.Cache.SetCacheability(HttpCacheability.Public);
            Response.Cache.SetExpires(Cache.NoAbsoluteExpiration);
            Response.Cache.SetLastModifiedFromFileDependencies();
            Response.AppendHeader("Content-Length", imageBytes.Length.ToString());
            Response.OutputStream.Write(imageBytes, 0, imageBytes.Length);
            Response.Flush();
        }

        void Text(string path, string type)
        {
            //解码路径 
            var server = HttpContext.Server;
            string decodedPath = server.UrlDecode(path);
            string[] paths = decodedPath.Split('|').Select(m => server.MapPath(m)).ToArray();

            //确定是否响应可以用gzip压缩
            string encodingHeader = Request.Headers["Accept-Encoding"];
            bool gzip = (encodingHeader.Contains("gzip") || encodingHeader.Contains("deflate"));
            string encoding = gzip ? "gzip" : "utf-8";

            //创建缓存Key
            string key = MD5.Encode(decodedPath + encoding);
            byte[] bytes = GetBytesFromCache(key);

            if (bytes == null)
            {
                using (var stream = new MemoryStream())
                using (var wstream = gzip ? (Stream)new GZipStream(stream, CompressionMode.Compress) : stream)
                {
                    var allFileText = new StringBuilder();
                    foreach (string filePath in paths)
                    {
                        string fileText = this.file.ReadAllText(filePath);
                        allFileText.Append(fileText);
                        allFileText.Append(Environment.NewLine);
                    }

                    byte[] utf8Bytes = Encoding.UTF8.GetBytes(allFileText.ToString());
                    wstream.Write(utf8Bytes, 0, utf8Bytes.Length);
                    wstream.Close();

                    bytes = stream.ToArray();
                    HttpContext.Cache.Insert(
                        key,
                        bytes,
                        null, //or: new CacheDependency(paths), 
                        Cache.NoAbsoluteExpiration,
                        TimeSpan.FromMinutes(cacheForMins));
                }
            }

            Response.AddFileDependencies(paths);
            Response.ContentType = type;
            Response.AppendHeader("Content-Encoding", encoding);
            Response.Cache.SetCacheability(HttpCacheability.Public);
            Response.Cache.SetExpires(Cache.NoAbsoluteExpiration);
            Response.Cache.SetLastModifiedFromFileDependencies();
            Response.OutputStream.Write(bytes, 0, bytes.Length);
            Response.Flush();
        }

        public void Script(string path)
        {
            Text(path, "application/x-javascript");
        }

        public void Style(string path)
        {
            Text(path, "text/css");
        }
    } 
}

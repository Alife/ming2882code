using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MC.Mvc.Helpers
{
    public class JsonNetResult : ActionResult
    {
        public JsonNetResult() : this(null, null, null) { }
        public JsonNetResult(Object data) : this(data, null, null) { }
        public JsonNetResult(Object data, String contentType) : this(data, contentType, null) { }

        public JsonNetResult(Object data, String contentType, Encoding encoding)
        {
            if (SerializerSettings == null)
                SerializerSettings = new JsonSerializerSettings();
            SerializerSettings.Converters.Add(new JavaScriptDateTimeConverter());
            Formatting = Formatting.Indented;
            Data = data;
            ContentType = contentType;
            ContentEncoding = encoding;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType)
              ? ContentType
              : "application/json";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (Data != null)
            {
                JsonTextWriter writer = new JsonTextWriter(response.Output) { Formatting = Formatting };
                JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
                serializer.Serialize(writer, Data);
                writer.Flush();
            }
        }

        public Encoding ContentEncoding { get; set; }
        public string ContentType { get; set; }
        public object Data { get; set; }
        public JsonSerializerSettings SerializerSettings { get; set; }
        public Formatting Formatting { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;

namespace WebSite
{
    /// <summary>
    /// Uploads 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class Uploads : System.Web.Services.WebService
    {
        /// <summary>
        /// 通过WebService上传文件
        /// </summary>
        /// <param name="fs">文件二进制流</param>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        [WebMethod(Description = "web提供的方法，上传文件到相应的地址")]
        public bool UploadFile(byte[] fs, string path)
        {
            try
            {
                ///定义并实例化一个内存流，以存放提交上来的字节数组。
                System.IO.MemoryStream m = new System.IO.MemoryStream(fs);
                ///定义实际文件对象，保存上载的文件。
                System.IO.FileStream fl = new System.IO.FileStream(path, FileMode.OpenOrCreate);
                ///把内内存里的数据写入物理文件
                m.WriteTo(fl);
                m.Close();
                fl.Close();
                m = null;
                fl = null;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

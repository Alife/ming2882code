using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using Models;
using BLL;

namespace WebSite
{
    /// <summary>
    /// Uploads 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    //允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务。
    [System.Web.Script.Services.ScriptService]
    public class Uploads : System.Web.Services.WebService
    {
        /// <summary>
        /// 通过WebService上传文件
        /// </summary>
        /// <param name="fs">文件二进制流</param>
        /// <param name="fileName">文件名</param>
        /// <param name="saveFileName">保存文件名</param>
        /// <param name="photoType">文件类型</param>
        /// <returns></returns>
        [WebMethod(Description = "web提供的方法，上传文件到相应的地址")]
        public bool UploadFile(byte[] fs, string fileName, string saveFileName, int photoType)
        {
            MemoryStream m = null;
            FileStream fl = null;
            try
            {
                m = new MemoryStream(fs);
                ///定义并实例化一个内存流，以存放提交上来的字节数组。
                string path = string.Format("/images/uploads/{0:yyyyMMdd}/", DateTime.Now);
                if (!Directory.Exists(Server.MapPath(path)))
                    Directory.CreateDirectory(Server.MapPath(path));
                ///定义实际文件对象，保存上载的文件。
                fl = new FileStream(Server.MapPath(path) + saveFileName, FileMode.OpenOrCreate);
                ///把内内存里的数据写入物理文件
                m.WriteTo(fl);
                web_Photo item = new web_Photo();
                item.PhotoTypeID = photoType;
                item.CreateTime = DateTime.Now;
                item.FilePath = path + saveFileName;
                item.Name = fileName;
                item.Remark = string.Empty;
                web_PhotoBLL.Insert(item);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            finally
            {
                if (m != null)
                {
                    m.Close();
                    m.Dispose();
                }
                if (fl != null)
                {
                    fl.Close();
                    fl.Dispose();
                }
            }
        }
    }
}

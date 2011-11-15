using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using Models;
using BLL;
using Common;

namespace Web2011
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
                string[] temStr = saveFileName.Split('.');
                SmallPicFactory.CutSmallPic(System.Drawing.Image.FromStream(fl), Server.MapPath(path) + temStr[0] + "_s400." + temStr[1], 400, 400, 100);
                SmallPicFactory.CutSmallPic(System.Drawing.Image.FromStream(fl), Server.MapPath(path) + temStr[0] + "_s." + temStr[1], 120, 90, 100);
                web_Photo item = new web_Photo();
                item.PhotoTypeID = photoType;
                item.CreateTime = DateTime.Now;
                item.FilePath = path + saveFileName;
                item.FileType = "." + saveFileName.Split('.')[1];
                item.FileSize = fs.Length;
                item.Name = fileName;
                item.Remark = string.Empty;
                web_PhotoBLL.Insert(item);
                return true;
            }
            catch(Exception ex)
            {
                string path = string.Format("/log/{0:yyyyMMdd}/", DateTime.Now);
                if (!Directory.Exists(Server.MapPath(path)))
                    Directory.CreateDirectory(Server.MapPath(path));
                path = Server.MapPath(path + "log.txt");
                StreamWriter sw;
                if (!System.IO.File.Exists(path))
                    sw = System.IO.File.CreateText(path);
                else
                    sw = System.IO.File.AppendText(path);
                sw.WriteLine(DateTime.Now);
                sw.WriteLine(ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : ""));
                sw.WriteLine("1.错误：" + ex.HelpLink);
                sw.WriteLine("2.错误：" + ex.Source);
                sw.WriteLine("3.错误：" + ex.StackTrace);
                sw.WriteLine("4.错误：" + ex.TargetSite);
                sw.Close();
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

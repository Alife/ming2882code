using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using Microsoft.VisualBasic;

namespace Common
{
    public class CG2BFilter : Stream
    {
        Stream responseStream;
        long position;
        StringBuilder responseHtml;

        public CG2BFilter(Stream inputStream)
        {
            responseStream = inputStream;
            responseHtml = new StringBuilder();
        }

        #region   Filter   overrides
        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Close()
        {
            responseStream.Close();
        }

        public override void Flush()
        {
            responseStream.Flush();
        }

        public override long Length
        {
            get { return 0; }
        }

        public override long Position
        {
            get { return position; }
            set { position = value; }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return responseStream.Seek(offset, origin);
        }

        public override void SetLength(long length)
        {
            responseStream.SetLength(length);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return responseStream.Read(buffer, offset, count);
        }
        #endregion

        #region   转换任务
        public override void Write(byte[] buffer, int offset, int count)
        {
            string strBuffer = System.Text.Encoding.UTF8.GetString(buffer, offset, count);
            string finalHtml = Simplified2Traditional(strBuffer);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(finalHtml);
            responseStream.Write(data, 0, data.Length);
        }

        #endregion
        #region   自定义函数
        /// <summary>
        /// 繁体转简体
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Traditional2Simplified(string str)
        {   
            return Strings.StrConv(str, VbStrConv.SimplifiedChinese, 9);
        }
        /// <summary>
        /// 简体转繁体
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Simplified2Traditional(string str)
        {   
            return Strings.StrConv(str, VbStrConv.TraditionalChinese, 9);
        }
        #endregion

    }

    public class RBTW
    {
        #region   自定义函数
        /// <summary>
        /// 繁体转简体
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Traditional2Simplified(string str)
        {
            return Strings.StrConv(str, VbStrConv.SimplifiedChinese, 9);
        }
        /// <summary>
        /// 简体转繁体
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Simplified2Traditional(string str)
        {
            return Strings.StrConv(str, VbStrConv.TraditionalChinese, 9);
        }
        #endregion
    }
}

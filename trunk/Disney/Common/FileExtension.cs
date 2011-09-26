using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class FileExtension
    {
        #region 验证图片格式
        public static bool IsImages(System.IO.Stream st)
        {
            System.IO.BinaryReader r = new System.IO.BinaryReader(st);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                fileclass = buffer.ToString();
                buffer = r.ReadByte();
                fileclass += buffer.ToString();
            }
            catch
            {
            }
            FileExt ext = (FileExt)Enum.Parse(typeof(FileExt), fileclass);
            if (ext == FileExt.JPG || ext == FileExt.GIF || ext == FileExt.PNG || ext == FileExt.BMP)
                return true;
            else
                return false;
        }
        #endregion
        #region 验证文件格式
        public static bool IsFile(System.IO.Stream st)
        {
            System.IO.BinaryReader r = new System.IO.BinaryReader(st);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                fileclass = buffer.ToString();
                buffer = r.ReadByte();
                fileclass += buffer.ToString();
            }
            catch
            {
            }
            FileExt ext = (FileExt)Enum.Parse(typeof(FileExt), fileclass);
            if (ext == FileExt.JPG || ext == FileExt.GIF || ext == FileExt.PNG || ext == FileExt.BMP || ext == FileExt.DOC)
                return true;
            else
                return false;
        }
        #endregion
        #region 验证视频格式
        public static bool IsVideo(System.IO.Stream st)
        {
            System.IO.BinaryReader r = new System.IO.BinaryReader(st);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                fileclass = buffer.ToString();
                buffer = r.ReadByte();
                fileclass += buffer.ToString();
            }
            catch
            {
            }
            FileExt ext = (FileExt)Enum.Parse(typeof(FileExt), fileclass);
            if (ext == FileExt.swf_flv || ext == FileExt.wmv)
                return true;
            else
                return false;
        }
        #endregion
        #region 验证图片格式
        public static bool IsCarte(System.IO.Stream st)
        {
            System.IO.BinaryReader r = new System.IO.BinaryReader(st);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                fileclass = buffer.ToString();
                buffer = r.ReadByte();
                fileclass += buffer.ToString();
            }
            catch
            {
                st.Close();
                st.Dispose();
                return false;
            }
            FileExt ext = (FileExt)Enum.Parse(typeof(FileExt), fileclass);
            if (ext == FileExt.JPG || ext == FileExt.PSD || ext == FileExt.PNG || ext == FileExt.BMP || ext == FileExt.PDF)
                return true;
            else
                return false;
        }
        #endregion
    }
    public enum FileExt
    {
        JPG = 255216,
        PSD = 5666,
        PDF = 3780,
        GIF = 7173,
        BMP = 6677,
        PNG = 13780,
        RAR = 8297,
        ZIP = 8075,
        _7Z = 55122,
        VALIDFILE = 9999999,
        TXT = 98109,
        xls_doc_ppt = 208207,
        swf_flv = 7076,
        wmv = 4838,
        EXE = 7790,
        DOC = 208207
    }
}

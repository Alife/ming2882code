using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing.Drawing2D;

namespace Common
{
    public class SmallPicFactory
    {
        /// <summary>
        /// 调整图片分辨率
        /// </summary>
        /// <param name="sourcePath">源图物理地址</param>
        /// <param name="savePath">调整图物理地址</param>
        /// <param name="resolution">要设置的分辨率</param>
        public static void setResolution(string sourcePath, string savePath, float resolution)
        {
            Bitmap img = new Bitmap(sourcePath);
            img.SetResolution(resolution, resolution);
            img.Save(savePath);
            img.Dispose();
        }
        /// <summary>
        /// 高品质缩放图片
        /// </summary>
        /// <param name="sourcePath">源图物理地址</param>
        /// <param name="savePath">缩放图物理地址</param>
        /// <param name="width">缩放后宽度</param>
        /// <param name="height">缩放后高度</param>
        /// <param name="contrast">对比度[-100, 100]</param>
        /// <param name="quality">品质[0-100]</param>
        /// <param name="resolution">分辨率</param>
        public static void HighQualityPicZoom(string sourcePath, string savePath, int width, int height, int contrast, long quality, float resolution)
        {
            System.Drawing.Image original = System.Drawing.Image.FromFile(sourcePath);
            Bitmap img = new Bitmap(width, height);
            if (resolution != 0f)
            {
                img.SetResolution(resolution, resolution);
            }
            Graphics gdiobj = Graphics.FromImage(img);
            gdiobj.CompositingQuality = CompositingQuality.HighQuality;
            gdiobj.SmoothingMode = SmoothingMode.HighQuality;
            gdiobj.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gdiobj.PixelOffsetMode = PixelOffsetMode.HighQuality;
            gdiobj.FillRectangle(new SolidBrush(Color.Transparent), 0, 0, width, height);
            Rectangle destrect = new Rectangle(0, 0, width, height);
            gdiobj.DrawImage(original, destrect, 0, 0, original.Width, original.Height, GraphicsUnit.Pixel);
            System.Drawing.Imaging.EncoderParameters ep = new System.Drawing.Imaging.EncoderParameters(1);
            ep.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            string sExt = sourcePath.Substring(sourcePath.LastIndexOf(".")).ToLower();
            System.Drawing.Imaging.ImageCodecInfo ici = GetEncoderInfo(GetImgType(sExt));
            try
            {
                savePath = savePath.Replace("\\", "/");
                img = ImageUtils.KiContrast(img, contrast);
                if (System.IO.Directory.Exists(savePath.Substring(0, savePath.LastIndexOf("/"))) == false)//如果不存在则创建
                {
                    System.IO.Directory.CreateDirectory(savePath.Substring(0, savePath.LastIndexOf("/")));
                }
                if (ici != null)
                {
                    img.Save(savePath, ici, ep);
                }
                else
                {
                    img.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            finally
            {
                original.Dispose();
                img.Dispose();
                gdiobj.Dispose();
                ep.Dispose();
            }
        }
        public static void HighQualityPicZoom(System.Drawing.Image original, string savePath,
            int width, int height, int contrast, long quality, float resolution)
        {
            Bitmap img = new Bitmap(width, height);
            if (resolution != 0f)
                img.SetResolution(resolution, resolution);
            Graphics gdiobj = Graphics.FromImage(img);
            gdiobj.CompositingQuality = CompositingQuality.HighQuality;
            gdiobj.SmoothingMode = SmoothingMode.HighQuality;
            gdiobj.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gdiobj.PixelOffsetMode = PixelOffsetMode.HighQuality;
            gdiobj.FillRectangle(new SolidBrush(Color.Transparent), 0, 0, width, height);
            Rectangle destrect = new Rectangle(0, 0, width, height);
            gdiobj.DrawImage(original, destrect, 0, 0, original.Width, original.Height, GraphicsUnit.Pixel);
            System.Drawing.Imaging.EncoderParameters ep = new System.Drawing.Imaging.EncoderParameters(1);
            ep.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            System.Drawing.Imaging.ImageCodecInfo ici = GetEncoderInfo(GetImgType(original.RawFormat));
            try
            {
                savePath = savePath.Replace("\\", "/");
                img = ImageUtils.KiContrast(img, contrast);
                if (System.IO.Directory.Exists(savePath.Substring(0, savePath.LastIndexOf("/"))) == false)//如果不存在则创建
                {
                    System.IO.Directory.CreateDirectory(savePath.Substring(0, savePath.LastIndexOf("/")));
                }
                if (ici != null)
                {
                    img.Save(savePath, ici, ep);
                }
                else
                {
                    img.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            finally
            {
                img.Dispose();
                gdiobj.Dispose();
                ep.Dispose();
            }
        }
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="sourcePath">源图物理地址</param>
        /// <param name="savePath">缩放图物理地址</param>
        /// <param name="maxWidth">最大宽度</param>
        /// <param name="maxHeight">最大高度</param>
        public static void CutSmallPic(string sourcePath, string savePath, int maxWidth, int maxHeight, float resolution)
        {
            System.Drawing.Image original = System.Drawing.Image.FromFile(sourcePath);
            Size _newSize = ImageUtils.ResizeImage(original.Width, original.Height, maxWidth, maxHeight);
            original.Dispose();
            HighQualityPicZoom(sourcePath, savePath, _newSize.Width, _newSize.Height, 5, 95L, resolution);
        }
        public static void CutSmallPic(System.Drawing.Image original, string savePath, int maxWidth, int maxHeight, float resolution)
        {
            Size _newSize = ImageUtils.ResizeImage(original.Width, original.Height, maxWidth, maxHeight);
            HighQualityPicZoom(original, savePath, _newSize.Width, _newSize.Height, 5, 95L, resolution);
        }
        /// <summary>
        /// 裁切缩略图，将大图缩小后，按指定大小进行裁切
        /// </summary>
        /// <param name="sourcePath">源图物理地址</param>
        /// <param name="savePath">缩放图物理地址</param>
        /// <param name="cutWidth">裁切宽度</param>
        /// <param name="cutHeight">裁切高度</param>
        public static void CutSmallPic(string sourcePath, string savePath, int cutWidth, int cutHeight)
        {
            System.Drawing.Image original = System.Drawing.Image.FromFile(sourcePath);
            Size _newSize = ImageUtils.ResizeImageForCut(original.Width, original.Height, cutWidth, cutHeight);
            original.Dispose();
            HighQualityPicZoom(sourcePath, savePath, _newSize.Width, _newSize.Height, 5, 100L, 0f);
            original = System.Drawing.Image.FromFile(savePath);
            float x = 0f;
            float y = 0f;
            if (cutWidth == _newSize.Width)
            {
                y = (_newSize.Height - cutHeight) / 2;
            }
            else
            {
                x = (_newSize.Width - cutWidth) / 2;
            }
            Bitmap img = new Bitmap(cutWidth, cutHeight);
            img.SetResolution(72f, 72f);
            Graphics gdiobj = Graphics.FromImage(img);
            gdiobj.CompositingQuality = CompositingQuality.HighQuality;
            gdiobj.SmoothingMode = SmoothingMode.HighQuality;
            gdiobj.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gdiobj.PixelOffsetMode = PixelOffsetMode.HighQuality;
            gdiobj.FillRectangle(new SolidBrush(Color.Transparent), 0, 0, _newSize.Width, _newSize.Height);
            Rectangle destrect = new Rectangle(0, 0, _newSize.Width, _newSize.Height);
            gdiobj.DrawImage(original, new Rectangle(0, 0, cutWidth, cutHeight), new Rectangle(Convert.ToInt32(x), Convert.ToInt32(y), cutWidth, cutHeight), GraphicsUnit.Pixel);
            original.Dispose();
            System.Drawing.Imaging.EncoderParameters ep = new System.Drawing.Imaging.EncoderParameters(1);
            ep.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)100);
            string sExt = sourcePath.Substring(sourcePath.LastIndexOf(".")).ToLower();
            System.Drawing.Imaging.ImageCodecInfo ici = GetEncoderInfo(GetImgType(sExt));
            try
            {
                savePath = savePath.Replace("\\", "/");
                //img = ImageUtils.KiContrast(img, 10);
                if (System.IO.Directory.Exists(savePath.Substring(0, savePath.LastIndexOf("/"))) == false)//如果不存在则创建
                {
                    System.IO.Directory.CreateDirectory(savePath.Substring(0, savePath.LastIndexOf("/")));
                }
                if (ici != null)
                {
                    img.Save(savePath, ici, ep);
                }
                else
                {
                    img.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            finally
            {

                img.Dispose();
                gdiobj.Dispose();
                ep.Dispose();
            }
        }
        public static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        public static string GetImgType(System.Drawing.Imaging.ImageFormat ext)
        {
            if (ext == System.Drawing.Imaging.ImageFormat.Jpeg)
                return "image/jpeg";
            else if (ext == System.Drawing.Imaging.ImageFormat.Png)
                return "image/png";
            else if (ext == System.Drawing.Imaging.ImageFormat.Tiff)
                return "image/tif";
            else if (ext == System.Drawing.Imaging.ImageFormat.Bmp)
                return "image/bmp";
            else if (ext == System.Drawing.Imaging.ImageFormat.Emf)
                return "image/emf";
            else if (ext == System.Drawing.Imaging.ImageFormat.Gif)
                return "image/gif";
            else
                return "image/jpeg";
        }
        public static string GetImgType(string ext)
        {
            //string imgType = "image/jpeg";
            switch (ext)
            {
                case ".jpe":
                    return "image/jpeg";
                case ".jpeg":
                    return "image/jpeg";
                case ".jpg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".tif":
                    return "image/tif";
                case ".tiff":
                    return "image/tiff";
                case ".bmp":
                    return "image/bmp";
                case ".gif":
                    return "image/gif";
                default:
                    return "image/jpeg";
            }
        }
        private static Size NewSize(int maxWidth, int maxHeight, int width, int height)
        {
            double w = 0.0;
            double h = 0.0;
            double sw = Convert.ToDouble(width);
            double sh = Convert.ToDouble(height); double mw = Convert.ToDouble(maxWidth);
            double mh = Convert.ToDouble(maxHeight); if (sw < mw && sh < mh)
            {
                w = sw;
                h = sh;
            }
            else if ((sw / sh) > (mw / mh))
            {
                w = maxWidth;
                h = (w * sh) / sw;
            }
            else
            {
                h = maxHeight;
                w = (h * sw) / sh;
            }
            return new Size(Convert.ToInt32(w), Convert.ToInt32(h));
        }
        public static void SendSmallImage(string fileName,string newFile,int maxHeight,int maxWidth)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(fileName);
            System.Drawing.Imaging.ImageFormat
            thisFormat = img.RawFormat;
            Size newSize = NewSize(maxWidth, maxHeight, img.Width, img.Height);
            Bitmap outBmp = new Bitmap(newSize.Width, newSize.Height);
            Graphics g = Graphics.FromImage(outBmp);
            // 设置画布的描绘质量
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(img, new Rectangle(0, 0, newSize.Width, newSize.Height),
            0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
            g.Dispose();
            // 以下代码为保存图片时,设置压缩质量
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;
            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象.
            ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo jpegICI = null;
            for (int x = 0;
            x < arrayICI.Length;
            x++)
            {
                if (arrayICI[x].FormatDescription.Equals("JPEG"))
                {
                    jpegICI = arrayICI[x];
                    //设置JPEG编码
                    break;
                }
            }
            if (jpegICI != null)
            {
                outBmp.Save(newFile, jpegICI, encoderParams);
            }
            else
            {
                outBmp.Save(newFile,
                thisFormat);
            }
            img.Dispose();
            outBmp.Dispose();
        }
        /// <summary>
        /// 固定宽度
        /// </summary>
        /// <param name="original"></param>
        /// <param name="savePath"></param>
        /// <param name="width"></param>
        public static void CutSmallPic(System.Drawing.Image original, string savePath, int width)
        {            int ow = original.Width;
            int oh = original.Height;
            int height = original.Height * width / original.Width;
            HighQualityPicZoom(original, savePath, width, height, 5, 95L, 72f);
        }
        /// <summary>
        /// 生成缩略图(最终图片固定大小,图片按比例缩小,并为缩略图加上边框,以jpg格式保存)
        /// </summary>
        /// <param name="originalImage"></param>
        /// <param name="toPath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="backColor"></param>
        /// <param name="borderColor"></param>
        public static void CutSmallPic(System.Drawing.Image originalImage, string toPath, int width, int height, string backColor, string borderColor)
        {
            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            string mode;

            if (ow < towidth && oh < toheight)
            {
                towidth = ow;
                toheight = oh;
            }
            else
            {
                if (originalImage.Width / originalImage.Height >= width / height)
                {
                    mode = "W";
                }
                else
                {
                    mode = "H";
                }
                switch (mode)
                {
                    case "W"://指定宽，高按比例 
                        toheight = originalImage.Height * width / originalImage.Width;
                        break;
                    case "H"://指定高，宽按比例 
                        towidth = originalImage.Width * height / originalImage.Height;
                        break;
                    default:
                        break;
                }
            }
            //新建一个bmp图片 
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(width, height);
            //新建一个画板 
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            g.CompositingQuality = CompositingQuality.HighQuality;
            //设置高质量插值法
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            //清空画布并以指定颜色填充 
            g.Clear(ColorTranslator.FromHtml(backColor));
            //在指定位置并且按指定大小绘制原图片的指定部分 
            int top = (height - toheight) / 2;
            int left = (width - towidth) / 2;
            g.DrawImage(originalImage, 
                        new System.Drawing.Rectangle(left, top, towidth, toheight),
                        new System.Drawing.Rectangle(x, y, ow, oh),
                        System.Drawing.GraphicsUnit.Pixel);
            if (borderColor != string.Empty)
            {
                Pen pen = new Pen(ColorTranslator.FromHtml(borderColor));
                g.DrawRectangle(pen, 0, 0, width - 1, height - 1);
            }
            try
            {
                bitmap = ImageUtils.KiContrast(bitmap, 5);
                //以jpg格式保存缩略图 
                bitmap.Save(toPath, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                //originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
    }
}

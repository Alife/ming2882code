using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Common
{
    public class CutPhotoHelp
    {
        public static string SaveCutPic(string pPath, string pSavedPath, int pPartStartPointX, int pPartStartPointY, 
            int pPartWidth, int pPartHeight, int pOrigStartPointX, int pOrigStartPointY, int imageWidth, int imageHeight)
        {
            using (Image originalImg = Image.FromFile(pPath))
            {
                if (originalImg.Width == imageWidth && originalImg.Height == imageHeight)
                {
                    return SaveCutPic(pPath, pSavedPath, pPartStartPointX, pPartStartPointY, pPartWidth, pPartHeight,
                            pOrigStartPointX, pOrigStartPointY);
                }
                string filename = DateTime.Now.ToString("yyyyMMddHHmmss");
                string filePath = pSavedPath + "\\" + filename + ".jpg";
                Bitmap thumimg = MakeThumbnail(originalImg, imageWidth, imageHeight);
                Bitmap partImg = new Bitmap(pPartWidth, pPartHeight);
                Graphics graphics = Graphics.FromImage(partImg);
                Rectangle destRect = new Rectangle(new Point(pPartStartPointX, pPartStartPointY), new Size(pPartWidth, pPartHeight));//目标位置
                Rectangle origRect = new Rectangle(new Point(pOrigStartPointX, pOrigStartPointY), new Size(pPartWidth, pPartHeight));//原图位置（默认从原图中截取的图片大小等于目标图片的大小）
                ///文字水印  
                Graphics G = Graphics.FromImage(partImg);
                //Font f = new Font("Lucida Grande", 6);
                //Brush b = new SolidBrush(Color.Gray);
                G.Clear(Color.Transparent);
                // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。 
                G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                // 指定高质量、低速度呈现。 
                G.SmoothingMode = SmoothingMode.HighQuality;
                G.CompositingQuality = CompositingQuality.HighQuality;
                graphics.DrawImage(thumimg, destRect, origRect, GraphicsUnit.Pixel);
                //G.DrawString("Xuanye", f, b, 0, 0);
                G.Dispose();
                originalImg.Dispose();

                if (File.Exists(filePath))
                {
                    File.SetAttributes(filePath, FileAttributes.Normal);
                    File.Delete(filePath);
                }
                // 以下代码为保存图片时，设置压缩质量
                EncoderParameters encoderParams = new EncoderParameters();
                long[] quality = new long[1];
                quality[0] = 100;

                EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;

                //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象。
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICI = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    string Format = arrayICI[x].FormatDescription;
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICI = arrayICI[x];//设置JPEG编码
                        break;
                    }
                }

                if (jpegICI != null)
                {
                    partImg.Save(filePath, jpegICI, encoderParams);
                }
                else
                {
                    partImg.Save(filePath, ImageFormat.Jpeg);
                }

                //partImg.Save(filePath, ImageFormat.Jpeg);
                partImg.Dispose();
                thumimg.Dispose();
                return filename + ".jpg";
            }
        }

        public static void SendSmallImage(string fileName, string newFile, int maxHeight, int maxWidth)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(fileName);
            System.Drawing.Imaging.ImageFormat thisFormat = img.RawFormat;

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

            // 以下代码为保存图片时，设置压缩质量
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;

            //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象。
            ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo jpegICI = null;
            for (int x = 0; x < arrayICI.Length; x++)
            {
                string Format = arrayICI[x].FormatDescription;
                if (arrayICI[x].FormatDescription.Equals("JPEG"))
                {
                    jpegICI = arrayICI[x];//设置JPEG编码
                    break;
                }
            }

            if (jpegICI != null)
            {
                outBmp.Save(newFile, jpegICI, encoderParams);
            }
            else
            {
                outBmp.Save(newFile, thisFormat);
            }

            img.Dispose();
            outBmp.Dispose();
        }

        private static Size NewSize(int maxWidth, int maxHeight, int width, int height)
        {
            double w = 0.0;
            double h = 0.0;
            double sw = Convert.ToDouble(width);
            double sh = Convert.ToDouble(height);
            double mw = Convert.ToDouble(maxWidth);
            double mh = Convert.ToDouble(maxHeight);

            if (sw < mw && sh < mh)
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

        public static Bitmap MakeThumbnail(Image fromImg, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            int ow = fromImg.Width;
            int oh = fromImg.Height;

            //新建一个画板
            Graphics g = Graphics.FromImage(bmp);

            //设置高质量插值法
            g.InterpolationMode = InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            g.DrawImage(fromImg, new Rectangle(0, 0, width, height),
                new Rectangle(0, 0, ow, oh),
                GraphicsUnit.Pixel);

            return bmp;

        }

        public static string SaveCutPic(string pPath, string pSavedPath, int pPartStartPointX, int pPartStartPointY, int pPartWidth, int pPartHeight, int pOrigStartPointX, int pOrigStartPointY)
        {
            string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            string filePath = pSavedPath + "\\" + filename;

            using (Image originalImg = Image.FromFile(pPath))
            {
                Bitmap partImg = new Bitmap(pPartWidth, pPartHeight);
                Graphics graphics = Graphics.FromImage(partImg);
                Rectangle destRect = new Rectangle(new Point(pPartStartPointX, pPartStartPointY), new Size(pPartWidth, pPartHeight));//目标位置
                Rectangle origRect = new Rectangle(new Point(pOrigStartPointX, pOrigStartPointY), new Size(pPartWidth, pPartHeight));//原图位置（默认从原图中截取的图片大小等于目标图片的大小）

                ///注释 文字水印  
                Graphics G = Graphics.FromImage(partImg);
                //Font f = new Font("Lucida Grande", 6);
                //Brush b = new SolidBrush(Color.Gray);
                G.Clear(Color.White);
                // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。 
                G.InterpolationMode = InterpolationMode.HighQualityBicubic;
                // 指定高质量、低速度呈现。 
                G.SmoothingMode = SmoothingMode.HighQuality;

                graphics.DrawImage(originalImg, destRect, origRect, GraphicsUnit.Pixel);
                //G.DrawString("Xuanye", f, b, 0, 0);
                G.Dispose();

                originalImg.Dispose();
                if (File.Exists(filePath))
                {
                    File.SetAttributes(filePath, FileAttributes.Normal);
                    File.Delete(filePath);
                }
                partImg.Save(filePath, ImageFormat.Jpeg);
                partImg.Dispose();
            }
            return filename;
        }

    }
}

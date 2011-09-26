using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.IO;

namespace Common
{
    public class ImageUtils
    {
        /// <summary>
        /// 检查文件的格式
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ImageFormat GetFormat(string name)
        {
            string ext = name.Substring(name.LastIndexOf(".") + 1);
            switch (ext.ToLower())
            {
                case "jpg":
                case "jpeg":
                    return ImageFormat.Jpeg;
                case "bmp":
                    return ImageFormat.Bmp;
                case "png":
                    return ImageFormat.Png;
                case "gif":
                    return ImageFormat.Gif;
                default:
                    return ImageFormat.Jpeg;
            }
        }
        /// <summary>
        /// 检查文件是否存在和文件名是否合法
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static bool ValidateFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                return false;
            }

            return Regex.IsMatch(filepath,
                "(?:[^\\/\\*\\?\\\"\\<\\>\\|\\n\\r\\t]+)\\.(?:jpg|jpeg|gif|png|bmp)",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant
                );
        }

        /// <summary>
        /// 返回新图片尺寸
        /// </summary>
        /// <param name="width">原始宽</param>
        /// <param name="height">原始高</param>
        /// <param name="maxWidth">新图片最大宽</param>
        /// <param name="maxHeight">新图片最大高</param>
        /// <returns>返回新图片尺寸</returns>
        public static Size ResizeImage(int width, int height, int maxWidth, int maxHeight)
        {
            decimal MAX_WIDTH = (decimal)maxWidth;
            decimal MAX_HEIGHT = (decimal)maxHeight;
            decimal ASPECT_RATIO = MAX_WIDTH / MAX_HEIGHT;

            int newWidth, newHeight;

            decimal originalWidth = (decimal)width;
            decimal originalHeight = (decimal)height;

            if (originalWidth > MAX_WIDTH || originalHeight > MAX_HEIGHT)
            {
                decimal factor;
                // determine the largest factor 
                if (originalWidth / originalHeight > ASPECT_RATIO)
                {
                    factor = originalWidth / MAX_WIDTH;
                    newWidth = Convert.ToInt32(originalWidth / factor);
                    newHeight = Convert.ToInt32(originalHeight / factor);
                }
                else
                {
                    factor = originalHeight / MAX_HEIGHT;
                    newWidth = Convert.ToInt32(originalWidth / factor);
                    newHeight = Convert.ToInt32(originalHeight / factor);
                }
            }
            else
            {
                newWidth = width;
                newHeight = height;
            }

            return new Size(newWidth, newHeight);

        }

        /// <summary>
        /// 返回新图片尺寸
        /// </summary>
        /// <param name="width">原始宽</param>
        /// <param name="height">原始高</param>
        /// <param name="cutWidth">新图片裁切宽度</param>
        /// <param name="cutHeight">新图片裁切高度</param>
        /// <returns>返回新图片尺寸</returns>
        public static Size ResizeImageForCut(int width, int height, int cutWidth, int cutHeight)
        {
            decimal MAX_WIDTH = (decimal)cutWidth;
            decimal MAX_HEIGHT = (decimal)cutHeight;
            decimal ASPECT_RATIO = MAX_WIDTH / MAX_HEIGHT;

            int newWidth, newHeight;

            decimal originalWidth = (decimal)width;
            decimal originalHeight = (decimal)height;

            if (originalWidth > MAX_WIDTH && originalHeight > MAX_HEIGHT)
            {
                decimal factor;
                // determine the largest factor 
                if (originalWidth / originalHeight > ASPECT_RATIO)
                {
                    factor = originalHeight / MAX_HEIGHT;
                    newWidth = Convert.ToInt32(originalWidth / factor);
                    newHeight = cutHeight;
                }
                else
                {
                    factor = originalWidth / MAX_WIDTH;
                    newWidth = cutWidth;
                    newHeight = Convert.ToInt32(originalHeight / factor);
                }
            }
            else
            {
                newWidth = width;
                newHeight = height;
            }

            return new Size(newWidth, newHeight);

        }

        /// <summary>
        /// 获取图片的方向
        /// </summary>
        /// <param name="width">图片的宽</param>
        /// <param name="height">图片的高</param>
        /// <returns>返回图片的方向</returns>
        public static string getAspect(int width, int height)
        {
            if (width > height)
            {
                return "horizontal";//横片
            }
            else if (width == height)
            {
                return "square";//方片
            }
            else
            {
                return "erect";//竖片
            }
        }

        //// <summary>
        /// 图像对比度调整
        /// </summary>
        /// <param name="b">原始图</param>
        /// <param name="degree">对比度[-100, 100]</param>
        /// <returns></returns>
        public static Bitmap KiContrast(Bitmap b, int degree)
        {
            if (b == null)
            {
                return null;
            }

            if (degree < -100) degree = -100;
            if (degree > 100) degree = 100;

            try
            {

                double pixel = 0;
                double contrast = (100.0 + degree) / 100.0;
                contrast *= contrast;
                int width = b.Width;
                int height = b.Height;
                BitmapData data = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                unsafe
                {
                    byte* p = (byte*)data.Scan0;
                    int offset = data.Stride - width * 3;
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            // 处理指定位置像素的对比度
                            for (int i = 0; i < 3; i++)
                            {
                                pixel = ((p[i] / 255.0 - 0.5) * contrast + 0.5) * 255;
                                if (pixel < 0) pixel = 0;
                                if (pixel > 255) pixel = 255;
                                p[i] = (byte)pixel;
                            } // i
                            p += 3;
                        } // x
                        p += offset;
                    } // y
                }
                b.UnlockBits(data);
                return b;
            }
            catch
            {
                return null;
            }
        } // end of Contrast



        //// <summary>
        /// 图像明暗调整
        /// </summary>
        /// <param name="b">原始图</param>
        /// <param name="degree">亮度[-255, 255]</param>
        /// <returns></returns>
        public static Bitmap KiLighten(Bitmap b, int degree)
        {
            if (b == null)
            {
                return null;
            }

            if (degree < -255) degree = -255;
            if (degree > 255) degree = 255;

            try
            {

                int width = b.Width;
                int height = b.Height;

                int pix = 0;

                BitmapData data = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                unsafe
                {
                    byte* p = (byte*)data.Scan0;
                    int offset = data.Stride - width * 3;
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            // 处理指定位置像素的亮度
                            for (int i = 0; i < 3; i++)
                            {
                                pix = p[i] + degree;

                                if (degree < 0) p[i] = (byte)Math.Max(0, pix);
                                if (degree > 0) p[i] = (byte)Math.Min(255, pix);

                            } // i
                            p += 3;
                        } // x
                        p += offset;
                    } // y
                }

                b.UnlockBits(data);

                return b;
            }
            catch
            {
                return null;
            }

        } // end of Lighten

        public static void Main()
        {

            Size size = ResizeImage(1024, 794, 640, 480);
            System.Console.WriteLine(size.Width);
            System.Console.WriteLine(size.Height);
        }
    }
}

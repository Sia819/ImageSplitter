using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageSplitter.Common
{
    public class BitmapExtension
    {
        /// <summary>
        /// Bitmap 객체를 BitmapImage 객체로 변환합니다.
        /// </summary>
        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using MemoryStream memory = new();
            bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
            memory.Position = 0;
            BitmapImage bitmapimage = new BitmapImage();
            bitmapimage.BeginInit();
            bitmapimage.StreamSource = memory;
            bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapimage.EndInit();
            return bitmapimage;
        }

        /// <summary>
        /// BitmapImage 객체를 Bitmap 객체로 변환합니다.
        /// </summary>
        public static Bitmap BitmapImageToBitmap(BitmapImage bitmapImage)
        {
            using MemoryStream outStream = new();
            BitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            encoder.Save(outStream);
            return new Bitmap(outStream);
        }

        public static Bitmap BitmapSourceToBitmap(BitmapSource bitmapSource)
        {
            Bitmap bitmap;
            bitmap = new Bitmap(bitmapSource.PixelWidth, bitmapSource.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb); // Create a new Bitmap of the same size and format as the BitmapSource
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat); // Lock the Bitmap's bits
            bitmapSource.CopyPixels(System.Windows.Int32Rect.Empty, bitmapData.Scan0, bitmapData.Height * bitmapData.Stride, bitmapData.Stride);// Copy the BitmapSource's pixel data to the Bitmap's bits
            bitmap.UnlockBits(bitmapData); // Unlock the Bitmap's bits
            return bitmap;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSpliter.Common
{
    public class Crop
    {
        public static Bitmap CropImage(Bitmap sourceImage)
        {
            // Find the boundaries of the non-white region
            int left = 0, top = 0, right = sourceImage.Width - 1, bottom = sourceImage.Height - 1;
            for (int x = 0; x < sourceImage.Width; x++)
            {
                for (int y = 0; y < sourceImage.Height; y++)
                {
                    Color pixelColor = sourceImage.GetPixel(x, y);
                    if (pixelColor.R < 240 || pixelColor.G < 240 || pixelColor.B < 240)
                    {
                        left = Math.Max(left, x);
                        top = Math.Max(top, y);
                        right = Math.Min(right, x);
                        bottom = Math.Min(bottom, y);
                    }
                }
            }

            // Create a new image with the dimensions of the cropped region
            int newWidth = right - left + 1;
            int newHeight = bottom - top + 1;
            Bitmap croppedImage = new Bitmap(newWidth, newHeight);

            // Copy the pixels from the cropped region to the new image
            using (Graphics g = Graphics.FromImage(croppedImage))
            {
                g.DrawImage(sourceImage, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(left, top, newWidth, newHeight), GraphicsUnit.Pixel);
            }

            return croppedImage;
        }
    }
}

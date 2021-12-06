using FourierTransform.Imaging.Models;
using FourierTransform.Imaging.Services.Base;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace FourierTransform.Imaging.Services
{
    public class BitmapFactory : IBitmapFactory
    {
        private readonly IProgress<ProgressInfo> _progress;

        public BitmapFactory(IProgress<ProgressInfo> progress)
        {
            _progress = progress;
        }
        
        public Bitmap Create(NormalizedColor[][] colors, PixelFormat pixelFormat)
        {
            _progress?.Report(CreateInfo(0));

            Bitmap bitmap = new(colors[0].Length, colors.Length, pixelFormat);

            var data = bitmap.LockBits(new Rectangle(0,
                                                     0,
                                                     bitmap.Width,
                                                     bitmap.Height),
                                       ImageLockMode.WriteOnly,
                                       bitmap.PixelFormat);

            SetPixels(colors, data, bitmap);

            bitmap.UnlockBits(data);

            return bitmap;
        }

        private ProgressInfo CreateInfo(double percentage)
            => new()
            {
                Stage = "Writing Image...",
                Percentage = percentage,
            };

        private void SetPixels(NormalizedColor[][] colors, BitmapData bitmapData, Bitmap bitmap)
        {
            IntPtr ptr = bitmapData.Scan0;

            int bytes = Math.Abs(bitmapData.Stride) * bitmap.Height;
            byte[] rgbValues = new byte[bytes];

            for (int y = 0, counter = 0; y < colors.Length; y++)
            {
                for (int x = 0; x < colors[y].Length; x++, counter += 3)
                {
                    rgbValues[counter] = (byte)(colors[y][x].R * 255);
                    rgbValues[counter + 1] = (byte)(colors[y][x].G * 255);
                    rgbValues[counter + 2] = (byte)(colors[y][x].B * 255);
                }

                double percentage = y / (bitmap.Height * 1.0);

                _progress?.Report(CreateInfo(percentage * 100));
            }

            Marshal.Copy(rgbValues, 0, ptr, bytes);
        }
    }
}

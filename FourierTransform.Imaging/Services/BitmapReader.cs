using FourierTransform.Imaging.Models;
using FourierTransform.Imaging.Services.Base;
using Ninject;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace FourierTransform.Imaging.Services
{
    public class BitmapReader : IBitmapReader
    {
        private readonly IProgress<ProgressInfo> _progress;

        [Inject]
        public BitmapReader(IProgress<ProgressInfo> progress)
        {
            _progress = progress;
        }

        public NormalizedColor[][] Read(Bitmap bitmap)
        {
            BitmapData data 
                = bitmap.LockBits(new Rectangle(0,
                                                0,
                                                bitmap.Width,
                                                bitmap.Height),
                                  ImageLockMode.ReadOnly,
                                  bitmap.PixelFormat);

            var colors = SetPixels(data, bitmap);

            bitmap.UnlockBits(data);

            return colors;
        }

        private NormalizedColor[][] SetPixels(BitmapData bitmapData, Bitmap bitmap)
        {
            NormalizedColor[][] colors = new NormalizedColor[bitmapData.Height][];

            IntPtr firstPixel = bitmapData.Scan0;

            int bytes = Math.Abs(bitmapData.Stride) * bitmap.Height;
            byte[] rgbValues = new byte[bytes];

            Marshal.Copy(firstPixel, rgbValues, 0, bytes);

            for (int y = 0, t = 0; y < bitmap.Height; y++)
            {
                colors[y] = new NormalizedColor[bitmap.Width];
                for (int x = 0; x < bitmap.Width; x++, t += 3)
                    colors[y][x] = CreateColor(rgbValues[t],
                                               rgbValues[t + 1],
                                               rgbValues[t + 2]);

                double percentage = y / (bitmap.Height * 1.0);

                _progress?.Report(CreateInfo(percentage * 100));
            }

            return colors;
        }

        private NormalizedColor CreateColor(byte r, byte g, byte b)
            => new ()
            {
                R = r / 255.0,
                G = g / 255.0,
                B = b / 255.0
            };

        private ProgressInfo CreateInfo(double percentage)
            => new ()
            {
                Stage = "Reading Image...",
                Percentage = percentage,
            };
    }
}

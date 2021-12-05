using FourierTransfrom.Services.Base;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FourierTransfrom.Services
{
    internal class BitmapConverter : IBitmapConverter
    {
        public BitmapSource ToBitmapSource(Bitmap bitmap)
        {
            PixelFormat format = PixelFormats.Bgr24;

            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            var bitmapSource = BitmapSource.Create(
                bitmapData.Width, bitmapData.Height,
                bitmap.HorizontalResolution, bitmap.VerticalResolution,
                format, null,
                bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

            bitmap.UnlockBits(bitmapData);

            return bitmapSource;
        }
    }
}

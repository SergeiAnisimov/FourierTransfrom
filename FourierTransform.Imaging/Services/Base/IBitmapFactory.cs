using FourierTransform.Imaging.Models;
using System.Drawing;
using System.Drawing.Imaging;

namespace FourierTransform.Imaging.Services.Base
{
    public interface IBitmapFactory
    {
        Bitmap Create(NormalizedColor[][] colors, PixelFormat pixelFormat);
    }
}

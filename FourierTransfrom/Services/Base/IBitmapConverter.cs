using System.Drawing;
using System.Windows.Media.Imaging;

namespace FourierTransfrom.Services.Base
{
    internal interface IBitmapConverter
    {
        BitmapSource ToBitmapSource(Bitmap bitmap);
    }
}

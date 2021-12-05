using System.Drawing;

namespace FourierTransform.Imaging.Services.Base
{
    internal interface IFileReader
    {
        Bitmap Read(string path);
    }
}

using System.Drawing;

namespace FourierTransform.Imaging.Services.Base
{
    public interface IFileReader
    {
        Bitmap Read(string path);
    }
}

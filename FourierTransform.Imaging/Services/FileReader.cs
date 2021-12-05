using FourierTransform.Imaging.Services.Base;
using System.Drawing;

namespace FourierTransform.Imaging.Services
{
    internal class FileReader : IFileReader
    {
        public Bitmap Read(string path)
            => new Bitmap(path);
    }
}

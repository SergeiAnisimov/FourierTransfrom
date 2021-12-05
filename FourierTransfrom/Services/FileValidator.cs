using FourierTransfrom.Services.Base;
using System.IO;

namespace FourierTransfrom.Services
{
    internal class FileValidator : IFileValidator
    {
        private readonly string _formats = ".jpg.jpeg.png";
        
        public bool ValidatePath(string path)
            => _formats.Contains(Path.GetExtension(path));
    }
}

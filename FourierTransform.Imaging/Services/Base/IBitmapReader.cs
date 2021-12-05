using FourierTransform.Imaging.Models;
using System.Drawing;

namespace FourierTransform.Imaging.Services.Base
{
    /// <summary>
    /// Reads all pixel in bitmap memory
    /// </summary>
    public interface IBitmapReader
    {
        /// <summary>
        /// Locks bitmap bits, than read all pixel from memory
        /// </summary>
        /// <param name="bitmap">
        /// Image to read
        /// </param>
        /// <returns>
        /// Normalized colors of bitmap pixels
        /// </returns>
        NormalizedColor[][] Read(Bitmap bitmap);
    }
}

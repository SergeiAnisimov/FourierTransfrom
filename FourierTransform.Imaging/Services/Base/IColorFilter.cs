using FourierTransform.Imaging.Models;

namespace FourierTransform.Imaging.Services.Base
{
    public interface IColorFilter
    {
        /// <summary>
        /// Applies linear filtration
        /// </summary>
        /// <param name="colors">
        /// Input colors
        /// </param>
        /// <param name="filter">
        /// Filter kernel
        /// </param>
        /// <returns>
        /// Filtrated colors
        /// </returns>
        NormalizedColor[][] Filter(NormalizedColor[][] colors, Func<int, int, double> filter);
    }
}

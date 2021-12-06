using FourierTransform.Imaging.Models;
using FourierTransform.Imaging.Services.Base;

namespace FourierTransform.Imaging.Services
{
    public class ColorFilter : IColorFilter
    {
        public NormalizedColor[][] Filter(NormalizedColor[][] colors, Func<int, int, double> filter)
        {
            return colors;
        }
    }
}

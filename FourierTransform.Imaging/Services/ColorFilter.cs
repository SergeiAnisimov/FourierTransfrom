using FourierTransform.Imaging.Models;
using FourierTransform.Imaging.Services.Base;
using Ninject;

namespace FourierTransform.Imaging.Services
{
    public class ColorFilter : IColorFilter
    {
        private readonly IProgress<ProgressInfo> _progress;

        [Inject]
        public ColorFilter(IProgress<ProgressInfo> progress)
        {
            _progress = progress;
        }

        public NormalizedColor[][] Filter(NormalizedColor[][] colors, Func<int, int, double> filter)
        {
            _progress?.Report(CreateInfo(0));

            NormalizedColor[][] fColors = new NormalizedColor[colors.Length][];

            for (int y = 0; y < colors.Length; y++)
            {
                fColors[y] = new NormalizedColor[colors[y].Length];

                for (int x = 0; x < colors[y].Length; x++)
                    fColors[y][x] = Convolution(x, y, colors, filter);

                double percentage = y / (colors.Length * 1.0);
                
                _progress?.Report(CreateInfo(percentage * 100));
            }
            
            return fColors;
        }

        public NormalizedColor Convolution(int x, int y, NormalizedColor[][] colors, Func<int, int, double> filter)
        {
            int lowX = x == 0 ? x : x - 1;
            int highX = x == colors[y].Length - 1 ? x : x + 1;

            int lowY = y == 0 ? y : y - 1;
            int highY = y == colors.Length - 1 ? y : y + 1;

            var sum = NormalizedColor.Zero;

            for (int i = lowY, t = 0; i <= highY; i++, t++)
            {    
                for (int j = lowX, l = 0; j <= highX; j++, l++)
                {
                    sum += filter(t, l) * colors[i][j];
                }
            }

            return sum;
        }

        private ProgressInfo CreateInfo(double percentage)
            => new()
            {
                Stage = "Appling filter...",
                Percentage = percentage,
            };
    }
}

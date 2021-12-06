using System.ComponentModel.DataAnnotations;

namespace FourierTransform.Imaging.Models
{
    /// <summary>
    /// Represents rgb color scheme
    /// with componenets between 0 and 1
    /// </summary>
    public struct NormalizedColor
    {
        [Range(0, 1)]
        public double R { get; init; }

        [Range(0, 1)]
        public double G { get; init; }

        [Range(0, 1)]
        public double B { get; init; }

        public override string ToString() => $"R : {R} ; G : {G} ; B : {B}";

        public static NormalizedColor operator* (double k, NormalizedColor a)
            => new NormalizedColor
            {
                R = k * a.R,
                G = k * a.G,
                B = k * a.B
            };

        public static NormalizedColor operator +(NormalizedColor a, NormalizedColor b)
            => new NormalizedColor
            {
                R = b.R + a.R,
                G = b.G + a.G,
                B = b.B + a.B
            };

        public static NormalizedColor Zero
            => new NormalizedColor { R = 0, B = 0, G = 0 };
    }
}

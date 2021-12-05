using System.ComponentModel.DataAnnotations;

namespace FourierTransform.Imaging.Models
{
    /// <summary>
    /// Represents rgb color scheme
    /// with componenets between 0 and 1
    /// </summary>
    internal struct NormalizedColor
    {
        [Range(0, 1)]
        public double R { get; init; }

        [Range(0, 1)]
        public double G { get; init; }

        [Range(0, 1)]
        public double B { get; init; }

        public override string ToString() => $"R : {R} ; G : {G} ; B : {B}";
    }
}

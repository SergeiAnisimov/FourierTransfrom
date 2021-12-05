using FourierTransfrom.Services.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FourierTransform.Imaging.Services.Base;
using Ninject;
using FourierTransform.Imaging.Models;

namespace FourierTransfrom.Services
{
    internal class FilterConveyor : IFilterConveyor
    {
        private readonly IFileReader _reader;

        private readonly IColorFilter _colorFilter;

        private readonly IBitmapReader _bitmapReader;

        private readonly IBitmapFactory _bitmapFactory;

        private IEnumerable<Func<int, int, double>> _filters
            = Enumerable.Empty<Func<int, int, double>>();

        [Inject]
        public FilterConveyor(IFileReader fileReader,
                              IColorFilter colorFilter,
                              IBitmapReader bitmapReader,
                              IBitmapFactory bitmapFactory)
        {
            _reader = fileReader;
            _colorFilter = colorFilter;
            _bitmapReader = bitmapReader;
            _bitmapFactory = bitmapFactory;
        }
        
        public void AddFilter(Func<int, int, double> fiterKernel)
            => _filters = _filters.Append(fiterKernel);

        public void ClearFilters()
            => _filters = Enumerable.Empty<Func<int, int, double>>();

        public Bitmap Filter(string pathToImage)
        {
            Bitmap bitmap = _reader.Read(pathToImage);

            NormalizedColor[][] colors = _bitmapReader.Read(bitmap);

            foreach (var filter in _filters)
                colors = _colorFilter.Filter(colors, filter);
            
            return _bitmapFactory.Create(colors, bitmap.PixelFormat);
        }
    }
}

using System;
using System.Drawing;

namespace FourierTransfrom.Services.Base
{
    internal interface IFilterConveyor
    {
        void AddFilter(Func<int, int, double> fiterKernel);

        void ClearFilters();

        bool IsEmpty { get; }
        
        Bitmap Filter(string pathToImage);
    }
}

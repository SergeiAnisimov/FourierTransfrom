using FourierTransfrom.Models;
using System;

namespace FourierTransfrom.Services.Base
{
    internal interface IFilterFactory
    {
        Func<int, int, double> Get(Filter filter);
    }
}

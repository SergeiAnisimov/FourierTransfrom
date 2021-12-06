using FourierTransfrom.Models;
using FourierTransfrom.Services.Base;
using System;

namespace FourierTransfrom.Services
{
    internal class FilterFactory : IFilterFactory
    {
        private readonly double[][] MContr1 = { new double[] { 0, -1, 0 },
                                               new double[] { -1, 5, -1 },
                                               new double[] { 0, -1, 0 } };

        private readonly double[][] MContr2 = { new double[] { -1, -1, -1 }, 
                                               new double[] { -1, 9, -1 }, 
                                               new double[] { -1, -1, -1 } };

        private readonly double[][] MDiff =  { new double[] { 0, 1, 0 },
                                               new double[] { 1, 4, 1 },
                                               new double[] { 0, 1, 0 } };

        private readonly double[][] Prewitt =  { new double[] { -1, 0, 1 },
                                                 new double[] { -1, 0, 1 },
                                                 new double[] { -1, 0, 1 } };

        private readonly double[][] Sobel =  { new double[] { -1, 0, 1 },
                                               new double[] { -2, 0, 2 },
                                               new double[] { -1, 0, 1 } };

        private readonly double[][] Analising2 =  { new double[] { 1, 1, 1 },
                                                    new double[] { 1, 2, 1 },
                                                    new double[] { 1, 1, 1 } };

        public Func<int, int, double> Get(Filter filter)
            => filter switch
            {
                Filter.Gauss => (i, j) => (1.0 / (2 * Math.PI) * Math.Exp(-(i * i + j * j) / 2)),
                Filter.Analising1 => (i, j) => 1 / 9.0,
                Filter.Analising2 => (i, j) => Analising2[i][j] / 10.0,
                Filter.Contr1 => (i, j) => MContr1[i][j],
                Filter.Contr2 => (i, j) => MContr2[i][j],
                Filter.Differential => (i, j) => MDiff[i][j],
                Filter.Prewitt => (i, j) => Prewitt[i][j],
                Filter.Sobel => (i, j) => Sobel[i][j],
                _ => throw new ArgumentException("How????")
            };
    }
}

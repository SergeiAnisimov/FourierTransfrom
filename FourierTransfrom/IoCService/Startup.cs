using FourierTransform.Imaging.Models;
using FourierTransform.Imaging.Services;
using FourierTransform.Imaging.Services.Base;
using FourierTransfrom.Services;
using FourierTransfrom.Services.Base;
using FourierTransfrom.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FourierTransfrom.IoCService
{
    internal class Startup
    {
        public IServiceCollection Services { get; }

        public Startup()
        {
            Services = new ServiceCollection().AddScoped<IFileReader, FileReader>()
                                              .AddScoped<IBitmapReader, BitmapReader>()
                                              .AddScoped<IBitmapFactory, BitmapFactory>()
                                              .AddScoped<IColorFilter, ColorFilter>()
                                              .AddScoped<IFilterFactory, FilterFactory>()
                                              .AddScoped<IFileValidator, FileValidator>()
                                              .AddScoped<IBitmapConverter, BitmapConverter>()
                                              .AddScoped<IFilterConveyor, FilterConveyor>()
                                              .AddScoped<ImageFilterViewModel>()
                                              .AddSingleton<IProgress<ProgressInfo>, Progress<ProgressInfo>>();
        }
    }
}

﻿using FourierTransform.Imaging.Models;
using FourierTransform.Imaging.Services;
using FourierTransform.Imaging.Services.Base;
using FourierTransfrom.Services;
using FourierTransfrom.Services.Base;
using FourierTransfrom.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using System;

namespace FourierTransfrom.IoCService
{
    internal class AppKernel
    {
        private readonly IKernel _kernel
            = new StandardKernel(new IoCModule());

        private readonly IServiceCollection _services;

        private readonly IServiceProvider _serviceProvider;

        public ImageFilterViewModel ImageFilterViewModel
            => _serviceProvider?.GetService<ImageFilterViewModel>();

        public AppKernel()
        {
            _services = new ServiceCollection().AddScoped<IFileReader, FileReader>()
                                              .AddScoped<IBitmapReader, BitmapReader>()
                                              .AddScoped<IBitmapFactory, BitmapFactory>()
                                              .AddScoped<IColorFilter, ColorFilter>()
                                              .AddScoped<IFilterFactory, FilterFactory>()
                                              .AddScoped<IFileValidator, FileValidator>()
                                              .AddScoped<IBitmapConverter, BitmapConverter>()
                                              .AddScoped<IFilterConveyor, FilterConveyor>()
                                              .AddScoped<ImageFilterViewModel>()
                                              .AddSingleton<IProgress<ProgressInfo>, Progress<ProgressInfo>>();

            _serviceProvider = _services.BuildServiceProvider();
        }
    }
}

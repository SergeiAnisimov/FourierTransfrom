using FourierTransform.Imaging.Models;
using FourierTransform.Imaging.Services;
using FourierTransform.Imaging.Services.Base;
using FourierTransfrom.Services;
using FourierTransfrom.Services.Base;
using Ninject.Modules;
using System;

namespace FourierTransfrom.IoCService
{
    internal class IoCModule : NinjectModule
    {
        public override void Load()
        {            
            Bind<IProgress<ProgressInfo>>().To<Progress<ProgressInfo>>().InSingletonScope();

            Bind<IFileReader>().To<FileReader>().InTransientScope();
            Bind<IBitmapReader>().To<BitmapReader>().InTransientScope();
            Bind<IBitmapFactory>().To<BitmapFactory>().InTransientScope();
            Bind<IColorFilter>().To<ColorFilter>().InTransientScope();
            
            Bind<IFileValidator>().To<FileValidator>().InTransientScope();
            Bind<IBitmapConverter>().To<BitmapConverter>().InTransientScope();
            Bind<IFilterConveyor>().To<FilterConveyor>().InTransientScope();
        }
    }
}

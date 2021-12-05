using FourierTransfrom.Services;
using FourierTransfrom.Services.Base;
using Ninject.Modules;

namespace FourierTransfrom.IoCService
{
    internal class IoCModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IFileValidator>().To<FileValidator>().InTransientScope();
            Bind<IBitmapConverter>().To<BitmapConverter>().InTransientScope();
        }
    }
}

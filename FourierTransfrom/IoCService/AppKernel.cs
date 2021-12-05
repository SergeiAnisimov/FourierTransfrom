using FourierTransfrom.ViewModels;
using Ninject;

namespace FourierTransfrom.IoCService
{
    internal class AppKernel
    {
        private readonly IKernel _kernel
            = new StandardKernel(new IoCModule());

        public ImageFilterViewModel ImageFilterViewModel
            => _kernel.Get<ImageFilterViewModel>();
    }
}

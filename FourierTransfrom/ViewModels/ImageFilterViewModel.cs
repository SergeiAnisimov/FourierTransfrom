using FourierTransfrom.Commands;
using FourierTransfrom.Models;
using FourierTransfrom.Services.Base;
using FourierTransfrom.ViewModels.Base;
using Ninject;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace FourierTransfrom.ViewModels
{
    internal class ImageFilterViewModel : ViewModel
    {
        #region Fields

        // Check if droped file is image
        private readonly IFileValidator _validator;

        // Creates new BitmapSource from given bitmap
        private readonly IBitmapConverter _converter;

        // Applies filters to image colors
        private readonly IFilterConveyor _filterConveyor;

        #endregion Fields

        [Inject]
        public ImageFilterViewModel(IFileValidator validator,
                                    IBitmapConverter converter,
                                    IFilterConveyor filterConveyor)
        {
            _validator = validator;
            _converter = converter;
            _filterConveyor = filterConveyor;

            Status = "Inited";

            DropCommand = new Command(OnDropCommandExecuting);
        }

        #region Props

        #region Status

        private string _status = "";

        public string Status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        #endregion Status

        #region ProgressValue

        private double _progressValue = 0;

        public double ProgressValue
        {
            get => _progressValue;
            set => Set(ref _progressValue, value);
        }

        #endregion ProgressValue

        #region Filters

        private string[] _filters = Enum.GetNames<Filter>();

        public string[] Filters
        {
            get => _filters;
            set => Set(ref _filters, value);
        }

        #endregion Filters

        #region Input

        private BitmapSource _input;

        public BitmapSource Input
        {
            get => _input;
            set
            {
                ProgressValue = 0;
                if(Set(ref _input, value))
                {
                    Status = "Image Loaded";
                    ProgressValue = 100;
                }
            }
        }

        #endregion Input

        #region Output

        private BitmapSource _output;

        public BitmapSource Output
        {
            get => _output;
            set => Set(ref _output, value);
        }

        #endregion Output

        #endregion Props

        #region Commands

        public ICommand DropCommand { get; }

        private void OnDropCommandExecuting(object p)
        {
            if (p is DragEventArgs e)
            {
                string path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];

                if (!_validator.ValidatePath(path))
                    return;
                
                Input = new BitmapImage(new Uri(path, UriKind.Absolute));
            }
        }

        #endregion Commands
    }
}

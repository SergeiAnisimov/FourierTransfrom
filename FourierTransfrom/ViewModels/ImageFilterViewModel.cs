using FourierTransform.Imaging.Models;
using FourierTransfrom.Commands;
using FourierTransfrom.Models;
using FourierTransfrom.Services.Base;
using FourierTransfrom.ViewModels.Base;
using Ninject;
using System;
using System.Threading.Tasks;
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

        // GEts funcs
        private readonly IFilterFactory _filterFactory;

        private string _pathToImage = "";

        #endregion Fields

        [Inject]
        public ImageFilterViewModel(IFileValidator validator,
                                    IBitmapConverter converter,
                                    IFilterConveyor filterConveyor,
                                    IFilterFactory filterFactory,
                                    IProgress<ProgressInfo> progress)
        {
            _validator = validator;
            _converter = converter;
            _filterConveyor = filterConveyor;
            _filterFactory = filterFactory;

            Status = "Inited";

            DropCommand = new Command(OnDropCommandExecuting);
            AddFilterCommand = new Command(OnAddFilterCommandExecuting);
            FiltrateCommand = new Command(OnFiltrateCommandExecuting,
                                          CanFiltrateCommandExecute);
            CloseCommand = new Command(OnCloseCommandExecuting);
            MinimizeCommand = new Command(OnMinimizeCommandExecuting);
            ClearFiltersCommand = new Command(OnClearFiltersCommandExecuting, CanClearFiltersCommandExecute);

            ((Progress<ProgressInfo>)progress).ProgressChanged += ProgressChanged;
        }

        private void ProgressChanged(object? sender, ProgressInfo e)
        {
            ProgressValue = e.Percentage;
            Status = e.Stage;
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

        #region Pipeline

        private string _pipeline = "";

        public string Pipeline
        {
            get => _pipeline;
            set => Set(ref _pipeline, value);
        }

        #endregion Pipeline

        #endregion Props

        #region Commands

        #region Drop Command

        public ICommand DropCommand { get; }

        private void OnDropCommandExecuting(object p)
        {
            if (p is DragEventArgs e)
            {
                string path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];

                if (!_validator.ValidatePath(path))
                    return;
                
                Input = new BitmapImage(new Uri(path, UriKind.Absolute));
                
                _pathToImage = path;
            }
        }

        #endregion Drop Command

        #region Add Filter Command

        public ICommand AddFilterCommand { get; }

        private void OnAddFilterCommandExecuting(object p)
        {
            if (p is string filterName)
            {
                if(Enum.TryParse(filterName, true, out Filter filter))
                {
                    _filterConveyor.AddFilter(_filterFactory.Get(filter));
                    Status = $"{filterName} filter added";

                    Pipeline += Pipeline == "" ? filterName : $" -> {filterName}";
                }
            }
        }

        #endregion Add Filter Command

        #region Filtrate Command

        public ICommand FiltrateCommand { get; }

        private async void OnFiltrateCommandExecuting(object p)
        {
            var bitmap = await Task.Run(() => _filterConveyor.Filter(_pathToImage));

            Output = _converter.ToBitmapSource(bitmap);

            Status = "Image Filtrated";
        }

        private bool CanFiltrateCommandExecute(object p)
            => !_filterConveyor.IsEmpty && !string.IsNullOrEmpty(_pathToImage);

        #endregion Filtrate Command

        #region Clear Filters Command

        public ICommand ClearFiltersCommand { get; }

        private void OnClearFiltersCommandExecuting(object p)
        {
            _filterConveyor.ClearFilters();
            Status = "Filters Cleared";
            Pipeline = "";
        }

        private bool CanClearFiltersCommandExecute(object p)
            => !_filterConveyor.IsEmpty;

        #endregion Clear Filters Command

        #region Close Command

        public ICommand CloseCommand { get; }

        private void OnCloseCommandExecuting(object p)
        {
            if (p is Window window)
            {
                window.Close();
            }
        }

        #endregion Close Command

        #region Minimize Command

        public ICommand MinimizeCommand { get; }

        private void OnMinimizeCommandExecuting(object p)
        {
            if (p is Window window)
            {
                window.WindowState = window.WindowState == WindowState.Normal ? WindowState.Minimized : WindowState.Normal;
            }
        }

        #endregion Minimize Command

        #endregion Commands
    }
}

using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Wrox.ProCSharp.Composition
{
    public enum ExtensionChange
    {
        Added,
        Removed
    }
    public class ActivatedExtensionEventArgs : EventArgs
    {
        public ExtensionChange ExtensionChange { get; set; }
    }

    public delegate void ExtensionHandler(ExtensionViewModel extension, ActivatedExtensionEventArgs e);

    public class ExtensionViewModel : BindableBase
    {
        public ExtensionViewModel(Lazy<ICalculatorExtension, CalculatorExtensionMetadataAttribute> extension)
        {
            ActivateCommand = new DelegateCommand(OnActivate);
            CloseCommand = new DelegateCommand(OnClose);
            Extension = extension;
        }

        public event ExtensionHandler ActivatedExtensionChanged;


        public DelegateCommand ActivateCommand { get; }
        public DelegateCommand CloseCommand { get; }
        public Lazy<ICalculatorExtension, CalculatorExtensionMetadataAttribute> Extension { get; }

        public void OnActivate()
        {
            UI = Extension.Value.UI;
            ActivatedExtensionChanged?.Invoke(this, new ActivatedExtensionEventArgs { ExtensionChange = ExtensionChange.Added });
        }

        public void OnClose()
        {
            ActivatedExtensionChanged?.Invoke(this, new ActivatedExtensionEventArgs { ExtensionChange = ExtensionChange.Removed });
        }

        private object _ui;
        public object UI
        {
            get { return _ui; }
            set { SetProperty(ref _ui, value); }
        }

    }

    public class CalculatorExtensionsViewModel : BindableBase
    {
        public CalculatorExtensionsViewModel()
        {
            _calculatorExtensionsManager = new CalculatorExtensionsManager();
            _calculatorExtensionsManager.ImportsSatisfied += (sender, e) =>
            {
                Status += $"{e.StatusMessage}\n";
            };
        }
        public void Init(params Type[] parts)
        {
            _calculatorExtensionsManager.InitializeContainer(parts);

            foreach (var extension in _calculatorExtensionsManager.GetExtensionInformation())
            {
                var vm = new ExtensionViewModel(extension);
                vm.ActivatedExtensionChanged += OnActivatedExtensionChanged;
                Extensions.Add(vm);
            }
        }

        private void OnActivatedExtensionChanged(ExtensionViewModel extension, ActivatedExtensionEventArgs e)
        {
            switch (e.ExtensionChange)
            {
                case ExtensionChange.Added:
                    ActivatedExtensions.Add(extension);
                    SelectedExtension = extension;
                    break;
                case ExtensionChange.Removed:
                    ActivatedExtensions.Remove(extension);
                    SelectedExtension = ActivatedExtensions.FirstOrDefault();
                    break;
                default:
                    break;
            }
        }

        private string _status;

        public string Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        private ExtensionViewModel _selectedExtension;
        public ExtensionViewModel SelectedExtension
        {
            get { return _selectedExtension; }
            set { SetProperty(ref _selectedExtension, value); }
        }

        private void OnActivateExtension(ExtensionViewModel sender, EventArgs e)
        {
            ActivatedExtensions.Add(sender);
            SelectedExtension = sender;
        }


        private CalculatorExtensionsManager _calculatorExtensionsManager;

        public ObservableCollection<ExtensionViewModel> Extensions { get; } = new ObservableCollection<ExtensionViewModel>();

        public ObservableCollection<ExtensionViewModel> ActivatedExtensions { get; } = new ObservableCollection<ExtensionViewModel>();
    }
}

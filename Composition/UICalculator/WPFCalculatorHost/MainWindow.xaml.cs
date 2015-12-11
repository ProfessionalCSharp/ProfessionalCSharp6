using System.Windows;
using System.Windows.Controls;
using Wrox.ProCSharp.Composition;

namespace WPFCalculatorHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            CalculatorViewModel = new CalculatorViewModel();
            CalculatorViewModel.Init(typeof(Calculator), typeof(SubtractOperation), typeof(SlowAddOperation), typeof(AddOperation));

            CalculatorExtensionsViewModel = new CalculatorExtensionsViewModel();
            CalculatorExtensionsViewModel.Init(typeof(FuelCalculatorExtension), typeof(TemperatureConversionExtension));
        }

        public CalculatorViewModel CalculatorViewModel { get; }

        private void OnNumberClick(object sender, RoutedEventArgs e)
        {
            var b = e.Source as Button;
            if (b != null)
            {
                CalculatorViewModel.Input += b.Content.ToString();
            }
        }

        private void OnDefineOperation(object sender, RoutedEventArgs e)
        {
            var b = e.Source as Button;
            if (b != null)
            {
                CalculatorViewModel.CurrentOperation = b.Tag as IOperation;
            }
        }

        public CalculatorExtensionsViewModel CalculatorExtensionsViewModel { get; }
    }
}

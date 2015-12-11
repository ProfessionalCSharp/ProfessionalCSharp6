using System.Windows.Controls;
using Wrox.ProCSharp.Composition;

namespace FuelEconomyWPF
{
    /// <summary>
    /// Interaction logic for FuelEconomyUC.xaml
    /// </summary>
    public partial class FuelEconomyUC : UserControl
    {
        public FuelEconomyUC()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public FuelEconomyViewModel ViewModel { get; } = new FuelEconomyViewModel();

    }
}

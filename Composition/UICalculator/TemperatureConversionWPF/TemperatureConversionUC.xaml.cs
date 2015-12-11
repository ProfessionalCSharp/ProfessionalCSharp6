using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wrox.ProCSharp.Composition;

namespace TemperatureConversionWPF
{
    /// <summary>
    /// Interaction logic for TemperatureConversionUC.xaml
    /// </summary>
    public partial class TemperatureConversionUC : UserControl
    {
        public TemperatureConversionUC()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public TemperatureConversionViewModel ViewModel { get;  } = new TemperatureConversionViewModel();
    }
}

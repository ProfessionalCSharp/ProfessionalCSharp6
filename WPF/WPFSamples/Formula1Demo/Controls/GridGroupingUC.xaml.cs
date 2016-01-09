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

namespace Formula1Demo.Controls
{
    /// <summary>
    /// Interaction logic for GridGroupingUC.xaml
    /// </summary>
    public partial class GridGroupingUC : UserControl
    {
        public GridGroupingUC()
        {
            InitializeComponent();
        }

        private void OnGetPage(object sender, RoutedEventArgs e)
        {
            int page = int.Parse(textPageNumber.Text);
            var odp = (sender as FrameworkElement).FindResource("races") as ObjectDataProvider;
            odp.MethodParameters[0] = page;
            odp.Refresh();
        }
    }
}

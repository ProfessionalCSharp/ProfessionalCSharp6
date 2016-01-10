using Formula1Demo.Controls;
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

namespace Formula1Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (container != null)
            {
                container.Child = null;
                switch ((sender as ComboBox).SelectedIndex)
                {
                    case 1:
                        container.Child = new TreeUC();
                        break;
                    case 2:
                        container.Child = new GridUC();
                        break;
                    case 3:
                        container.Child = new GridCustomUC();
                        break;
                    case 4:
                        container.Child = new GridGroupingUC();
                        break;
                    default:
                        break;
                }
            }

        }
    }
}

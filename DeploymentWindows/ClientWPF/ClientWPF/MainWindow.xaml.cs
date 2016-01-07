using AppSupport;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
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

namespace ClientWPF
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


        private void OnGetDate(object sender, RoutedEventArgs e)
        {
            var dateService = new DateService();
            text1.Text = dateService.GetLongDateInfoString();
            text2.Text = dateService.GetShortDateInfoString();
        }

        private void OnUpdate(object sender, RoutedEventArgs e)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment.CurrentDeployment.CheckForUpdateCompleted +=
                  (sender1, e1) =>
                  {
                      if (e1.UpdateAvailable)
                      {
                          ApplicationDeployment.CurrentDeployment.UpdateCompleted +=
                    (sender2, e2) =>
                        {
                            MessageBox.Show("Update completed");
                        };
                          ApplicationDeployment.CurrentDeployment.UpdateAsync();
                      }
                      else
                      {
                          MessageBox.Show("No update available");
                      }

                  };
                ApplicationDeployment.CurrentDeployment.CheckForUpdateAsync();
            }
            else
            {
                MessageBox.Show("not a ClickOnce installation");
            }

        }
    }
}

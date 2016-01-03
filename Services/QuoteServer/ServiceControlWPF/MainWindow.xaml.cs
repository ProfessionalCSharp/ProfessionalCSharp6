using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
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

namespace ServiceControlWPF
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      RefreshServiceList();
    }

    private void OnServiceCommand(object sender, RoutedEventArgs e)
    {
      Cursor oldCursor = this.Cursor;
      try
      {
        this.Cursor = Cursors.Wait;
        ButtonState currentButtonState = (ButtonState)(sender as Button).Tag;
        var si = listBoxServices.SelectedItem as ServiceControllerInfo;
        if (currentButtonState == ButtonState.Start)
        {
          si.Controller.Start();
          si.Controller.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
        }
        else if (currentButtonState == ButtonState.Stop)
        {
          si.Controller.Stop();
          si.Controller.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
        }
        else if (currentButtonState == ButtonState.Pause)
        {
          si.Controller.Pause();
          si.Controller.WaitForStatus(ServiceControllerStatus.Paused, TimeSpan.FromSeconds(10));
        }
        else if (currentButtonState == ButtonState.Continue)
        {
          si.Controller.Continue();
          si.Controller.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
        }
        int index = listBoxServices.SelectedIndex;
        RefreshServiceList();
        listBoxServices.SelectedIndex = index;
      }
      catch (System.ServiceProcess.TimeoutException ex)
      {
        MessageBox.Show(ex.Message, "Timeout Service Controller", MessageBoxButton.OK, MessageBoxImage.Error);
      }
      catch (InvalidOperationException ex)
      {
        MessageBox.Show(String.Format("{0} {1}", ex.Message, ex.InnerException != null ? ex.InnerException.Message : String.Empty),
            "Error Service Controller", MessageBoxButton.OK, MessageBoxImage.Error);
      }
      finally
      {
        this.Cursor = oldCursor;
      }
    }

    protected void RefreshServiceList()
    {
      this.DataContext = ServiceController.GetServices().
        OrderBy(sc => sc.DisplayName).
        Select(sc => new ServiceControllerInfo(sc));
    }

    private void OnExit(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

    private void OnRefresh(object sender, RoutedEventArgs e) => RefreshServiceList();
  }
}

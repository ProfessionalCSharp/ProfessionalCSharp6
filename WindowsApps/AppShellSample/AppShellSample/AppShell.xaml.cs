using AppShellSample.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AppShellSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppShell : Page
    {
        public AppShell()
        {
            this.InitializeComponent();
        }

        public Frame AppFrame => frame;

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            bool handled = false;
            BackRequested(ref handled);
        }

        private void BackRequested(ref bool handled)
        {
            if (AppFrame?.CanGoBack ?? false && !handled)
            {
                handled = true;
                AppFrame.GoBack();
            }
        }

        private void OnNavigatingToPage(object sender, NavigatingCancelEventArgs e)
        {

        }

        private void OnNavigatedToPage(object sender, NavigationEventArgs e)
        {

        }

        private void HamburgerMenu_UnChecked(object sender, RoutedEventArgs e)
        {

        }

        public void GoToHomePage()
        {
            while (AppFrame?.CanGoBack ?? false) AppFrame.GoBack();
        }

        public void GoToEditPage()
        {            
            AppFrame?.Navigate(typeof(EditPage));
        }
    }
}

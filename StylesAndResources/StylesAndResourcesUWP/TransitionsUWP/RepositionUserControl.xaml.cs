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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace TransitionsUWP
{
    public sealed partial class RepositionUserControl : UserControl
    {
        public RepositionUserControl()
        {
            this.InitializeComponent();
        }

        private void OnReposition(object sender, RoutedEventArgs e)
        {
            buttonReposition.Margin = new Thickness(100);
            button2.Margin = new Thickness(100);
        }

        private void OnReset(object sender, RoutedEventArgs e)
        {
            buttonReposition.Margin = new Thickness(10);
            button2.Margin = new Thickness(10);
        }
    }
}

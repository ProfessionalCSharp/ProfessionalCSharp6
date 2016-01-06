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
    public sealed partial class PaneTransitionUserControl : UserControl
    {
        public PaneTransitionUserControl()
        {
            this.InitializeComponent();
        }

        private void OnShow(object sender, RoutedEventArgs e)
        {
            popup1.IsOpen = true;
            popup2.IsOpen = true;
            popup3.IsOpen = true;
        }

        private void OnHide(object sender, RoutedEventArgs e)
        {
            popup1.IsOpen = false;
            popup2.IsOpen = false;
            popup3.IsOpen = false;
        }
    }
}

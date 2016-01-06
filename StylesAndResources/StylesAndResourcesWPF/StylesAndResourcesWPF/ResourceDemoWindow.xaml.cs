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
using System.Windows.Shapes;

namespace StylesAndResourcesWPF
{
    /// <summary>
    /// Interaction logic for ResourceDemoWindow.xaml
    /// </summary>
    public partial class ResourceDemoWindow : Window
    {
        public ResourceDemoWindow()
        {
            InitializeComponent();
        }

        private void OnApplyResources(object sender, RoutedEventArgs e)
        {
            Control ctrl = sender as Control;
            ctrl.Background = ctrl.TryFindResource("MyGradientBrush") as Brush;

        }

        private void OnChangeDynamicResource(object sender, RoutedEventArgs e)
        {
            myContainer.Resources.Clear();
            var brush = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0, 1)
            };

            brush.GradientStops = new GradientStopCollection()
            {
                new GradientStop(Colors.White, 0.0),
                new GradientStop(Colors.Yellow, 0.14),
                new GradientStop(Colors.YellowGreen, 0.7)
            };
            myContainer.Resources.Add("MyGradientBrush", brush);

        }
    }
}

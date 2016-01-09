using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LayoutSamples.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VariableSizedWrapGridSample : Page
    {
        public VariableSizedWrapGridSample()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var r = new Random();
            Grid[] items =
                Enumerable.Range(0, 30).Select(i =>
                {
                    byte[] colorBytes = new byte[3];
                    r.NextBytes(colorBytes);
                    var rect = new Rectangle
                    {

                        Height = r.Next(40, 150),
                        Width = r.Next(40, 150),
                        Fill = new SolidColorBrush(new Color
                        {
                            R = colorBytes[0],
                            G = colorBytes[1],
                            B = colorBytes[2],
                            A = 255
                        })
                    };
                    var textBlock = new TextBlock
                    {
                        Text = (i + 1).ToString(),
                        HorizontalAlignment =HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    var grid = new Grid();
                    grid.Children.Add(rect);
                    grid.Children.Add(textBlock);
                    return grid;
                }).ToArray();

            foreach (var item in items)
            {
                grid1.Children.Add(item);
                Rectangle rect = item.Children.First() as Rectangle;
                if (rect.Width > 50)
                {
                    int columnSpan = ((int)rect.Width / 50) + 1;
                    VariableSizedWrapGrid.SetColumnSpan(item, columnSpan);
                    int rowSpan = ((int)rect.Height / 50) + 1;
                    VariableSizedWrapGrid.SetRowSpan(item, rowSpan);
                }
            }

        }
    }
}

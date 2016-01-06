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

namespace ShapesWPF
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

        private void OnChangeShape(object sender, RoutedEventArgs e)
        {
            SetMouth();
        }

        private void OnChangeShape2(object sender, RoutedEventArgs e)
        {
            SetMouth();
        }

        private bool _laugh = false;

        private void SetMouth2()
        {
            if (_laugh)
            {
                mouth.Data = Geometry.Parse("M 40,82 Q 57,65 80,82");
            }
            else
            {
                mouth.Data = Geometry.Parse("M 40,74 Q 57,95 80,74");
            }
            _laugh = !_laugh;
        }

        private readonly Point[,] _mouthPoints = new Point[2, 3]
        {
            {
                new Point(40, 74), new Point(57, 95), new Point(80, 74),
            },
            {
                new Point(40, 82), new Point(57, 65), new Point(80, 82),
            }
        };
        public void SetMouth()
        {
            int index = _laugh ? 0 : 1;

            var figure = new PathFigure() { StartPoint = _mouthPoints[index, 0] };
            figure.Segments = new PathSegmentCollection();
            var segment1 = new QuadraticBezierSegment();
            segment1.Point1 = _mouthPoints[index, 1];
            segment1.Point2 = _mouthPoints[index, 2];
            figure.Segments.Add(segment1);
            var geometry = new PathGeometry();
            geometry.Figures = new PathFigureCollection();
            geometry.Figures.Add(figure);

            mouth.Data = geometry;
            _laugh = !_laugh;
        }
    }


}

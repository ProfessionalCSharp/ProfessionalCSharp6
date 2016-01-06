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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnimationWPF
{
    /// <summary>
    /// Interaction logic for EasingChartControl.xaml
    /// </summary>
    public partial class EasingChartControl : UserControl
    {
        private const double _samplingInterval = 0.01;


        public EasingChartControl()
        {
            InitializeComponent();
        }

        public void Draw(EasingFunctionBase easingFunction)
        {
            canvas1.Children.Clear();


            PathSegmentCollection pathSegments = new PathSegmentCollection();

            for (double i = 0; i < 1; i += _samplingInterval)
            {
                double x = i * canvas1.Width;
                double y = easingFunction.Ease(i) * canvas1.Height;

                var segment = new LineSegment();
                segment.Point = new Point(x, y);

                pathSegments.Add(segment);

            }
            var p = new Path();
            p.Stroke = new SolidColorBrush(Colors.Black);
            p.StrokeThickness = 3;
            PathFigureCollection figures = new PathFigureCollection();
            figures.Add(new PathFigure() { Segments = pathSegments });
            p.Data = new PathGeometry() { Figures = figures };
            canvas1.Children.Add(p);
        }
    }
}

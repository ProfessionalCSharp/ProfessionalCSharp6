using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AnimationWPF
{
    /// <summary>
    /// Interaction logic for EasingFunctions.xaml
    /// </summary>
    public partial class EasingFunctions : Window
    {
        private EasingFunctionsManager _easingFunctions = new EasingFunctionsManager();
        private const int AnimationTimeSeconds = 6;

        public EasingFunctions()
        {
            InitializeComponent();
            foreach (var easingFunctionModel in _easingFunctions.EasingFunctionModels)
            {
                comboEasingFunctions.Items.Add(easingFunctionModel);
            }
            comboEasingFunctions.SelectedIndex = 0;
        }

        private EasingMode GetEasingMode()
        {
            if (easingModeIn.IsChecked == true) return EasingMode.EaseIn;
            else if (easingModeOut.IsChecked == true) return EasingMode.EaseOut;
            else return EasingMode.EaseInOut;
        }

        private void OnStartAnimation(object sender, RoutedEventArgs e)
        {
            var easingFunctionModel = comboEasingFunctions.SelectedItem as EasingFunctionModel;
            if (easingFunctionModel != null)
            {
                EasingFunctionBase easingFunction = easingFunctionModel.EasingFunction;
                easingFunction.EasingMode = GetEasingMode();
                StartAnimation(easingFunction);
            }
        }

        private void StartAnimation(EasingFunctionBase easingFunction)
        {
            // show the chart
            chartControl.Draw(easingFunction);

            // animation
            NameScope.SetNameScope(translate1, new NameScope());

            var storyboard = new Storyboard();
            var ellipseMove = new DoubleAnimation();
            ellipseMove.EasingFunction = easingFunction;
            ellipseMove.Duration = new Duration(TimeSpan.FromSeconds(AnimationTimeSeconds));
            ellipseMove.From = 0;
            ellipseMove.To = 460;
            Storyboard.SetTargetName(ellipseMove, nameof(translate1));
            Storyboard.SetTargetProperty(ellipseMove, new PropertyPath(TranslateTransform.XProperty));
            ellipseMove.BeginTime = TimeSpan.FromSeconds(1.5); // start animation in 0.5 seconds
            ellipseMove.FillBehavior = FillBehavior.HoldEnd; // keep position after animation

            storyboard.Children.Add(ellipseMove);
            storyboard.Begin(this);
        }
    }
}

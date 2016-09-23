using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CodeIntroUWP
{
    public class App : Application
    {
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            var b = new Button
            {
                Content = "Click Me!"
            };
            b.Click += (sender, e) =>
            {
                b.Content = "clicked";
            };
            Window.Current.Content = b;
            Window.Current.Activate();
           
        }
    }
}

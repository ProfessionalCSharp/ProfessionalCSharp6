using ControlsSamples.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ControlsSamples
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnInk(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InkSample));
        }


        private void OnPicker(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TimePickerSample));
        }

        private void OnText(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TextSample));
        }

        private void OnAutoSuggest(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AutoSuggestSample));
        }
    }
}

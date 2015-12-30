using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WinAppInsights
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void OnNavigateToSecondPage(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SecondPage));
        }

        private TelemetryClient _telemetry = new TelemetryClient();

        private async void OnAction(object sender, RoutedEventArgs e)
        {
            _telemetry.TrackEvent("OnAction", properties: new Dictionary<string, string>() { ["data"] = sampleDataText.Text });
            var dialog = new ContentDialog
            {
                Title = "Sample",
                Content = $"You entered {sampleDataText.Text}",
                PrimaryButtonText = "Ok"
            };
            await dialog.ShowAsync();
        }

        private void OnError(object sender, RoutedEventArgs e)
        {
            try
            {
                throw new Exception("something bad happened");
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(new ExceptionTelemetry { Exception = ex, HandledAt = ExceptionHandledAt.UserCode, SeverityLevel = SeverityLevel.Error });
            }


        }
    }
}

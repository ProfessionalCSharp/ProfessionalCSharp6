using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ControlsSamples.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TimePickerSample : Page
    {
        public TimePickerSample()
        {
            this.InitializeComponent();
        }

        private DateTimeOffset _date = DateTimeOffset.UtcNow.AddDays(100).AddHours(3);

        public DateTimeOffset Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private TimeSpan _timeSpan = new TimeSpan(10, 10, 30);
        public TimeSpan Time
        {
            get { return _timeSpan; }
            set { _timeSpan = value; }
        }

        private async void OnButtonClick(object sender, RoutedEventArgs e)
        {
            MessageDialog dlg = new MessageDialog($"Date: {Date} {Time}");
            await dlg.ShowAsync();
        }
    }
}

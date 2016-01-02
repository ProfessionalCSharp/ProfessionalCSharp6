using SensorSampleApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SensorSampleApp
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

        public LightViewModel LightViewModel { get; } = new LightViewModel();

        public CompassViewModel CompassViewModel { get; } = new CompassViewModel();

        public AccelerometerViewModel AccelerometerViewModel { get; } = new AccelerometerViewModel();

        public InclinometerViewModel InclinometerViewModel { get; } = new InclinometerViewModel();

        public GyrometerViewModel GyrometerViewModel { get; } = new GyrometerViewModel();

        public OrientationViewModel OrientationViewModel { get; } = new OrientationViewModel();

    }
}

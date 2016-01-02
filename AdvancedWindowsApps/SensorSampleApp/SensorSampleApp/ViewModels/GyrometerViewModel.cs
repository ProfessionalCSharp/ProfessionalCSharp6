using SensorSampleApp.Framework;
using System;
using Windows.ApplicationModel.Core;
using Windows.Devices.Sensors;
using Windows.UI.Core;

namespace SensorSampleApp.ViewModels
{
    public class GyrometerViewModel : BindableBase
    {
        public void OnGetGyrometer()
        {
            Gyrometer sensor = Gyrometer.GetDefault();
            if (sensor != null)
            {
                GyrometerReading reading = sensor.GetCurrentReading();
                GyrometerInfo = $"X: {reading.AngularVelocityX} Y: {reading.AngularVelocityY} Z: {reading.AngularVelocityZ}";
            }
            else
            {
                GyrometerInfo = "Gyrometer not found";
            }
        }

        private string _gyrometerInfo;

        public string GyrometerInfo
        {
            get { return _gyrometerInfo; }
            set { SetProperty(ref _gyrometerInfo, value); }
        }

        public void OnGetGyrometerReport()
        {
            Gyrometer sensor = Gyrometer.GetDefault();
            if (sensor != null)
            {
                sensor.ReportInterval = Math.Max(sensor.MinimumReportInterval, 1000);

                sensor.ReadingChanged += async (s, e) =>
                {
                    GyrometerReading reading = e.Reading;
                    await CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
                    {
                        GyrometerInfoReport = $"X: {reading.AngularVelocityX} Y: {reading.AngularVelocityY} Z: {reading.AngularVelocityZ} { reading.Timestamp:T}";
                    });

                };
            }
        }

        private string _gyrometerInfoReport;

        public string GyrometerInfoReport
        {
            get { return _gyrometerInfoReport; }
            set { SetProperty(ref _gyrometerInfoReport, value); }
        }
    }
}

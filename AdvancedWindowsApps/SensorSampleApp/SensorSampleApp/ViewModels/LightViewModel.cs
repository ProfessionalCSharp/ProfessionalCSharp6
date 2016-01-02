using SensorSampleApp.Framework;
using Windows.Devices.Sensors;
using Windows.UI.Core;
using System;
using Windows.ApplicationModel.Core;

namespace SensorSampleApp.ViewModels
{
    public class LightViewModel : BindableBase
    {
        public void OnGetLight()
        {
            LightSensor sensor = LightSensor.GetDefault();
            if (sensor != null)
            {
                LightSensorReading reading = sensor.GetCurrentReading();
                Illuminance = $"Illuminance: {reading?.IlluminanceInLux}";
            }
            else
            {
                Illuminance = "Light sensor not found";
            }
        }

        private string _illuminance;

        public string Illuminance
        {
            get { return _illuminance; }
            set { SetProperty(ref _illuminance, value); }
        }

        public void OnGetLightReport()
        {
            LightSensor sensor = LightSensor.GetDefault();
            if (sensor != null)
            {
                sensor.ReportInterval = Math.Max(sensor.MinimumReportInterval, 1000);
                sensor.ReadingChanged += async (s, e) =>
                {
                    LightSensorReading reading = e.Reading;
                  
                    await CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
                    {
                        IlluminanceReport = $"{reading.IlluminanceInLux} {reading.Timestamp:T}";
                    });

                };

            }
        }

        private string _illuminanceReport;

        public string IlluminanceReport
        {
            get { return _illuminanceReport; }
            set { SetProperty(ref _illuminanceReport, value); }
        }

    }
}

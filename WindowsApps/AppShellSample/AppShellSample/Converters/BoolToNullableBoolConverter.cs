using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace AppShellSample.Converters
{
    public class BoolToNullableBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
            => value;

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            bool defaultValue = false;
            if (parameter != null)
            {
                defaultValue = (bool)parameter;
            }

            bool? val = (bool?)value;
            return val ?? defaultValue;
        }
    }
}

using System;
using Windows.UI.Xaml.Data;

namespace DataBindingSamples.Converters
{
    public class ObjectToObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) => value;

        public object ConvertBack(object value, Type targetType, object parameter, string language) => value;
    }
}

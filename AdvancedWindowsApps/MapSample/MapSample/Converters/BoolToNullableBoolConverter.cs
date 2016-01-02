using System;
using Windows.UI.Xaml.Data;

namespace MapSample.Converters
{
    //public class BoolToNullableBoolConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, string language) => value;

    //    public object ConvertBack(object value, Type targetType, object parameter, string language) => value ?? false;

    //}

    public class NullableBoolToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) => value ?? false;

        public object ConvertBack(object value, Type targetType, object parameter, string language) => value;

    }
}

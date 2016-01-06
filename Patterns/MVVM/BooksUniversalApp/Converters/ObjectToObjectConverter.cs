using System;
using Windows.UI.Xaml.Data;

namespace BooksUniversalApp.Converters
{
    public class ObjectToObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) => value;


        public object ConvertBack(object value, Type targetType, object parameter, string language) => value;

    }
}

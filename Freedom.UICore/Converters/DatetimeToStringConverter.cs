using System;
using Windows.UI.Xaml.Data;

namespace Freedom.UICore.Converters
{
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime dateTime = (DateTime)value;
            return dateTime.ToString(parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return default(Object);
        }
    }
}
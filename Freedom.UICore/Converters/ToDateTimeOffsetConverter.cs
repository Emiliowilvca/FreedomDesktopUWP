using System;
using Windows.UI.Xaml.Data;

namespace Freedom.UICore.Converters
{
    public class ToDateTimeOffsetConverter : IValueConverter
    {
        // on binding property call this method
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return new DateTimeOffset(DateTime.UtcNow).ToUniversalTime();
            }
            return new DateTimeOffset((DateTime)value).ToUniversalTime();
        }

        // on select date call this method
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return DateTime.UtcNow;
            }

            return ((DateTimeOffset)value).DateTime;
        }
    }
}
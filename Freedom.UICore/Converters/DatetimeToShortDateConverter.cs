using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace Freedom.UICore.Converters
{
    public class DatetimeToShortDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return null;
            }

            if (DateTime.TryParse(value.ToString(), CultureInfo.CurrentCulture,
                                   DateTimeStyles.AssumeLocal, out var novo))
            {
                DateTime dateTime = (DateTime)value;
                return dateTime.ToShortDateString();
            }

            return "no date";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return default(Object);
        }
    }
}
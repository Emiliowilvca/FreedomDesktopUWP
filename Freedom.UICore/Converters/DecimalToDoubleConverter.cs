using System;
using Windows.UI.Xaml.Data;

namespace Freedom.UICore.Converters
{
    public class DecimalToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return 0;
            }
            return System.Convert.ToDouble(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return System.Convert.ToDecimal(value);
        }
    }
}
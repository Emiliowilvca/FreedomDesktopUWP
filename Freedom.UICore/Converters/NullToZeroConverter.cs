using System;
using Windows.UI.Xaml.Data;

namespace Freedom.UICore.Converters
{
    public class NullToZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value.Equals(0))
                return null;
            else

                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return 0;
            else
                return value;
        }
    }
}
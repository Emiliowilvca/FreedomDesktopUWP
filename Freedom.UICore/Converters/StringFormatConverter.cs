using System;
using Windows.UI.Xaml.Data;

namespace Freedom.UICore.Converters
{
    public class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return null;

            if (parameter == null)
                return value;

            return string.Format((string)parameter, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    //<TextBlock Text="{x:Bind Text, Converter={StaticResource StringFormatConverter},
    //                               ConverterParameter='{}{0:dd/MM/yyy HH\\\\:mm (ddd)}'}" />
}
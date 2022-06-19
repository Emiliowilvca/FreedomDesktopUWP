using System;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
namespace Freedom.UICore.Converters
{
    public class CustomStringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var mask = parameter as string;
            var inString = value as string;

            if (mask != null & inString != null)
            {
                MaskedTextProvider mtp = new MaskedTextProvider(mask);

                if (mtp.Set(inString))
                {
                    return mtp.ToDisplayString();
                }
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
using System.Globalization;

namespace Freedom.Core.CoreInterface
{
    public interface ILocalizeResource
    {
        string GetDescription(string resourceKey);

        string GetDescription(string resourceKey, CultureInfo cultureInfo);
    }
}
using Freedom.Core.CoreInterface;
using Freedom.Utility.Langs;
using System.Globalization;
using System.Resources;
namespace Freedom.Core.Implement
{
    public class LocalizeResource : ILocalizeResource
    {
        private readonly ResourceManager _resource;

        public LocalizeResource()
        {
            _resource = new ResourceManager(typeof(Lang));
        }

        public string GetDescription(string resourceKey)
        {
            string displayName = _resource.GetString(resourceKey);
            return string.IsNullOrEmpty(displayName) ? resourceKey : displayName;
        }

        public string GetDescription(string resourceKey, CultureInfo cultureInfo)
        {
            string displayName = _resource.GetString(resourceKey, cultureInfo);
            return string.IsNullOrEmpty(displayName) ? resourceKey : displayName;
        }
    }
}
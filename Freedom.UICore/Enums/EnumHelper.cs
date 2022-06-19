using System;
using System.ComponentModel;
using System.Linq;

namespace Freedom.UICore
{
    public static class EnumHelper
    {
        public static string GetDescription(Enum enumValue)
        {
            var descriptionAttribute = enumValue.GetType()
                .GetField(enumValue.ToString())
                .GetCustomAttributes(false)
                .SingleOrDefault(attr => attr.GetType() == typeof(DescriptionAttribute)) as DescriptionAttribute;
            return descriptionAttribute?.Description ?? "";
        }
    }
}
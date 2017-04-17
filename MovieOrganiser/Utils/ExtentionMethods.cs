using System;
using System.ComponentModel;
using System.Linq;

namespace MovieOrganiser
{
    public static class ExtentionMethods
    {
        public static string GetDescription(this Enum enumValue)
        {
           var descriptionAttribute = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault();
            return descriptionAttribute?.Description ?? string.Empty;
        }
    }
}
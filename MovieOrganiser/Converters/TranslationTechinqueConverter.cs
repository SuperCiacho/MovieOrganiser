using System;
using System.Globalization;
using System.Windows.Data;
using MovieOrganiser.Model;

namespace MovieOrganiser.Converters
{
    class TranslationTechinqueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var v = (TranslationTechnique) value;
            return v;
        }
    }
}

// File created by Bartosz Nowak on 20/04/2015 22:29

using System;
using System.Globalization;
using System.Windows.Data;
using MovieOrganiser.Model;

namespace MovieOrganiser.Converters
{
    [ValueConversion(typeof(TranslationTechnique), typeof(int))]
    internal class TranslationTechinqueConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)(value ?? -1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            return (TranslationTechnique) value;
        }

        #endregion
    }
}
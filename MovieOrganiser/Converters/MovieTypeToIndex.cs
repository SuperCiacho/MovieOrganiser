// File created by Bartosz Nowak on 20/07/2014 18:09

using System;
using System.Globalization;
using System.Windows.Data;
using Yorgi.FilmWebApi.Models;

namespace MovieOrganiser.Converters
{
    internal class MovieTypeToIndex : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return -1;
            if (value is MovieType) return (int) value;
            throw new ArgumentException("Value parameter has to be MovieType type.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (MovieType) value;
        }

        #endregion
    }
}
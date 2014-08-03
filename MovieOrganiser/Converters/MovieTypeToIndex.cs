using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using MovieOrganiser.Model;

namespace MovieOrganiser.Converters
{
    class MovieTypeToIndex : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return -1;
            if(value is MovieType) return (int)value;
            throw new ArgumentException("Value parameter has to be MovieType type.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (MovieOrganiser.Model.MovieType)value;
        }
    }
}

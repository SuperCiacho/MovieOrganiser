using System;
using MovieOrganiser.Utils;

namespace MovieOrganiser.Converters
{
    public class WidthToColumnNumberConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var test = value == null || double.IsNaN((double)value) || double.IsInfinity((double)value);
            if (test) return 1;
            var numberOfColumns = (int)((double)value / Constants.ColumnWidth);
            return numberOfColumns < 1 ? 1 : numberOfColumns;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

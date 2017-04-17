// File created by Bartosz Nowak on 09/07/2014 00:37

using System;
using System.Globalization;
using System.Windows.Data;
using MovieOrganiser.Utils;

namespace MovieOrganiser.Converters
{
    public class WidthToColumnNumberConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var test = value == null || double.IsNaN((double) value) || double.IsInfinity((double) value);
            if (test) return 1;
            var numberOfColumns = (int) ((double) value/Constants.ColumnWidth);
            return numberOfColumns < 1 ? 1 : numberOfColumns;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
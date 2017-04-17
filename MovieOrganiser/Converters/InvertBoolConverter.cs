// File created by Bartosz Nowak on 20/07/2014 01:50

using System;
using System.Globalization;
using System.Windows.Data;

namespace MovieOrganiser.Converters
{
    public class InvertBoolConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool) value;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Convert(value, targetType, parameter, culture);
        }

        #endregion
    }
}
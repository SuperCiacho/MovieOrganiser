// File created by Bartosz Nowak on 06/07/2014 18:30

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using MovieOrganiser.Model;

namespace MovieOrganiser.Converters
{
    public class ValueToVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool state = false;

            if (value is bool) state = (bool) value;
            else if (value is string) state = true;
            else if (value is MovieInfo) state = true;

            if (parameter != null)
            {
                switch (parameter as string)
                {
                    case "NULL":
                        state = value != null;
                        break;
                    case "Invert":
                        state = !state;
                        break;
                }
            }

            return state ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
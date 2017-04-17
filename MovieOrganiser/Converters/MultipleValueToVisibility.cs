﻿// File created by Bartosz Nowak on 23/07/2014 20:29

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MovieOrganiser.Converters
{
    public class TextInputToVisibilityConverter : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Always test MultiValueConverter inputs for non-null
            // (to avoid crash bugs for views in the designer)
            if (values[0] is bool && values[1] is bool)
            {
                bool hasText = !(bool) values[0];
                bool hasFocus = (bool) values[1];

                if (hasFocus || hasText) return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
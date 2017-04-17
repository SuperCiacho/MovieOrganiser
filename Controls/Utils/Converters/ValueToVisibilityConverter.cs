using System;
using System.Windows;
using System.Windows.Data;

namespace YorgiControls.Utils.Converters
{
    public class ValueToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool state = false;

            if (value is bool) state = (bool)value;
            else if (value is string) state = value != null;

            if (parameter != null)
            {
                switch(parameter as string)
                {
                    case "NULL": state = value != null; 
                        break;
                    case "Invert": state = !state;
                        break;
                }
            }

            return state ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

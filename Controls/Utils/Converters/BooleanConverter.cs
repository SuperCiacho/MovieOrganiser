using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace YorgiControls.Utils.Converters
{
    internal class BooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? b = value as bool?;

            if (value == null) return false;

            switch((string)parameter)
            {
                case "Invert":
                    return !b;
                case "Visibility":
                    return b == true ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                default:
                    return b;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}

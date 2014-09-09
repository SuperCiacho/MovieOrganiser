using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using YorgiControls.Model;

namespace YorgiControls.Utils.Converters
{
    public class MultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Any(value => value == null) ? values : new[] {(values[0] as Item).Path, values[1]};
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return value as object[];
        }
    }
}
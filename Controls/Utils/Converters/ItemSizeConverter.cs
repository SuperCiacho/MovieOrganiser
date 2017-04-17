using System;
using System.Globalization;
using System.Windows.Data;

namespace YorgiControls.Utils.Converters
{
    public class ItemSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dim = (double) value;
            int div = 1;
            if ((string) parameter == "h") div = 3;
            if ((string) parameter == "w") div = 4;
            return Math.Floor(dim/div);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

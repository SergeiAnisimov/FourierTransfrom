using System;
using System.Globalization;
using System.Windows.Data;

namespace FourierTransfrom.BindingConverters
{
    internal class HalfConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double width)
                return width / 2;

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double width)
                return width  * 2;

            return 0;
        }
    }
}

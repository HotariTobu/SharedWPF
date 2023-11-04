using System;
using System.Globalization;
using System.Windows.Data;

namespace SharedWPF
{
    public class DoubleToByteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte b)
            {
                return b;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double d)
            {
                return d;
            }

            return null;
        }
    }
}

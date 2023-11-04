using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace SharedWPF
{
    public class BooleanToInvertedVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                return b ? Visibility.Collapsed : Visibility.Visible;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility v)
            {
#if NET5_0 || NETCOREAPP || NETCOREAPP3_1
                return v switch
                {
                    Visibility.Visible => false,
                    Visibility.Collapsed => true,
                    _ => null
                };
#else
                switch (v)
                {
                    case Visibility.Visible:
                        return false;
                    case Visibility.Collapsed:
                        return true;
                }
#endif
            }

            return null;
        }
    }
}

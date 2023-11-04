using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SharedWPF
{
    public class MultiDoubleToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            byte a, r, g, b;
            switch (values.Length)
            {
                case 1:
                    if (tryCast(values[0], out byte v))
                    {
                        return Color.FromRgb(v, v, v);
                    }
                    break;
                case 3:
                    if (tryCast(values[0], out r) && tryCast(values[1], out g) && tryCast(values[2], out b))
                    {
                        return Color.FromRgb(r, g, b);
                    }
                    break;
                case 4:
                    if (tryCast(values[0], out a) && tryCast(values[1], out r) && tryCast(values[2], out g) && tryCast(values[3], out b))
                    {
                        return Color.FromArgb(a, r, g, b);
                    }
                    break;
            }

            bool tryCast(object o, out byte b)
            {
                if (o is double d)
                {
                    b = (byte)d;
                    return true;
                }

                b = 0;
                return false;
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                return new object[] { color.A, color.R, color.G, color.B, };
            }

            return null;
        }
    }
}

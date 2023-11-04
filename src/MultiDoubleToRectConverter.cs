using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace SharedWPF
{
    public class MultiDoubleToRectConverter : IMultiValueConverter
    {
        public bool IsPointBase;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            switch (values.Length)
            {
                case 2:
                    if (values[0] is double v1 && values[1] is double v2)
                    {
                        if (IsPointBase)
                        {
                            return new Rect(v1, v2, 0, 0);
                        }
                        else
                        {
                            return new Rect(0, 0, v1, v2);
                        }
                    }
                    break;
                case 4:
                    if (values[0] is double x && values[1] is double y && values[2] is double w && values[3] is double h)
                    {
                        return new Rect(x, y, w, h);
                    }
                    break;
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is Rect rect)
            {
                return new object[] { rect.X, rect.Y, rect.Width, rect.Height };
            }

            return null;
        }
    }
}

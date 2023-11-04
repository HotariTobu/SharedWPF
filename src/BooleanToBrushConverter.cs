using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace SharedWPF
{
    /**
     * How To Use
     * <shared:BooleanToBrushConverter x:Key="BooleanToBrushConverter" TrueBrush="Black" FalseBrush="White" NullBrush="Transparent"/>
     * SomeBrush="{Binding IsSome Converter={StaticResource BooleanToBrushConverter}}"
     */
    internal class BooleanToBrushConverter : IValueConverter
    {
        public Brush TrueBrush { get; set; }
        public Brush FalseBrush { get; set; }
        public Brush NullBrush { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                if (b)
                {
                    return TrueBrush;
                }
                else
                {
                    return FalseBrush;
                }
            }

            return NullBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Brush b)
            {
                if (b.Equals(TrueBrush))
                {
                    return true;
                }
                else if (b.Equals(FalseBrush))
                {
                    return false;
                }
            }

            return null;
        }
    }
}

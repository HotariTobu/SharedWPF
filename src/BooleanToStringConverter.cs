using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace SharedWPF
{
    /**
     * How To Use
     * <shared:BooleanToStringConverter x:Key="BooleanToStringConverter"/>
     * SomeProperty="{Binding IsSome Converter={StaticResource BooleanToStringConverter}, ConverterParameter=StringWhenTrue//StringWhenFalse//StringWhenNull}"
     */
    internal class BooleanToStringConverter : IValueConverter
    {
        public string Separator { get; set; } = @"//";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string p)
            {
                string[] ps = p.Split(Separator);

                if (value is bool b)
                {
                    if (b)
                    {
                        return ps[0];
                    }
                    else if (ps.Length >= 2)
                    {
                        return ps[1];
                    }
                }
                else if (ps.Length >= 3)
                {
                    return ps[2];
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s && parameter is string p)
            {
                string[] ps = p.Split(Separator);
                if (s.Equals(ps[0]))
                {
                    return true;
                }
                else if (ps.Length >= 2 && s.Equals(ps[1]))
                {
                    return false;
                }
            }

            return null;
        }
    }
}

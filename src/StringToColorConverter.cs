using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace SharedWPF
{
    class StringToColorConverter : IValueConverter
    {
        public string Parameter
        {
            get
            {
                if (Dic == null)
                {
                    return null;
                }
                else
                {
                    return string.Join("|", Dic.Select(x => $"{x.Key}/{x.Value}"));
                }
            }

            set
            {
                Dic = AnalyzeParameter(value, CultureInfo.CurrentUICulture);
            }
        }

        private Dictionary<string, Color> Dic;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Dictionary<string, Color> dic = Dic;
            if (parameter != null)
            {
                dic = AnalyzeParameter(parameter, culture);
            }

            if (dic != null)
            {
                string text = value.ToString();
                if (dic.ContainsKey(text))
                {
                    return dic[text];
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Dictionary<string, Color> dic = Dic;
            if (parameter != null)
            {
                dic = AnalyzeParameter(parameter, culture);
            }

            if (dic != null)
            {
                if (value is Color color)
                {
                    foreach (KeyValuePair<string, Color> pair in dic)
                    {
                        if (color.Equals(pair.Value))
                        {
                            return pair.Key;
                        }
                    }
                }
            }

            return null;
        }

        private Dictionary<string, Color> AnalyzeParameter(object parameter, CultureInfo culture)
        {
            if (parameter is string param)
            {
                return new Dictionary<string, Color>(param.Split('|', StringSplitOptions.RemoveEmptyEntries).Select(pair =>
                {
                    NumberFormatInfo formatInfo = culture.NumberFormat;
                    string[] keyValue = pair.Split('/', StringSplitOptions.RemoveEmptyEntries);
                    if (keyValue.Length == 2)
                    {
                        string[] values = keyValue[1].Trim(' ', '#').Select((c, i) => (c, i)).GroupBy(x => x.i / 2).Select(x => new string(x.Select(y => y.c).ToArray())).ToArray();
                        bool parse(int index, out byte v) => byte.TryParse(values[index], NumberStyles.HexNumber, formatInfo, out v);
#if NET5_0 || NETCOREAPP || NETCOREAPP3_1
                        return KeyValuePair.Create(keyValue[0].Trim(),
                            values.Length switch
                            {
                                1 => parse(0, out byte v) ? Color.FromRgb(v, v, v) : new Color(),
                                3 => parse(0, out byte r) && parse(1, out byte g) && parse(2, out byte b) ? Color.FromRgb(r, g, b) : new Color(),
                                4 => parse(0, out byte a) && parse(1, out byte r) && parse(2, out byte g) && parse(3, out byte b) ? Color.FromArgb(a, r, g, b) : new Color(),
                                _ => new Color(),
                            });
#else
                        switch (values.Length)
                        {
                            case 1:
                                return parse(0, out byte v) ? Color.FromRgb(v, v, v) : new Color();
                            case 3:
                                return parse(0, out byte r) && parse(1, out byte g) && parse(2, out byte b) ? Color.FromRgb(r, g, b) : new Color();
                            case 4:
                                return parse(0, out byte a) && parse(1, out byte r) && parse(2, out byte g) && parse(3, out byte b) ? Color.FromArgb(a, r, g, b) : new Color();
                            default:
                                return new Color();
                        }
#endif
                    }
                    else
                    {
                        return KeyValuePair.Create(string.Empty, new Color());
                    }
                }).Where(x => !string.IsNullOrWhiteSpace(x.Key)));
            }
            else
            {
                return null;
            }
        }
    }
}

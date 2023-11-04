using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SharedWPF
{
    public static class ControlExtension
    {
        public static Size MeasureText(this Control control, string text) => Utilities.MeasureText(text, control.FontFamily, control.FontStyle, control.FontWeight, control.FontStretch, control.FontSize);
    }
}

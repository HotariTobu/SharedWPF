using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;

namespace SharedWPF.Extensions
{
    public static class RichTextBoxExtension
    {
        public static string GetText(this RichTextBox richTextBox) => new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;
    }
}

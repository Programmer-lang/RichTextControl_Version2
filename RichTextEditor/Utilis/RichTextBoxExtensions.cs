using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace RichTextEditor.Utilis
{
    public static class RichTextBoxExtensions
    {
        public static string GetRtfText(FlowDocument document)
        {
            TextRange tr = new TextRange(document.ContentStart, document.ContentEnd);
            using (MemoryStream ms = new MemoryStream())
            {
                tr.Save(ms, System.Windows.DataFormats.Rtf);
                return ASCIIEncoding.Default.GetString(ms.ToArray());
            }
        }

        public static void SetRtfText(FlowDocument document, string text)
        {
            try
            {
                if (String.IsNullOrEmpty(text))
                {
                    document.Blocks.Clear();
                }
                else
                {
                    TextRange tr = new TextRange(document.ContentStart, document.ContentEnd);
                    using (MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(text)))
                    {
                        tr.Load(ms, DataFormats.Rtf);
                    }
                }
            }
            catch
            {
                throw new InvalidDataException("Data provided is not in the correct RTF format.");
            }
        }

        public static string GetText(FlowDocument document)
        {
            return new TextRange(document.ContentStart, document.ContentEnd).Text;
        }

        public static void SetText(FlowDocument document, string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                document.Blocks.Clear();
            }
            else
            {
                new TextRange(document.ContentStart, document.ContentEnd).Text = text;

            }
        }

    }
}

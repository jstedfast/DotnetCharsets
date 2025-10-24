using System.Text;

namespace DotnetCharsets
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var encodings = Encoding.GetEncodings();
            Array.Sort(encodings, (a, b) => a.CodePage.CompareTo(b.CodePage));

            WriteTableHeader("Complete set");
            foreach (var info in encodings)
            {
                var encoding = Encoding.GetEncoding(info.CodePage);
                WriteCharsetInfo(encoding);
            }

            Console.WriteLine();
            WriteTableHeader("Encodings where HeaderName != WebName");
            foreach (var info in encodings)
            {
                var encoding = Encoding.GetEncoding(info.CodePage);
                if (!encoding.HeaderName.Equals(encoding.WebName, StringComparison.OrdinalIgnoreCase))
                    WriteCharsetInfo(encoding);
            }

            Console.WriteLine();
            WriteTableHeader("Encodings where HeaderName != BodyName");
            foreach (var info in encodings)
            {
                var encoding = Encoding.GetEncoding(info.CodePage);
                if (!encoding.HeaderName.Equals(encoding.BodyName, StringComparison.OrdinalIgnoreCase))
                    WriteCharsetInfo(encoding);
            }

            Console.WriteLine();
            WriteTableHeader("Encodings where WebName != BodyName");
            foreach (var info in encodings)
            {
                var encoding = Encoding.GetEncoding(info.CodePage);
                if (!encoding.WebName.Equals(encoding.BodyName, StringComparison.OrdinalIgnoreCase))
                    WriteCharsetInfo(encoding);
            }

            Console.WriteLine();
            WriteTableHeader("Extra Encodings");
            var extra = new int[] { 932, 949, 50220, 50221, 50222, 50225 };
            foreach (var codepage in extra)
            {
                var encoding = Encoding.GetEncoding(codepage);
                WriteCharsetInfo(encoding);
            }

            Console.WriteLine();
            WriteTableHeader("Encodings that have byte-order marks (BOMs)");
            foreach (var info in encodings)
            {
                var encoding = Encoding.GetEncoding(info.CodePage);
                var preamble = encoding.GetPreamble();

                if (preamble.Length > 0)
                    WriteCharsetInfo(encoding);
            }

            Console.WriteLine();
            WriteTableHeader("Unicode Encodings");
            WriteCharsetInfo(Encoding.Unicode);
            WriteCharsetInfo(Encoding.BigEndianUnicode);
            WriteCharsetInfo(Encoding.UTF32);
            WriteCharsetInfo(Encoding.UTF8);
        }

        static void WriteTableHeader (string tableName)
        {
            Console.WriteLine($"## {tableName}");
            Console.WriteLine();
            Console.WriteLine("| CodePage | HeaderName              | BodyName                | WebName                 |");
            Console.WriteLine("|:--------:|:-----------------------:|:-----------------------:|:-----------------------:|");
        }

        static void WriteCharsetInfo(Encoding encoding)
        {
            Console.WriteLine($"| {encoding.CodePage,-8} | {encoding.HeaderName,-23} | {encoding.BodyName,-23} | {encoding.WebName,-23} |");
        }
    }
}
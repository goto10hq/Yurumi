using HtmlAgilityPack;
using System.Text;
using System.Text.RegularExpressions;

namespace Yurumi.Helpers
{
    static class Extensions
    {
        public static string ToPlainText(this string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var root = doc.DocumentNode;
            var sb = new StringBuilder();

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//body//text()"))
            {
                var text = node.InnerText;

                if (Regex.IsMatch(text, @"^[\r\n\t\s]*$", RegexOptions.Singleline))
                    continue;

                text = text.Trim(new[] { '\r', '\n', '\t', ' ' });

                if (!string.IsNullOrWhiteSpace(text))
                    sb.AppendLine(text);
            }

            return sb.ToString();
        }
    }
}
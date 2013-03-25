using System.IO;
using HtmlAgilityPack;

namespace MtgExplorer.Gatherer
{
    public static class HtmlConverter
    {
        public static string ConvertHtml(HtmlNode node)
        {
            var sw = new StringWriter();
            ConvertTo(node, sw);
            sw.Flush();
            return sw.ToString();
        }

        private static void ConvertTo(HtmlNode node, TextWriter outText)
        {
            switch (node.NodeType)
            {
                case HtmlNodeType.Comment:
                    break;
                case HtmlNodeType.Document:
                    ConvertContentTo(node, outText);
                    break;
                case HtmlNodeType.Text:
                    string parentName = node.ParentNode.Name;
                    if ((parentName == "script") || (parentName == "style"))
                        break;

                    string html = ((HtmlTextNode) node).Text;
                    if (HtmlNode.IsOverlappedClosingElement(html))
                        break;
                    if (html.Trim().Length > 0)
                    {
                        outText.Write(HtmlEntity.DeEntitize(html));
                    }
                    break;
                case HtmlNodeType.Element:
                    switch (node.Name)
                    {
                        case "p":
                            // treat as crlf
                            outText.Write("\r\n");
                            break;
                        case "img":
                            // extract alt text
                            outText.Write('{');
                            outText.Write(node.GetAttributeValue("alt", string.Empty));
                            outText.Write('}');
                            break;
                    }

                    if (node.HasChildNodes)
                    {
                        ConvertContentTo(node, outText);
                    }
                    break;
            }
        }

        private static void ConvertContentTo(HtmlNode node, TextWriter outText)
        {
            foreach (HtmlNode subnode in node.ChildNodes)
            {
                ConvertTo(subnode, outText);
            }
        }
    }
}
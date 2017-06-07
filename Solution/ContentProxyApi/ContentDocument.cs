using ContentContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ContentProxyApi
{
    internal class ContentDocument
    {
        private readonly string _body;

        public string Title { get; private set; }

        public Uri NextUrl { get; private set; }

        public string Html
        {
            get
            {
                return "<html>"
                    + Environment.NewLine
                    + "<head><title>"
                    + Title
                    + "</title></head>"
                    + Environment.NewLine
                    + "<body>"
                    + Environment.NewLine
                    + _body
                    + Environment.NewLine
                    + "</body></html>";
            }
        }

        public static ContentDocument LoadHtml(string html, PolicyModel policy)
        {
            var doc = new HtmlDocument();

            doc.LoadHtml(html);

            var titleNode = doc.DocumentNode.SelectSingleNode("//title");
            var title = titleNode == null ? string.Empty : titleNode.InnerText;
            var nextUrl = SelectNextUrl(doc.DocumentNode, policy.NextUrlXPath);
            var nodes = from path in policy.XPaths
                        select doc.DocumentNode.SelectSingleNode(path);
            var fragments = from n in nodes
                            let scrubbed = Scrub(n)
                            select scrubbed.OuterHtml;
            var body = string.Join(Environment.NewLine, fragments);

            return new ContentDocument(title, new Uri(nextUrl), body);
        }

        private static string SelectNextUrl(HtmlNode node, string nextUrlXPath)
        {
            if (!string.IsNullOrWhiteSpace(nextUrlXPath))
            {
                var urlNode = node.SelectSingleNode(nextUrlXPath);

                if (urlNode != null)
                {
                    var lastPart = nextUrlXPath.Split('/').Last();

                    if (lastPart.StartsWith("@"))
                    {
                        var attribute = urlNode.Attributes[lastPart.Substring(1)];

                        if (attribute != null)
                        {
                            return attribute.Value;
                        }
                    }
                    else
                    {
                        return urlNode.InnerHtml;
                    }
                }
            }

            return string.Empty;
        }

        public ContentDocument Merge(ContentDocument document)
        {
            throw new NotImplementedException();
        }

        public ContentDocument MapImages()
        {
            throw new NotImplementedException();
        }

        private ContentDocument(string title, Uri nextUrl, string body)
        {
            _body = body;
            Title = title;
            NextUrl = nextUrl;
        }

        private static HtmlNode Scrub(HtmlNode node)
        {
            RemoveAttribute(node, "id");
            RemoveAttribute(node, "class");
            RemoveElement(node, "//script");
            RemoveElement(node, "//noscript");
            RemoveElement(node, "//form");
            RemoveElement(node, "//input");

            return node;
        }

        private static HtmlNode RemoveAttribute(HtmlNode node, string attributeName)
        {
            foreach (var child in node.SelectNodes("//*[@" + attributeName + "]"))
            {
                child.Attributes[attributeName].Remove();
            }

            return node;
        }

        private static HtmlNode RemoveElement(HtmlNode node, string xpath)
        {
            foreach (var child in node.SelectNodes(xpath))
            {
                child.Remove();
            }

            return node;
        }
    }
}
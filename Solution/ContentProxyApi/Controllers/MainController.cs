using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContentContract;
using ApiLib;

namespace ContentProxyApi.Controllers
{
    [Route("/")]
    public class MainController : Controller
    {
        private const int MAX_RECURSION = 20;

        public async Task Post([FromBody]ProxyRequestModel model)
        {
            var content = await GetScrubbedContentRecursive(
                new Uri(model.RequestUrl),
                model.Policy,
                MAX_RECURSION);

            await HttpHelper.WriteStreamAsync(content.Html, Response.Body);
        }

        private static async Task<ContentDocument> GetScrubbedContentRecursive(
            Uri requestUrl,
            PolicyModel policy,
            int maxRecursion)
        {
            var urls = new List<Uri>(new[] { requestUrl });
            var content = await GetScrubbedContent(requestUrl, policy);

            while (--maxRecursion > 0 && content.IsNextUrl)
            {
                var nextUrl = ComputeAbsoluteNextUrl(
                    content.NextUrl,
                    urls.Last());

                if (!urls.Contains(nextUrl))
                {
                    var subContent = await GetScrubbedContent(nextUrl, policy);

                    urls.Add(nextUrl);
                    content = content.Merge(subContent);
                }
                else
                {
                    return content;
                }
            }

            return content;
        }

        private static Uri ComputeAbsoluteNextUrl(Uri nextUrl, Uri currentUrl)
        {
            return nextUrl.IsAbsoluteUri
                ? nextUrl
                : new Uri(
                    new Uri(
                        currentUrl.AbsoluteUri.Substring(
                            0,
                            currentUrl.AbsoluteUri.Length - currentUrl.PathAndQuery.Length)),
                    nextUrl.OriginalString);
        }

        private static async Task<ContentDocument> GetScrubbedContent(
            Uri requestUrl,
            PolicyModel policy)
        {
            var proxyRequest = new HttpRequest();
            var proxyResponse = await HttpHelper.GetAsync(requestUrl, proxyRequest);
            var document = ContentDocument.LoadHtml(proxyResponse.Payload, policy);

            return document;
        }
    }
}
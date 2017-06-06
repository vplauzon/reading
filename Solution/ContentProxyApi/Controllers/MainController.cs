using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContentContract;
using ApiLib;
using HtmlAgilityPack;

namespace ContentProxyApi.Controllers
{
    [Route("/")]
    public class MainController : Controller
    {
        public async Task Post([FromBody]ProxyRequestModel model)
        {
            var proxyRequest = new HttpRequest
            {
                Payload = await HttpHelper.ReadStreamAsync(Request.Body)
            };
            var proxyResponse = await HttpHelper.GetAsync(new Uri(model.RequestUrl), proxyRequest);
            var doc = new HtmlDocument();

            doc.LoadHtml(proxyResponse.Payload);

            var nodes = from path in model.Policy.XPaths
                        select doc.DocumentNode.SelectSingleNode(path);
            var fragments = from n in nodes
                            select n.OuterHtml;
            var text = string.Join(Environment.NewLine, fragments);

            await HttpHelper.WriteStreamAsync(text, Response.Body);
        }
    }
}
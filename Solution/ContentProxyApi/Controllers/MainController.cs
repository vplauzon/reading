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
        public async Task Post([FromBody]ProxyRequestModel model)
        {
            var proxyRequest = new HttpRequest
            {
                Payload = await HttpHelper.ReadStreamAsync(Request.Body)
            };
            var proxyResponse = await HttpHelper.GetAsync(new Uri(model.RequestUrl), proxyRequest);

            await HttpHelper.WriteStreamAsync(proxyResponse.Payload, Response.Body);
        }
    }
}
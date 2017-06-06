using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HtmlSelector.ViewModels;
using ContentContract;
using ApiLib;
using HtmlSelector.Configuration;
using Microsoft.Extensions.Options;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HtmlSelector.Controllers
{
    [Route("api/[controller]")]
    public class ProxyController : Controller
    {
        private readonly ApiConfiguration _apiConfiguration;

        public ProxyController(IOptions<ApiConfiguration> apiConfiguration)
        {
            if (apiConfiguration == null)
            {
                throw new ArgumentNullException(nameof(apiConfiguration));
            }
            _apiConfiguration = apiConfiguration.Value;
        }

        [HttpPost]
        public async Task Post()
        {
            var proxyRequest = new HttpRequest
            {
                Payload = await HttpHelper.ReadStreamAsync(Request.Body)
            };
            var proxyResponse = await HttpHelper.PostAsync(
                new Uri(_apiConfiguration.ContentApiUrl),
                proxyRequest);

            await HttpHelper.WriteStreamAsync(proxyResponse.Payload, Response.Body);
        }
    }
}
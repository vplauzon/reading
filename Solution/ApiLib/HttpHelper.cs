using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ApiLib
{
    public static class HttpHelper
    {
        private const string DEFAULT_CONTENT_TYPE = "application/json";

        public static async Task<string> ReadStreamAsync(Stream input)
        {
            using (var reader = new StreamReader(input))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public static async Task WriteStreamAsync(string input, Stream output)
        {
            using (var writer = new StreamWriter(output))
            {
                await writer.WriteAsync(input);
            }
        }

        public static Task<HttpResponse> GetAsync(Uri requestUrl, HttpRequest request)
        {
            return QueryAsync(requestUrl, "GET", request);
        }

        public static Task<HttpResponse> PostAsync(Uri requestUrl, HttpRequest request)
        {
            return QueryAsync(requestUrl, "POST", request);
        }

        private async static Task<HttpResponse> QueryAsync(
            Uri requestUrl,
            string method,
            HttpRequest request)
        {
            var webRequest = WebRequest.Create(requestUrl) as HttpWebRequest;

            webRequest.Method = method;
            webRequest.ContentType = request.ContentType;

            if (!string.IsNullOrWhiteSpace(request.Payload))
            {
                var requestStream = await webRequest.GetRequestStreamAsync();

                using (var writer = new StreamWriter(requestStream))
                {
                    await writer.WriteAsync(request.Payload);
                }
            }
            using (var webResponse = await webRequest.GetResponseAsync() as HttpWebResponse)
            {
                var responseStream = webResponse.GetResponseStream();

                using (var reader = new StreamReader(responseStream))
                {
                    var payload = await reader.ReadToEndAsync();

                    return new HttpResponse
                    {
                        Payload = payload,
                        Status = webResponse.StatusCode
                    };
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiLib
{
    public class HttpRequest
    {
        public static string JSON_CONTENT_TYPE = "application/json";

        public static string HTML_CONTENT_TYPE = "text/html, application/xhtml+xml, image/jxr, */*";

        public string ContentType { get; set; } = JSON_CONTENT_TYPE;

        public string Payload { get; set; }
    }
}
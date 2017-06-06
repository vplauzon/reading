using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ApiLib
{
    public class HttpResponse
    {
        public HttpStatusCode Status { get; set; }

        public string Payload { get; set; }
    }
}
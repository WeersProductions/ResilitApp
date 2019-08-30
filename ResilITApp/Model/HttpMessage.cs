using System;
using System.Net.Http;

namespace ResilITApp.Model
{
    public class HttpMessage
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public HttpResponseMessage Response { get; set; }
    }
}

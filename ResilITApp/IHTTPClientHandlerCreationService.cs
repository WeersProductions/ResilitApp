using System;
using System.Net.Http;

public interface IHTTPClientHandlerCreationService
{
    HttpClientHandler GetInsecureHandler();
}

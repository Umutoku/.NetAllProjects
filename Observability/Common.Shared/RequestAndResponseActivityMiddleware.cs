using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using System.Diagnostics;
using System.Net.Http;

namespace Common.Shared
{
    //Microsoft.IO.RecyclableMemoryStream is a high performance memory manager library for .NET Core
    public class RequestAndResponseActivityMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        public RequestAndResponseActivityMiddleware(RequestDelegate next)
        {
            _next = next;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager(); // bu sayede bellek yönetimini daha iyi yapabiliriz. //DI ekli olmadığı için new'liyoruz.
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            await AddRequestBodyContentToActivityTags(httpContext);

            await AddResponseBodyContentToActivityTags(httpContext);
        }

        private async Task AddResponseBodyContentToActivityTags(HttpContext httpContext)
        {
            var originalResponseBodyStream = httpContext.Response.Body;

            await using var responseBodyMemoryStream = _recyclableMemoryStreamManager.GetStream(); 
            httpContext.Response.Body = responseBodyMemoryStream;

            await _next(httpContext);

            responseBodyMemoryStream.Position = 0; // bu sayede body içeriğini tekrar okuyabiliriz.
            
            var responseBodyStreamReader = new StreamReader(responseBodyMemoryStream);
            var responseBodyContent = await responseBodyStreamReader.ReadToEndAsync();
            Activity.Current?.AddTag("http.response.body", responseBodyContent);

            responseBodyMemoryStream.Position = 0; // bu sayede body içeriğini tekrar okuyabiliriz.

            await responseBodyMemoryStream.CopyToAsync(originalResponseBodyStream);

        }

        private async Task AddRequestBodyContentToActivityTags(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();
            var requestBodyStreamReader = new StreamReader(httpContext.Request.Body);

            var requestBodyContent = await requestBodyStreamReader.ReadToEndAsync();

            Activity.Current?.AddTag("http.request.body", requestBodyContent); // add tag to general activity

            httpContext.Request.Body.Position = 0; // bu sayede body içeriğini tekrar okuyabiliriz.
        }
    }
}

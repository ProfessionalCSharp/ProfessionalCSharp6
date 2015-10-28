using Microsoft.AspNet.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace WebSampleApp.Middleware
{
    public class HeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public HeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.Headers.Add("sampleheader", new string[] { "from addheadermiddleware" });
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseHeaderMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HeaderMiddleware>();
        }
    }
}

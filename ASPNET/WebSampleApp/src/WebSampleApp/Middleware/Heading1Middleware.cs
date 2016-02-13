using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebSampleApp.Middleware
{
    // You may need to install the Microsoft.AspNet.Http.Abstractions package into your project
    public class Heading1Middleware
    {
        private readonly RequestDelegate _next;

        public Heading1Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await httpContext.Response.WriteAsync("<h1>From Middleware</h1>");
            await _next(httpContext);
        }
    }


    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class Heading1MiddlewareExtensions
    {
        public static IApplicationBuilder UseHeading1Middleware(this IApplicationBuilder builder) =>
             builder.UseMiddleware<Heading1Middleware>(); 
    }
}

using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GrooveMessengerAPI.Middlewares
{
    public class AuthorizationTokenCheckMiddleware
    {
        private readonly ILogger<AuthorizationTokenCheckMiddleware> _logger;
        private readonly RequestDelegate _next;

        public AuthorizationTokenCheckMiddleware(RequestDelegate next,
            ILogger<AuthorizationTokenCheckMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            await HandleMiddlewareAsync(context);
        }

        private Task HandleMiddlewareAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                return context.Response.WriteAsync("Error: No token");
            }

            return _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthorizationTokenCheckMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthorizationTokenCheckMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationTokenCheckMiddleware>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GrooveMessengerAPI.Middlewares
{
    public class ClientCheckMiddlewareMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ClientCheckMiddlewareMiddleware> _logger;
        private IConfiguration _config;

        public ClientCheckMiddlewareMiddleware(RequestDelegate next, ILogger<ClientCheckMiddlewareMiddleware> logger, IConfiguration config)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
            _config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            await HandleMiddlewareAsync(context);
        }

        private Task HandleMiddlewareAsync(HttpContext context)
        {
            var origin = context.Request.Headers["Origin"];
            var validClients = _config.GetSection("Client").Value;
            var server = _config.GetSection("Server").Value;

            if (server.Equals(origin))
            {
                return this._next(context);
            }
            if (!string.IsNullOrEmpty(origin) && !validClients.Equals(origin))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return context.Response.WriteAsync("Error: Not valid client");
            }
            return this._next(context);
        }

    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ClientCheckMiddlewareMiddlewareExtensions
    {
        public static IApplicationBuilder UseClientCheckMiddlewareMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ClientCheckMiddlewareMiddleware>();
        }
    }
}

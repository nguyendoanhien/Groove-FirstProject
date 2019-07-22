using GrooveMessengerAPI.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace GrooveMessengerAPI
{
    public partial class Startup
    {
        public void RegisterMiddlewares(IApplicationBuilder builder)
        {
            //builder.UseAuthorizationTokenCheckMiddleware();
            builder.UseClientCheckMiddlewareMiddleware();
            builder.UseErrorHandlingExceptionMiddleware();
        }
    }
}
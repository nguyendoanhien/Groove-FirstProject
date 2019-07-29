using GrooveMessengerAPI.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace GrooveMessengerAPI
{
    public partial class Startup
    {
        public void RegisterMiddlewares(IApplicationBuilder builder, IHostingEnvironment env)
        {
            //builder.UseAuthorizationTokenCheckMiddleware();
            if (!env.IsDevelopment()) builder.UseClientCheckMiddlewareMiddleware();
            builder.UseErrorHandlingExceptionMiddleware();
        }
    }
}
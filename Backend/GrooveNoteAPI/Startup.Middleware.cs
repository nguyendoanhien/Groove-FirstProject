using GrooveNoteAPI.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace GrooveNoteAPI
{
    public partial class Startup
    {
        public void RegisterMiddlewares(IApplicationBuilder builder)
        {
            //builder.UseAuthorizationTokenCheckMiddleware();
            builder.UseClientCheckMiddlewareMiddleware();
        }
    }
}
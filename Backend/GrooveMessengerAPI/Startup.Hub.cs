using GrooveMessengerAPI.Hubs;
using Microsoft.AspNetCore.Builder;

namespace GrooveMessengerAPI
{
    public partial class Startup
    {
        public void RegisterHub(IApplicationBuilder app)
        {
            app.UseSignalR(routes =>
            {
                routes.MapHub<MessageHub>("/messagehub");
                routes.MapHub<UserProfileHub>("/profilehub");
                routes.MapHub<ContactHub>("/contacthub");

            });
        }
    }
}
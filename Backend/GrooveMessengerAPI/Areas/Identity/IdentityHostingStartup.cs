using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(GrooveMessengerAPI.Areas.Identity.IdentityHostingStartup))]
namespace GrooveMessengerAPI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
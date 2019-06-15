using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(GrooveNoteAPI.Areas.Identity.IdentityHostingStartup))]
namespace GrooveNoteAPI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
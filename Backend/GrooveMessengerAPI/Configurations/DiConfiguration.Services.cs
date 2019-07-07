using GrooveMessengerAPI.Services;
using Microsoft.Extensions.DependencyInjection;
using GrooveMessengerDAL.Services;
using GrooveMessengerDAL.Services.Interface;

namespace GrooveMessengerAPI.Configurations
{
    public partial class DiConfiguration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<IUserResolverService, UserResolverService>();
            services.AddScoped<INoteService, NoteService>();
        }
    }
}
using GrooveMessengerAPI.Hubs.Utils;
using GrooveMessengerAPI.Services;
using GrooveMessengerDAL.Services;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IConversationService, ConversationService>();
            services.AddScoped<IParticipantService, ParticipantService>();
            services.AddScoped<IUserService, UserService>();

            services.AddSingleton(typeof(HubConnectionStore<>), typeof(HubConnectionStore<>));
        }
    }
}
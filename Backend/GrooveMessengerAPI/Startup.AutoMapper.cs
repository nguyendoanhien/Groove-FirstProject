using AutoMapper;
using GrooveMessengerDAL.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace GrooveMessengerAPI
{
    public partial class Startup
    {
        public void RegisterAutoMapperProfiles(IServiceCollection services)
        {

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new NoteAutoMapperProfile());
                mc.AddProfile(new UserAutoMapperProfile());
                mc.AddProfile(new ConversationAutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }        
    }
}
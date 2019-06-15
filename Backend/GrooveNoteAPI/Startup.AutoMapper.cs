using AutoMapper;
using GrooveNoteDAL.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace GrooveNoteAPI
{
    public partial class Startup
    {
        public void RegisterAutoMapperProfiles(IServiceCollection services)
        {

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new NoteAutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }        
    }
}
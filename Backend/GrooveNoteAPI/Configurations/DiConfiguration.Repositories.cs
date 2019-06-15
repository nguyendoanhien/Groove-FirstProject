using Microsoft.Extensions.DependencyInjection;
using GrooveNoteDAL.Repositories;
using GrooveNoteDAL.Repositories.Interface;

namespace GrooveNoteAPI.Configurations
{
    public partial class DiConfiguration
    {
        public static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<,,>), typeof(GenericRepository<,,>));
        }
    }
}

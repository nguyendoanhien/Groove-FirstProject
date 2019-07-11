using Microsoft.Extensions.DependencyInjection;
using GrooveMessengerDAL.Repositories;
using GrooveMessengerDAL.Repositories.Interface;

namespace GrooveMessengerAPI.Configurations
{
    public partial class DiConfiguration
    {
        public static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<,,>), typeof(GenericRepository<,,>));
        }
    }
}

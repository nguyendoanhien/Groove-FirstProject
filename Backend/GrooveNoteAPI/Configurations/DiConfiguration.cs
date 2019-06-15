using Microsoft.Extensions.DependencyInjection;

namespace GrooveNoteAPI.Configurations
{
    public partial class DiConfiguration
    {
        public static void Register(IServiceCollection services)
        {
            RegisterRepositories(services);
            RegisterUows(services);
            RegisterServices(services);
        }
    }
}

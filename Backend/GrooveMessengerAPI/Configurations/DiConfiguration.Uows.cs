using Microsoft.Extensions.DependencyInjection;
using GrooveMessengerDAL.Uow;
using GrooveMessengerDAL.Uow.Interface;

namespace GrooveMessengerAPI.Configurations
{
    public partial class DiConfiguration
    {
        public static void RegisterUows(IServiceCollection services)
        {
            services.AddScoped(typeof(IUowBase<>), typeof(UowBase<>));
        }
    }
}

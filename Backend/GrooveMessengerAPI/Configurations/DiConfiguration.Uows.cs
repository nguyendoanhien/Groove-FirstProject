using GrooveMessengerDAL.Uow;
using GrooveMessengerDAL.Uow.Interface;
using Microsoft.Extensions.DependencyInjection;

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
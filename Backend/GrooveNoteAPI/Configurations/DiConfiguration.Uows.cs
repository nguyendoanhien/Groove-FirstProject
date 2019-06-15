using Microsoft.Extensions.DependencyInjection;
using GrooveNoteDAL.Uow;
using GrooveNoteDAL.Uow.Interface;

namespace GrooveNoteAPI.Configurations
{
    public partial class DiConfiguration
    {
        public static void RegisterUows(IServiceCollection services)
        {
            services.AddScoped(typeof(IUowBase<>), typeof(UowBase<>));
        }
    }
}

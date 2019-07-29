using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace GrooveMessengerAPI
{
    public partial class Startup
    {
        public void RegisterRouting(IRouteBuilder routes)
        {
            //routes.MapAreaRoute("BusinessData", "Data",
            //   "Data/{controller}/{action?}/{id?}");

            //routes.MapRoute(
            //    name: "MyArea",
            //    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");                     

            routes.MapRoute(
                "default",
                "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
using Microsoft.AspNetCore.Builder;

namespace GrooveNoteAPI
{
    public partial class Startup
    {
        public void RegisterRouting(Microsoft.AspNetCore.Routing.IRouteBuilder routes)
        {

            //routes.MapAreaRoute("BusinessData", "Data",
            //   "Data/{controller}/{action?}/{id?}");

            //routes.MapRoute(
            //    name: "MyArea",
            //    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");                     

            routes.MapRoute(
               name: "default",
               template: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
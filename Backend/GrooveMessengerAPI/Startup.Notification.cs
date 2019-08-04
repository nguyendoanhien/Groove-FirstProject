using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPush;

namespace GrooveMessengerAPI
{
    public partial class Startup
    {
        public void RegisterNotification(IServiceCollection services)
        {
            // It load configures from log4net.config
            var vapidDetails = new VapidDetails(
          Configuration.GetValue<string>("VapidDetails:Subject"),
          Configuration.GetValue<string>("VapidDetails:PublicKey"),
          Configuration.GetValue<string>("VapidDetails:PrivateKey"));
            services.AddTransient(c => vapidDetails);
        }
    }
}

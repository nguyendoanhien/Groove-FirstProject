using GrooveMessengerDAL.Data;
using System;

namespace GrooveMessengerAPI
{
    public partial class Startup
    {
        public void SeedRootUserDatabase(IServiceProvider serviceProvider)
        {
            SeedDatabase.Initialize(serviceProvider);
        }
    }
}

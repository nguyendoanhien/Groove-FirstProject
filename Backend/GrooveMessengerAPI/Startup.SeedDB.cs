using System;
using GrooveMessengerDAL.Data;

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
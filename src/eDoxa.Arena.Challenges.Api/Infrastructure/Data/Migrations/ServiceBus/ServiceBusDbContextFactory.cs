// Filename: IntegrationEventDbContextFactory.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;

using eDoxa.Seedwork.Infrastructure.Factories;
using eDoxa.Seedwork.IntegrationEvents.Infrastructure;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Migrations.ServiceBus
{
    internal sealed class ServiceBusDbContextFactory : DesignTimeDbContextFactory<ServiceBusDbContext>
    {
        protected override string BasePath => Directory.GetCurrentDirectory();

        protected override Assembly MigrationsAssembly => Assembly.GetAssembly(typeof(Startup));

        
        public override ServiceBusDbContext CreateDbContext(string[] args)
        {
            return new ServiceBusDbContext(Options);
        }
    }
}

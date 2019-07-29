// Filename: IntegrationEventDbContextFactory.cs
// Date Created: 2019-06-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.IO;
using System.Reflection;

using eDoxa.Seedwork.Infrastructure.Factories;
using eDoxa.Seedwork.ServiceBus.Infrastructure;

namespace eDoxa.Identity.Api.Infrastructure.Data.Migrations.ServiceBus
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

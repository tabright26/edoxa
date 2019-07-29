// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Reflection;

using eDoxa.Seedwork.IntegrationEvents.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.IntegrationEvents.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIntegrationEventDbContext(this IServiceCollection services, string connectionString, Assembly migrationsAssembly)
        {
            services.AddDbContext<ServiceBusDbContext>(
                options => options.UseSqlServer(
                    connectionString,
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsAssembly(migrationsAssembly.GetName().Name);
                        sqlServerOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                    }
                )
            );
        }
    }
}

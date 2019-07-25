// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Reflection;

using eDoxa.IntegrationEvents.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.IntegrationEvents.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIntegrationEventDbContext(this IServiceCollection services, string connectionString, Assembly migrationsAssembly)
        {
            services.AddDbContext<IntegrationEventDbContext>(
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
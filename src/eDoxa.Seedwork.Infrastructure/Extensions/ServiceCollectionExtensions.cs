// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDbContext<TDbContext>(this IServiceCollection services, string connectionString, Assembly assembly)
        where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(
                options => options.UseSqlServer(
                    connectionString,
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsAssembly(assembly.GetName().Name);
                        sqlServerOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                    }
                )
            );
        }

        public static void AddDbContext<TDbContext, TDbContextData>(this IServiceCollection services, string connectionString, Assembly assembly)
        where TDbContext : DbContext
        where TDbContextData : class, IDbContextData
        {
            services.AddDbContext<TDbContext>(connectionString, assembly);

            services.AddScoped<IDbContextData, TDbContextData>();
        }
    }
}

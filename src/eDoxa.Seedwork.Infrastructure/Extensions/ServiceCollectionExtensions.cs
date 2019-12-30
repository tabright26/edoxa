// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-12-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Reflection;

using eDoxa.Storage.Azure.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomDbContext<TDbContext>(
            this IServiceCollection services,
            IConfiguration configuration,
            Assembly? migrationsAssembly = null,
            int maxRetryCount = 10,
            TimeSpan? maxRetryDelay = null
        )
        where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(
                builder => builder.UseSqlServer(
                    configuration.GetSqlServerConnectionString(),
                    options =>
                    {
                        options.MigrationsAssembly(migrationsAssembly?.GetName().Name ?? Assembly.GetAssembly(typeof(TDbContext))!.GetName().Name);

                        options.EnableRetryOnFailure(maxRetryCount, maxRetryDelay ?? TimeSpan.FromSeconds(30), null);
                    }));
        }

        public static void AddCustomAzureStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAzureStorage(configuration.GetAzureBlobStorageConnectionString());
        }
    }
}

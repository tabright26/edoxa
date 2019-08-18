// Filename: WebHostExtensions.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Data.SqlClient;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Polly;

namespace eDoxa.Seedwork.Infrastructure.Extensions
{
    public static class WebHostExtensions
    {
        public static void MigrateDbContextWithRetryPolicy<TDbContext>(this IWebHost host)
        where TDbContext : DbContext
        {
            host.MigrateDbContextWithRetryPolicy<TDbContext, IDbContextSeeder>();
        }

        private static void MigrateDbContextWithRetryPolicy<TDbContext, TDbContextData>(this IWebHost host)
        where TDbContext : DbContext
        where TDbContextData : IDbContextSeeder
        {
            // Create service scope.
            using (var scope = host.Services.CreateScope())
            {
                // Gets service provider.
                var provider = scope.ServiceProvider;

                // Gets database context logger service.
                var logger = provider.GetRequiredService<ILogger<TDbContext>>();

                // Gets database context service.
                var context = provider.GetService<TDbContext>();

                try
                {
                    logger.LogInformation($"Migrating database associated with context {typeof(TDbContext).Name}.");

                    // Try to retrieved the database policy.
                    var policy = Policy.Handle<SqlException>()
                        .WaitAndRetry(new[] {TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(15)});

                    // Execute the database commands if the policy is retrieved.
                    policy.Execute(
                        () =>
                        {
                            // Migrated database associated with context.
                            context.Database.Migrate();

                            // Seed context data to database.
                            provider.GetService<TDbContextData>()?.SeedAsync().Wait();
                        }
                    );

                    logger.LogInformation($"Migrated database associated with context {typeof(TDbContext).Name}.");
                }
                catch (Exception exception) // An error occurred while migrating the database used on context.
                {
                    logger.LogError(exception, $"An error occurred while migrating the database used on context {typeof(TDbContext).Name}.");
                }
            }
        }
    }
}

﻿// Filename: Program.cs
// Date Created: 2019-03-28
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Identity.Infrastructure;
using eDoxa.Identity.Infrastructure.Seeders;
using eDoxa.Monitoring.Extensions;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.ServiceBus;

using IdentityServer4.EntityFramework.DbContexts;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Serilog;

namespace eDoxa.Identity
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                Log.Information("Building {Application} web host...");

                var host = CreateWebHostBuilder(args).Build();

                Log.Information("Applying {Application} context migrations...");

                host.MigrateDbContext<PersistedGrantDbContext>(
                    (context, provider) =>
                    {
                    }
                );

                host.MigrateDbContext<IdentityDbContext>(
                    (context, provider) =>
                    {
                        var environment = provider.GetService<IHostingEnvironment>();

                        if (environment.IsDevelopment())
                        {
                            var factory = provider.GetService<ILoggerFactory>();

                            var task = context.SeedAsync(factory.CreateLogger<IdentityDbContext>());

                            task.Wait();
                        }
                    }
                );

                host.MigrateDbContext<IntegrationEventLogDbContext>(
                    (context, provider) =>
                    {
                    }
                );

                host.MigrateDbContext<ConfigurationDbContext>(
                    (context, provider) =>
                    {
                        var seeder = provider.GetService<ConfigurationDbContextSeeder>();

                        seeder.SeedAsync(context).Wait();
                    }
                );

                Log.Information("Starting {Application} web host...");

                host.Run();

                return 0;
            }
            catch (Exception exception)
            {
                Log.Fatal(exception, "Program '{Application}' exited with code 1.");

                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder<Startup>(args)
                          .CaptureStartupErrors(false)
                          .ConfigureLogging()
                          .UseHealthChecks()
                          .UseAzureKeyVault()
                          .UseApplicationInsights()
                          .UseSerilog();
        }
    }
}
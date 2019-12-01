﻿// Filename: Program.cs
// Date Created: 2019-09-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Autofac.Extensions.DependencyInjection;

using eDoxa.Challenges.Infrastructure;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Monitoring.Serilog.Extensions;
using eDoxa.Seedwork.Security.AzureKeyVault.Extensions;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Serilog;

namespace eDoxa.Challenges.Api
{
    public sealed class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var builder = CreateWebHostBuilder(args);

                Log.Information("Building {Application} host...");

                var host = builder.Build();

                Log.Information("Applying {Application} context migrations...");

                host.MigrateDbContextWithRetryPolicy<ChallengesDbContext>();

                Log.Information("Starting {Application} host...");

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

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder<Startup>(args)
                .CaptureStartupErrors(false)
                .ConfigureServices(
                    services =>
                    {
                        services.AddApplicationInsightsTelemetry();
                        services.AddAutofac();
                    })
                .UseAzureKeyVault()
                .UseCustomSerilog<Program>();
        }
    }
}

﻿// Filename: Program.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Challenges.Workers.Infrastructure;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Monitoring.ApplicationInsights.Extensions;
using eDoxa.Seedwork.Monitoring.Serilog.Extensions;
using eDoxa.Seedwork.Security.AzureKeyVault.Extensions;
using eDoxa.Seedwork.Security.Kestrel.Extensions;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Serilog;

namespace eDoxa.Challenges.Workers
{
    public sealed class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var builder = CreateWebHostBuilder(args);

                Log.Information("Building {AssemblyName} host...");

                var host = builder.Build();

                Log.Information("Applying {AssemblyName} context migrations...");

                var context = host.Services.GetRequiredService<HangfireDbContext>();

                context.Database.EnsureCreated();

                Log.Information("Starting {AssemblyName} host...");

                host.Run();

                return 0;
            }
            catch (Exception exception)
            {
                Log.Fatal(exception, "Program '{AssemblyName}' exited with code 1.");

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
                .ConfigureKestrel(options => options.ListenRest())
                .UseCustomAutofac()
                .UseCustomAzureKeyVault()
                .UseCustomApplicationInsights()
                .UseCustomSerilog();
        }
    }
}
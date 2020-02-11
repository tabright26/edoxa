// Filename: Program.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Clans.Infrastructure;
using eDoxa.Seedwork.Application.ApplicationInsights.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.Serilog.Extensions;
using eDoxa.Seedwork.Application.Server.Kestrel.Extensions;
using eDoxa.Seedwork.Infrastructure.AzureKeyVault.Extensions;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Serilog;

namespace eDoxa.Clans.Api
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

                host.MigrateDbContextWithRetryPolicy<ClansDbContext>();

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
                .ConfigureKestrel(
                    options =>
                    {
                        options.ListenRest();
                        options.ListenGrpc();
                    })
                .UseCustomAutofac()
                .UseCustomAzureKeyVault()
                .UseCustomApplicationInsights()
                .UseCustomSerilog();
        }
    }
}

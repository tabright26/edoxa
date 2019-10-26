// Filename: Program.cs
// Date Created: 2019-09-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Autofac.Extensions.DependencyInjection;

using eDoxa.Organizations.Clans.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Security.Extensions;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Serilog;

namespace eDoxa.Organizations.Clans.Api
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var builder = CreateWebHostBuilder(args);

                Log.Information("Building {Application} host...");

                var host = builder.Build();

                Log.Information("Applying {Application} context migrations...");

                host.MigrateDbContextWithRetryPolicy<ClansDbContext>();

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
                .UseSerilog(
                    (context, config) =>
                    {
                        var seqServerUrl = context.Configuration["Serilog:Sink:Seq"];

                        config.MinimumLevel.Verbose()
                            .Enrich.WithProperty("Application", typeof(Program).Namespace)
                            .Enrich.FromLogContext()
                            .WriteTo.Console()
                            .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
                            .ReadFrom.Configuration(context.Configuration);
                    });
        }
    }
}

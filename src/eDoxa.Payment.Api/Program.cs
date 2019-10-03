// Filename: Program.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Serilog;

namespace eDoxa.Payment.Api
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
                .ConfigureAppConfiguration(
                    config =>
                    {
                        var configuration = config.Build();

                        var builder = new ConfigurationBuilder();

                        builder.AddAzureKeyVault(
                            $"https://{configuration["AzureKeyVault:Name"]}.vault.azure.net",
                            configuration["AzureKeyVault:ClientId"],
                            configuration["AzureKeyVault:ClientSecret"]);

                        config.AddConfiguration(builder.Build());
                    })
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

// Filename: Program.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Serilog;

namespace eDoxa.Web.Spa
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateWebHostBuilder(args);

            var host = builder.Build();

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder<Startup>(args)
                .ConfigureServices(services => services.AddApplicationInsightsTelemetry())
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

                        config.MinimumLevel.Information()
                            .Enrich.WithProperty("Application", typeof(Program).Namespace)
                            .Enrich.FromLogContext()
                            .WriteTo.Console()
                            .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
                            .ReadFrom.Configuration(context.Configuration);
                    });
        }
    }
}

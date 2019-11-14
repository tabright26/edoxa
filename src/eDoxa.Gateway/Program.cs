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

namespace eDoxa.Gateway
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
            var builder = WebHost.CreateDefaultBuilder<Startup>(args);

            builder.ConfigureServices(
                    services =>
                    {
                        services.AddApplicationInsightsTelemetry();
                        services.AddSingleton(builder);
                    })
                .ConfigureAppConfiguration(config => config.AddJsonFile("ocelot.json", false, true))
                .UseSerilog(
                    (context, config) =>
                    {
                        var seqServerUrl = context.Configuration["Serilog:Sink:Seq"];

                        config.MinimumLevel.Information()
                            .Enrich.FromLogContext()
                            .WriteTo.Console()
                            .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl);
                    });

            return builder;
        }
    }
}

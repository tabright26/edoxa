// Filename: Program.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Serilog;

namespace eDoxa.Web.Gateway
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

            builder.ConfigureServices(services => services.AddSingleton(builder))
                .ConfigureAppConfiguration(config => config.AddJsonFile("ocelot.json", false, true))
                .UseApplicationInsights()
                .UseSerilog(
                    (context, config) => config.MinimumLevel.Information()
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .WriteTo.Seq(context.Configuration["Serilog:Seq"])
                );

            return builder;
        }
    }
}

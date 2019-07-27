// Filename: Program.cs
// Date Created: 2019-06-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
                .ConfigureAppConfiguration(configurationBuilder => configurationBuilder.AddJsonFile("ocelot.json", false, true))
                .ConfigureLogging(
                    (context, loggingbuilder) =>
                    {
                        loggingbuilder.AddConfiguration(context.Configuration.GetSection("Logging"));
                        loggingbuilder.AddConsole();
                        loggingbuilder.AddDebug();
                    }
                )
                .UseSerilog(
                    (context, loggerConfiguration) =>
                    {
                        loggerConfiguration.MinimumLevel.Information().Enrich.FromLogContext().WriteTo.Console();
                    }
                );

            return builder;
        }
    }
}

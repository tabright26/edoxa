// Filename: Program.cs
// Date Created: 2019-06-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

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
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args);

            builder.ConfigureServices(services => services.AddSingleton(builder))
                .ConfigureAppConfiguration(configurationBuilder => configurationBuilder.AddJsonFile("ocelot.json", false, true))
                .UseStartup<Startup>()
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

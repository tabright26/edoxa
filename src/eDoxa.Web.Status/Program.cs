// Filename: Program.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Security.Extensions;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Serilog;

namespace eDoxa.Web.Status
{
    public static class Program
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
                .UseApplicationInsights()
                .UseSerilog(
                    (context, config) => config.MinimumLevel.Verbose().Enrich.WithProperty("Application", typeof(Program).Namespace).Enrich.FromLogContext().WriteTo.Console().ReadFrom.Configuration(context.Configuration)
                );
        }
    }
}

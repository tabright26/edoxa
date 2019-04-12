// Filename: Program.cs
// Date Created: 2019-04-11
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Monitoring.Extensions;
using eDoxa.Security.Extensions;

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
                Log.Information("Building {Application} web host...");

                var host = CreateWebHostBuilder(args).Build();

                Log.Information("Starting {Application} web host...");

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

        private static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder<Startup>(args)
                          .CaptureStartupErrors(false)
                          .ConfigureLogging()
                          .UseAzureKeyVault()
                          .UseApplicationInsights()
                          .UseSerilog();
        }
    }
}
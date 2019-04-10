// Filename: Program.cs
// Date Created: 2019-03-25
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Security.Extensions;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

namespace eDoxa.Web.Spa
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            host.Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder<Startup>(args)
                          .ConfigureLogging(
                              (context, builder) =>
                              {
                                  builder.AddSerilog();
                                  builder.AddAzureWebAppDiagnostics();
                                  builder.AddApplicationInsights(context.Configuration["ApplicationInsights:InstrumentationKey"]);
                              }
                          )
                          .UseAzureKeyVault()
                          .UseApplicationInsights()
                          .UseSerilog(
                              (_, config) =>
                              {
                                  config.MinimumLevel.Information().Enrich.FromLogContext().WriteTo.Console();
                              }
                          );
        }
    }
}
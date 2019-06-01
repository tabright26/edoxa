// Filename: WebHostBuilderExtensions.cs
// Date Created: 2019-04-12
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder ConfigureLogging(this IWebHostBuilder builder)
        {
            return builder.ConfigureLogging(
                (context, loggingBuilder) =>
                {
                    loggingBuilder.AddSerilog();
                    loggingBuilder.AddAzureWebAppDiagnostics();
                    loggingBuilder.AddApplicationInsights(context.Configuration["ApplicationInsights:InstrumentationKey"]);
                }
            );
        }

        public static IWebHostBuilder UseSerilog(this IWebHostBuilder builder)
        {
            return builder.UseSerilog(
                (context, config) =>
                {
                    config.MinimumLevel.Verbose().Enrich.FromLogContext().WriteTo.Console().ReadFrom.Configuration(context.Configuration);
                }
            );
        }
    }
}
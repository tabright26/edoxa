// Filename: WebHostBuilderExtensions.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace eDoxa.Monitoring.Extensions
{
    public static class WebHostBuilderExtensions
    {
        private const string DefaultEndpoint = "/HealthCheck";

        public static IWebHostBuilder UseHealthChecks(this IWebHostBuilder builder)
        {
            return builder.UseHealthChecks(DefaultEndpoint);
        }

        public static IWebHostBuilder UseSerilog(this IWebHostBuilder builder)
        {
            return builder.UseSerilog(
                (context, config) =>
                {
                    config.ReadFrom.Configuration(context.Configuration);
                    config.Enrich.FromLogContext().WriteTo.Console();
                }
            );
        }
    }
}
// Filename: WebHostBuilderExtensions.cs
// Date Created: 2019-11-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Hosting;

using Serilog;

namespace eDoxa.Seedwork.Monitoring.Serilog.Extensions
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseCustomSerilog<TProgram>(this IWebHostBuilder builder)
        {
            return builder.UseSerilog(
                (context, configuration) =>
                {
                    configuration.MinimumLevel.Verbose();

                    configuration.Enrich.WithProperty("Application", typeof(TProgram).Namespace);

                    configuration.Enrich.FromLogContext();

                    configuration.WriteTo.Console();

                    var serverUrl = context.Configuration["Serilog:Sink:Seq"];

                    if (!string.IsNullOrWhiteSpace(serverUrl))
                    {
                        configuration.WriteTo.Seq(serverUrl);
                    }

                    configuration.ReadFrom.Configuration(context.Configuration);
                });
        }
    }
}

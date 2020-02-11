// Filename: WebHostBuilderExtensions.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Hosting;

using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.Destructurers;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using Serilog.Exceptions.SqlServer.Destructurers;

namespace eDoxa.Seedwork.Application.Serilog.Extensions
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseCustomSerilog(this IWebHostBuilder builder)
        {
            return builder.ConfigureLogging(loggingBuilder => loggingBuilder.AddSerilog())
                .UseSerilog(
                    (context, configuration) =>
                    {
                        configuration.MinimumLevel.Verbose();

                        configuration.Enrich.FromLogContext();

                        configuration.Enrich.WithAssemblyName();

                        configuration.Enrich.WithAssemblyVersion();

                        configuration.Enrich.WithProcessId();

                        configuration.Enrich.WithProcessName();

                        configuration.Enrich.WithThreadId();

                        configuration.Enrich.WithThreadName();

                        configuration.Enrich.WithCorrelationId();

                        configuration.Enrich.WithCorrelationIdHeader();

                        configuration.Enrich.WithMemoryUsage();

                        configuration.Enrich.WithMachineName();

                        //configuration.Enrich.WithHttpContext();

                        configuration.Enrich.WithExceptionDetails(
                            new DestructuringOptionsBuilder().WithDefaultDestructurers()
                                .WithDestructurers(new IExceptionDestructurer[] {new SqlExceptionDestructurer(), new DbUpdateExceptionDestructurer()}));

                        configuration.WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");

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

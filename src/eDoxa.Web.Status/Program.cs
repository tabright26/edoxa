﻿// Filename: Program.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Application.ApplicationInsights.Extensions;
using eDoxa.Seedwork.Application.Serilog.Extensions;
using eDoxa.Seedwork.Application.Server.Kestrel.Extensions;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Serilog;

namespace eDoxa.Web.Status
{
    public sealed class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var builder = CreateWebHostBuilder(args);

                Log.Information("Building {AssemblyName} host...");

                var host = builder.Build();

                Log.Information("Starting {AssemblyName} host...");

                host.Run();

                return 0;
            }
            catch (Exception exception)
            {
                Log.Fatal(exception, "Program '{AssemblyName}' exited with code 1.");

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
                .ConfigureKestrel(options => options.ListenRest())
                .UseCustomApplicationInsights()
                .UseCustomSerilog();
        }
    }
}

﻿// Filename: Program.cs
// Date Created: 2019-07-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Autofac.Extensions.DependencyInjection;

using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Security.Extensions;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Serilog;

namespace eDoxa.Payment.Api
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var builder = CreateWebHostBuilder(args);

                Log.Information("Building {Application} web host...");

                var host = builder.Build();

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

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder<Startup>(args)
                .ConfigureServices(services => services.AddAutofac())
                .CaptureStartupErrors(false)
                .ConfigureLogging()
                .UseAzureKeyVault()
                .UseApplicationInsights()
                .UseSerilog();
        }
    }
}

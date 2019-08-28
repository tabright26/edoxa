// Filename: Program.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Serilog;

namespace eDoxa.Web.Spa
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateWebHostBuilder(args);

            var host = builder.Build();

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder<Startup>(args)
                .UseApplicationInsights()
                .UseSerilog(
                    (context, config) => config.MinimumLevel.Information()
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .WriteTo.Seq(context.Configuration["Serilog:Seq"])
                );
        }
    }
}

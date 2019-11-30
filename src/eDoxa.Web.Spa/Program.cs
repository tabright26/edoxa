// Filename: Program.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Monitoring.Serilog.Extensions;
using eDoxa.Seedwork.Security.AzureKeyVault.Extensions;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Web.Spa
{
    public sealed class Program
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
                .ConfigureServices(services => services.AddApplicationInsightsTelemetry())
                .UseAzureKeyVault()
                .UseCustomSerilog<Program>();
        }
    }
}

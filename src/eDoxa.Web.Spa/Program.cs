// Filename: Program.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

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
                .ConfigureAppConfiguration(
                    config =>
                    {
                        var configuration = config.Build();

                        var builder = new ConfigurationBuilder();

                        builder.AddAzureKeyVault(
                            $"https://{configuration["AzureKeyVault:Name"]}.vault.azure.net",
                            configuration["AzureKeyVault:ClientId"],
                            configuration["AzureKeyVault:ClientSecret"]);

                        config.AddConfiguration(builder.Build());
                    })
                .UseApplicationInsights()
                .UseSerilog(
                    (context, config) => config.MinimumLevel.Information()
                        .Enrich.WithProperty("Application", typeof(Program).Namespace)
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .WriteTo.Seq(context.Configuration["Serilog:Seq"])
                        .ReadFrom.Configuration(context.Configuration));
        }
    }
}

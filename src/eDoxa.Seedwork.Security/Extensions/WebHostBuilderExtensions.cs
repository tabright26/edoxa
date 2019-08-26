// Filename: WebHostBuilderExtensions.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Seedwork.Security.Extensions
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseAzureKeyVault(this IWebHostBuilder webHostBuilder)
        {
            return webHostBuilder.ConfigureAppConfiguration(
                config =>
                {
                    var configuration = config.Build();

                    var builder = new ConfigurationBuilder();

                    builder.AddAzureKeyVault(
                        $"https://{configuration["AzureKeyVault:Name"]}.vault.azure.net",
                        configuration["AzureKeyVault:ClientId"],
                        configuration["AzureKeyVault:ClientSecret"]
                    );

                    config.AddConfiguration(builder.Build());
                }
            );
        }
    }
}

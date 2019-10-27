// Filename: WebHostBuilderExtensions.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Seedwork.Security.Extensions
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseAzureKeyVault(this IWebHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureAppConfiguration(
                configurationBuilder =>
                {
                    var configurationRoot = configurationBuilder.Build();

                    configurationBuilder.AddConfiguration(
                        new ConfigurationBuilder().AddAzureKeyVault(
                                $"https://{configurationRoot["AzureKeyVault:Name"]}.vault.azure.net",
                                configurationRoot["AzureKeyVault:ClientId"],
                                configurationRoot["AzureKeyVault:ClientSecret"])
                            .Build());
                });
        }
    }
}

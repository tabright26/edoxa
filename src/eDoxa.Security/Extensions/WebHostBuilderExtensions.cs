// Filename: WebHostBuilderExtensions.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Security.Extensions
{
    public static class WebHostBuilderExtensions
    {
        private const string AzureKeyVaultName = "AzureKeyVault:Name";
        private const string AzureKeyVaultClientId = "AzureKeyVault:ClientId";
        private const string AzureKeyVaultClientSecret = "AzureKeyVault:ClientSecret";

        public static IWebHostBuilder UseAzureKeyVault(this IWebHostBuilder webHostBuilder)
        {
            return webHostBuilder.ConfigureAppConfiguration(
                config =>
                {
                    var configuration = config.Build();

                    var builder = new ConfigurationBuilder();

                    var vault = $"https://{configuration[AzureKeyVaultName]}.vault.azure.net";

                    builder.AddAzureKeyVault(vault, configuration[AzureKeyVaultClientId], configuration[AzureKeyVaultClientSecret]);

                    config.AddConfiguration(builder.Build());
                }
            );
        }
    }
}
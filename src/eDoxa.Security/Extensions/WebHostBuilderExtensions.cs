// Filename: WebHostBuilderExtensions.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Security.Extensions
{
    public static class WebHostBuilderExtensions
    {
        private const string KeyVaultFromConfig = "AzureKeyVault:Name";
        private const string KeyVaultClientIdFromConfig = "AzureKeyVault:ClientId";
        private const string KeyVaultClientSecretFromConfig = "AzureKeyVault:ClientSecret";

        public static IWebHostBuilder UseAzureKeyVault(this IWebHostBuilder webHostBuilder)
        {
            return webHostBuilder.ConfigureAppConfiguration(
                config =>
                {
                    var configuration = config.Build();

                    var builder = new ConfigurationBuilder();

                    builder.AddAzureKeyVault(
                        configuration[KeyVaultFromConfig],
                        configuration[KeyVaultClientIdFromConfig],
                        configuration[KeyVaultClientSecretFromConfig]
                    );

                    config.AddConfiguration(builder.Build());
                }
            );
        }
    }
}
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
        private const string KeyVaultConfigurationEnabled = "ASPNETCORE_HOSTINGSTARTUP:KEYVAULT:CONFIGURATIONENABLED";
        private const string KeyVaultConfigurationVault = "ASPNETCORE_HOSTINGSTARTUP:KEYVAULT:CONFIGURATIONVAULT";
        private const string KeyVaultClientId = "KeyVault:ClientId";
        private const string KeyVaultClientSecret = "KeyVault:ClientSecret";

        public static IWebHostBuilder UseAzureKeyVault(this IWebHostBuilder webHostBuilder)
        {
            return webHostBuilder.ConfigureAppConfiguration(
                config =>
                {
                    var configuration = config.Build();

                    var builder = new ConfigurationBuilder();

                    if (configuration.GetValue<bool>(KeyVaultConfigurationEnabled))
                    {
                        builder.AddAzureKeyVault(
                            configuration[KeyVaultConfigurationVault],
                            configuration[KeyVaultClientId],
                            configuration[KeyVaultClientSecret]
                        );
                    }

                    config.AddConfiguration(builder.Build());
                }
            );
        }
    }
}
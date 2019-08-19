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
        private const string AzureKeyVaultName = "AzureKeyVault:Name";
        private const string AzureKeyVaultClientId = "AzureKeyVault:ClientId";
        private const string AzureKeyVaultClientSecret = "AzureKeyVault:ClientSecret";

        // TODO: Must be cleaner.
        // TODO: Add a validation to ensure the connection between Azure Key Vault and the service host.
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

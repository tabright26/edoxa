// Filename: WebHostBuilderExtensions.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Seedwork.Infrastructure.AzureKeyVault.Extensions
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseCustomAzureKeyVault(this IWebHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureAppConfiguration(
                (context, builder) =>
                {
                    var connectionString = new AzureKeyVaultConnectionStringBuilder(context.Configuration.GetConnectionString("AzureKeyVault"));

                    builder.AddAzureKeyVault($"https://{connectionString.Name}.vault.azure.net", connectionString.ClientId, connectionString.ClientSecret);
                });
        }
    }
}

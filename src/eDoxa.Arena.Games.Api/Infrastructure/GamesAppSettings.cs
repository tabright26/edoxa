// Filename: GamesAppSettings.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;

using IdentityServer4.Models;

namespace eDoxa.Arena.Games.Api.Infrastructure
{
    public sealed class GamesAppSettings : IHasAzureKeyVaultAppSettings, IHasApiResourceAppSettings
    {
        [Required]
        public ConnectionStrings ConnectionStrings { get; set; }

        [Required]
        public HttpClientsOptions HttpClients { get; set; }

        [Required]
        public ApiResource ApiResource { get; set; }

        [Required]
        public AuthorityOptions Authority { get; set; }

        [Required]
        public AzureKeyVaultOptions AzureKeyVault { get; set; }
    }

    public sealed class ConnectionStrings : IHasSqlServerConnectionString, IHasRedisConnectionString
    {
        [Required]
        public string Redis { get; set; }

        [Required]
        public string SqlServer { get; set; }
    }

    public sealed class HttpClientsOptions
    {
        [Required]
        public WebOptions Web { get; set; }
    }

    public sealed class WebOptions
    {
        [Required]
        public string GatewayUrl { get; set; }
    }
}

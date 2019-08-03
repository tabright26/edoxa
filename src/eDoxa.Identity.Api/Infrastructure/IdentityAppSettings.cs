// Filename: IdentityAppSettings.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;

using IdentityServer4.Models;

namespace eDoxa.Identity.Api.Infrastructure
{
    public class IdentityAppSettings : IHasAzureKeyVaultAppSettings, IHasApiResourceAppSettings
    {
        [Required]
        public IdentityServerOptions IdentityServer { get; set; }

        [Required]
        public ConnectionStrings ConnectionStrings { get; set; }

        [Required]
        public ApiResource ApiResource { get; set; }

        [Required]
        public AuthorityOptions Authority { get; set; }

        [Required]
        public AzureKeyVaultOptions AzureKeyVault { get; set; }
    }

    public class ConnectionStrings : IHasSqlServerConnectionString, IHasRedisConnectionString
    {
        [Required]
        public string Redis { get; set; }

        [Required]
        public string SqlServer { get; set; }
    }

    public class IdentityServerOptions
    {
        public string IdentityUrl { get; set; }

        public string CashierUrl { get; set; }

        public string ArenaChallengesUrl { get; set; }

        public WebOptions Web { get; set; }
    }
}

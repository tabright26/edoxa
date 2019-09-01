// Filename: WebSpaAppSettings.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;

namespace eDoxa.Web.Spa.Infrastructure
{
    public class WebSpaAppSettings : IHasAzureKeyVaultAppSettings, IHasAuthorityAppSettings
    {
        [Required]
        public HealthChecksOptions HealthChecks { get; set; }

        [Required]
        public ConnectionStrings ConnectionStrings { get; set; }

        [Required]
        public WebOptions Web { get; set; }

        [Required]
        public AuthorityOptions Authority { get; set; }

        [Required]
        public AzureKeyVaultOptions AzureKeyVault { get; set; }
    }

    public class HealthChecksOptions
    {
        [Required]
        public WebOptions Web { get; set; }
    }

    public class ConnectionStrings : IHasRedisConnectionString
    {
        [Required]
        public string Redis { get; set; }
    }

    public class WebOptions
    {
        [Required]
        public string GatewayUrl { get; set; }

        [Required]
        public string ReactUrl { get; set; }
    }
}

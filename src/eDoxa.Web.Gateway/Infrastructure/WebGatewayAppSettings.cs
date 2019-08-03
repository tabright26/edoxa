// Filename: WebGatewayAppSettings.cs
// Date Created: 2019-07-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;

namespace eDoxa.Web.Gateway.Infrastructure
{
    public class WebGatewayAppSettings : IHasAuthorityAppSettings
    {
        [Required]
        public HealthChecksOptions HealthChecks { get; set; }

        [Required]
        public AuthorityOptions Authority { get; set; }
    }

    public class HealthChecksOptions
    {
        [Required]
        public string IdentityUrl { get; set; }

        [Required]
        public string CashierUrl { get; set; }

        [Required]
        public string ArenaChallengesUrl { get; set; }
    }
}

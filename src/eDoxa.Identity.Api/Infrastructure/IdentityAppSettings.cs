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
    public class IdentityAppSettings : IHasApiResourceAppSettings<AuthorityEndpointsOptions>
    {
        [Required]
        public ApiResource ApiResource { get; set; }

        [Required]
        public string Authority { get; set; }

        [Required]
        public string WebSpaProxyUrl { get; set; }

        [Required]
        public AuthorityEndpointsOptions Endpoints { get; set; }

        [Required]
        public SwaggerOptions Swagger { get; set; }
    }

    public sealed class SwaggerOptions
    {
        [Required]
        public bool Enabled { get; set; }

        [Required]
        public SwaggerEndpointsOptions Endpoints { get; set; }
    }

    public sealed class SwaggerEndpointsOptions
    {
        [Required]
        public string IdentityUrl { get; set; }

        [Required]
        public string PaymentUrl { get; set; }

        [Required]
        public string CashierUrl { get; set; }

        [Required]
        public string NotificationsUrl { get; set; }

        [Required]
        public string ChallengesUrl { get; set; }

        [Required]
        public string GamesUrl { get; set; }
        
        [Required]
        public string ClansUrl { get; set; }
    }
}

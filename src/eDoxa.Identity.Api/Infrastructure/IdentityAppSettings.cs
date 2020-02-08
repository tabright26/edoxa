// Filename: IdentityAppSettings.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Grpc.Protos.Identity.Options;
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
        public string WebSpaUrl { get; set; }

        [Required]
        public AuthorityEndpointsOptions Endpoints { get; set; }

        [Required]
        public SwaggerOptions Swagger { get; set; }

        [Required]
        public AdministratorOptions Administrator { get; set; }
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
        public string IdentityUrl { get; set; }

        public string PaymentUrl { get; set; }

        public string CashierUrl { get; set; }

        public string NotificationsUrl { get; set; }

        public string ChallengesUrl { get; set; }

        public string GamesUrl { get; set; }
        
        public string ClansUrl { get; set; }

        public string ChallengesWebAggregatorUrl { get; set; }

        public string CashierWebAggregatorUrl { get; set; }
    }
}

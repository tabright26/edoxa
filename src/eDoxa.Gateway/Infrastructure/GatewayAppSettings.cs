// Filename: GatewayAppSettings.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;

namespace eDoxa.Gateway.Infrastructure
{
    public class GatewayAppSettings : IHasAuthorityAppSettings, IHasEndpointsAppSettings<EndpointsOptions>
    {
        [Required]
        public string Authority { get; set; }

        [Required]
        public EndpointsOptions Endpoints { get; set; }
    }

    public sealed class EndpointsOptions : AuthorityEndpointsOptions
    {
        [Required]
        public string CashierUrl { get; set; }

        [Required]
        public string PaymentUrl { get; set; }

        [Required]
        public string ChallengesUrl { get; set; }

        [Required]
        public string GamesUrl { get; set; }

        [Required]
        public string ClansUrl { get; set; }

        [Required]
        public string NotificationsUrl { get; set; }

        [Required]
        public string ChallengesWebAggregatorUrl { get; set; }
    }
}

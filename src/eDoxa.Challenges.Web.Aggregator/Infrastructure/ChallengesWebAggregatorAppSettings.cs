// Filename: ChallengesWebAggregatorAppSettings.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;

using IdentityServer4.Models;

namespace eDoxa.Challenges.Web.Aggregator.Infrastructure
{
    public class ChallengesWebAggregatorAppSettings : IHasApiResourceAppSettings<EndpointsOptions>
    {
        [Required]
        public ApiResource ApiResource { get; set; }

        [Required]
        public string Authority { get; set; }

        [Required]
        public EndpointsOptions Endpoints { get; set; }
    }

    public class EndpointsOptions : AuthorityEndpointsOptions
    {
        [Required]
        public string CashierUrl { get; set; }

        [Required]
        public string ChallengesUrl { get; set; }

        [Required]
        public string GamesUrl { get; set; }
    }
}

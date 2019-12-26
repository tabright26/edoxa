// Filename: CashierWebAggregatorAppSettings.cs
// Date Created: 2019-12-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;

using IdentityServer4.Models;

namespace eDoxa.Cashier.Web.Aggregator.Infrastructure
{
    public class CashierWebAggregatorAppSettings : IHasApiResourceAppSettings<EndpointsOptions>
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
        public string PaymentUrl { get; set; }
    }
}

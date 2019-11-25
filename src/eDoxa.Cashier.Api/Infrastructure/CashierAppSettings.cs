// Filename: CashierAppSettings.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Cashier.Api.Areas.Accounts;
using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;

using IdentityServer4.Models;

namespace eDoxa.Cashier.Api.Infrastructure
{
    public class CashierAppSettings : IHasApiResourceAppSettings<EndpointsOptions>
    {
        [Required]
        public ApiResource ApiResource { get; set; }

        [Required]
        public string Authority { get; set; }

        [Required]
        public EndpointsOptions Endpoints { get; set; }

        public BundlesOptions Bundles { get; set; }
    }

    public sealed class EndpointsOptions : AuthorityEndpointsOptions
    {
        [Required]
        public string PaymentUrl { get; set; }
    }
}

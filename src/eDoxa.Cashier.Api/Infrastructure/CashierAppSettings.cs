// Filename: CashierAppSettings.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Application.AppSettings;
using eDoxa.Seedwork.Application.AppSettings.Options;

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
    }

    public sealed class EndpointsOptions : AuthorityEndpointsOptions
    {
    }
}

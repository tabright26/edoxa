// Filename: ClansAppSettings.cs
// Date Created: 2019-09-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;

using IdentityServer4.Models;

namespace eDoxa.Organizations.Clans.Api.Infrastructure
{
    public class ClansAppSettings : IHasApiResourceAppSettings<AuthorityEndpointsOptions>
    {
        [Required]
        public ApiResource ApiResource { get; set; }

        [Required]
        public string Authority { get; set; }

        [Required]
        public AuthorityEndpointsOptions Endpoints { get; set; }
    }
}

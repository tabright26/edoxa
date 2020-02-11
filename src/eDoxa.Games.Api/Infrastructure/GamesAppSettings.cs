// Filename: GamesAppSettings.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Application.AppSettings;
using eDoxa.Seedwork.Application.AppSettings.Options;

using IdentityServer4.Models;

namespace eDoxa.Games.Api.Infrastructure
{
    public sealed class GamesAppSettings : IHasApiResourceAppSettings<AuthorityEndpointsOptions>
    {
        [Required]
        public ApiResource ApiResource { get; set; }

        [Required]
        public string Authority { get; set; }

        [Required]
        public AuthorityEndpointsOptions Endpoints { get; set; }
    }
}

// Filename: ChallengesAppSettings.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Application.AppSettings;
using eDoxa.Seedwork.Application.AppSettings.Options;

using IdentityServer4.Models;

namespace eDoxa.Challenges.Api.Infrastructure
{
    public class ChallengesAppSettings : IHasApiResourceAppSettings<EndpointsOptions>
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
    }
}

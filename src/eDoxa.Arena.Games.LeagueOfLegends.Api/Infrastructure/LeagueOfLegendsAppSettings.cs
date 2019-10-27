// Filename: LeagueOfLegendsAppSettings.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;

using IdentityServer4.Models;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Infrastructure
{
    public class LeagueOfLegendsAppSettings : IHasAzureKeyVaultAppSettings, IHasApiResourceAppSettings
    {
        [Required]
        public ConnectionStrings ConnectionStrings { get; set; }

        [Required]
        public ApiResource ApiResource { get; set; }

        [Required]
        public AuthorityOptions Authority { get; set; }

        [Required]
        public AzureKeyVaultOptions AzureKeyVault { get; set; }
    }

    public class ConnectionStrings : IHasRedisConnectionString
    {
        [Required]
        public string Redis { get; set; }
    }
}

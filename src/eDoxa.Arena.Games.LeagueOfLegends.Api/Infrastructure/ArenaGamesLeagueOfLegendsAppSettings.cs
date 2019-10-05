﻿// Filename: ArenaGamesLeagueOfLegendsAppSettings.cs
// Date Created: 2019-10-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;
using eDoxa.ServiceBus.Abstractions;

using IdentityServer4.Models;

namespace eDoxa.Arena.Games.LeagueOfLegends.Api.Infrastructure
{
    public class ArenaGamesLeagueOfLegendsAppSettings : IHasAzureKeyVaultAppSettings, IHasApiResourceAppSettings, IHasServiceBusAppSettings
    {
        [Required]
        public ApiResource ApiResource { get; set; }

        [Required]
        public AuthorityOptions Authority { get; set; }

        [Required]
        public AzureKeyVaultOptions AzureKeyVault { get; set; }

        public bool AzureServiceBusEnabled { get; set; }
    }
}

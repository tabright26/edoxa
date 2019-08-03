// Filename: ArenaChallengesAppSettings.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;
using eDoxa.ServiceBus.Abstractions;

using IdentityServer4.Models;

namespace eDoxa.Arena.Challenges.Api.Infrastructure
{
    public class ArenaChallengesAppSettings : IHasAzureKeyVaultAppSettings, IHasApiResourceAppSettings, IHasServiceBusAppSettings
    {
        [Required]
        public ConnectionStrings ConnectionStrings { get; set; }

        [Required]
        public ApiResource ApiResource { get; set; }

        [Required]
        public AuthorityOptions Authority { get; set; }

        [Required]
        public AzureKeyVaultOptions AzureKeyVault { get; set; }

        public bool AzureServiceBusEnabled { get; set; }
    }

    public class ConnectionStrings : IHasSqlServerConnectionString, IHasRedisConnectionString
    {
        [Required]
        public string Redis { get; set; }

        [Required]
        public string SqlServer { get; set; }
    }
}

// Filename: ArenaChallengesAppSettings.cs
// Date Created: 2019-07-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;

using IdentityServer4.Models;

namespace eDoxa.Arena.Challenges.Api.Infrastructure
{
    public class ArenaChallengesAppSettings : IHasAzureKeyVaultAppSettings,
                                              IHasApiResourceAppSettings,
                                              IHasServiceBusAppSettings,
                                              IHasConnectionStringsAppSettings
    {
        public bool SwaggerEnabled { get; set; }

        [Required]
        public ApiResource ApiResource { get; set; }

        [Required]
        public AuthorityOptions Authority { get; set; }

        [Required]
        public AzureKeyVaultOptions AzureKeyVault { get; set; }

        [Required]
        public ConnectionStrings ConnectionStrings { get; set; }

        public bool AzureServiceBusEnabled { get; set; }

        [Required]
        public ServiceBusOptions ServiceBus { get; set; }
    }
}

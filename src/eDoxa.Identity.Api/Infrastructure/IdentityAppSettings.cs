﻿// Filename: IdentityAppSettings.cs
// Date Created: 2019-07-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;

using IdentityServer4.Models;

namespace eDoxa.Identity.Api.Infrastructure
{
    public class IdentityAppSettings : IHasAzureKeyVaultAppSettings,
                                       IHasApiResourceAppSettings,
                                       IHasServiceBusAppSettings
    {
        [Required]
        public IdentityServerOptions IdentityServer { get; set; }

        [Required]
        public ApiResource ApiResource { get; set; }

        [Required]
        public AuthorityOptions Authority { get; set; }

        public bool SwaggerEnabled { get; set; }

        [Required]
        public AzureKeyVaultOptions AzureKeyVault { get; set; }

        [Required]
        public ConnectionStrings ConnectionStrings { get; set; }

        public bool AzureServiceBusEnabled { get; set; }

        [Required]
        public ServiceBusOptions ServiceBus { get; set; }
    }

    public class IdentityServerOptions
    {
        public string IdentityUrl { get; set; }

        public string CashierUrl { get; set; }

        public string ArenaChallengesUrl { get; set; }

        public WebOptions Web { get; set; }
    }

    public class ConnectionStrings : IHasSqlServerConnectionString, IHasRedisConnectionString
    {
        [Required]
        public string SqlServer { get; set; }

        [Required]
        public string Redis { get; set; }
    }
}
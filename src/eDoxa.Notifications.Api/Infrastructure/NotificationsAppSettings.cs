﻿// Filename: NotificationsAppSettings.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;

using IdentityServer4.Models;

namespace eDoxa.Notifications.Api.Infrastructure
{
    public class NotificationsAppSettings : IHasAzureKeyVaultAppSettings, IHasApiResourceAppSettings
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

    public class ConnectionStrings : IHasSqlServerConnectionString
    {
        [Required]
        public string SqlServer { get; set; }
    }
}

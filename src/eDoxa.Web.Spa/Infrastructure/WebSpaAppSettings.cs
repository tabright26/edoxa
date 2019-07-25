// Filename: WebSpaAppSettings.cs
// Date Created: 2019-07-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;

namespace eDoxa.Web.Spa.Infrastructure
{
    public class WebSpaAppSettings : IHasAzureKeyVaultAppSettings, IHasAuthorityAppSettings
    {
        [Required]
        public AuthorityOptions Authority { get; set; }

        [Required]
        public AzureKeyVaultOptions AzureKeyVault { get; set; }
    }
}

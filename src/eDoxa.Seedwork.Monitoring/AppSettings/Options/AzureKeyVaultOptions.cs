// Filename: AzureKeyVaultOptions.cs
// Date Created: 2019-07-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace eDoxa.Seedwork.Monitoring.AppSettings.Options
{
    public class AzureKeyVaultOptions
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }
}

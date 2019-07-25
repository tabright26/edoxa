// Filename: AuthorityOptions.cs
// Date Created: 2019-07-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace eDoxa.Seedwork.Monitoring.AppSettings.Options
{
    public class AuthorityOptions
    {
        [Required]
        public string PrivateUrl { get; set; }

        [Required]
        public string PublicUrl { get; set; }
    }
}

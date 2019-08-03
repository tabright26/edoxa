// Filename: AuthorityOptions.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

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

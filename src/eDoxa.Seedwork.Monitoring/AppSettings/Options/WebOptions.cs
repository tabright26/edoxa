// Filename: WebOptions.cs
// Date Created: 2019-07-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace eDoxa.Seedwork.Monitoring.AppSettings.Options
{
    public class WebOptions
    {
        [Required]
        public string SpaUrl { get; set; }
    }
}

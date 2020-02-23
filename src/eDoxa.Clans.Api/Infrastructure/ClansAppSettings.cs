// Filename: ClansAppSettings.cs
// Date Created: 2019-09-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Application.Options;

namespace eDoxa.Clans.Api.Infrastructure
{
    public class ClansAppSettings
    {
        [Required]
        public AuthorityOptions Authority { get; set; }
    }
}

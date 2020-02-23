// Filename: GamesAppSettings.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Application.Options;

namespace eDoxa.Games.Api.Infrastructure
{
    public sealed class GamesAppSettings
    {
        [Required]
        public AuthorityOptions Authority { get; set; }
    }
}

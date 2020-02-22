// Filename: ChallengesAppSettings.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Application.Options;

namespace eDoxa.Challenges.Api.Infrastructure
{
    public class ChallengesAppSettings
    {
        [Required]
        public AuthorityOptions Authority { get; set; }
    }
}

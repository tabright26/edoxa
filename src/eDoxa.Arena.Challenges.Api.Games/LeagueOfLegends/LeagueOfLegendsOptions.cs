// Filename: LeagueOfLegendsOptions.cs
// Date Created: 2019-06-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel.DataAnnotations;

#nullable disable

namespace eDoxa.Arena.Challenges.Api.Games.LeagueOfLegends
{
    internal sealed class LeagueOfLegendsOptions
    {
        [Required]
        public string RiotToken { get; set; }
    }
}

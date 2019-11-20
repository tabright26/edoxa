// Filename: GamesOptions.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using eDoxa.Games.LeagueOfLegends;

namespace eDoxa.Games.Api.Infrastructure
{
    public sealed class GamesOptions
    {
        public LeagueOfLegendsOptions LeagueOfLegends { get; set; }
    }
}

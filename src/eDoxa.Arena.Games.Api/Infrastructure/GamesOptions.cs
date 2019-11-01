// Filename: GamesOptions.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using eDoxa.Arena.Games.LeagueOfLegends;

namespace eDoxa.Arena.Games.Api.Infrastructure
{
    public sealed class GamesOptions
    {
        public LeagueOfLegendsOptions LeagueOfLegends { get; set; }
    }
}

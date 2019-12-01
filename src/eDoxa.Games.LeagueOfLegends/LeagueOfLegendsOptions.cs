// Filename: LeagueOfLegendsOptions.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.LeagueOfLegends
{
    public sealed class LeagueOfLegendsOptions : GameOptions
    {
        public LeagueOfLegendsOptions()
        {
            Name = Game.LeagueOfLegends.Name;
            DisplayName = Game.LeagueOfLegends.DisplayName;
        }
    }
}

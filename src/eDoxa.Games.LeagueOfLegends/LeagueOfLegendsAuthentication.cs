// Filename: LeagueOfLegendsAuthentication.cs
// Date Created: 2019-11-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Games.Domain.AggregateModels;
using eDoxa.Seedwork.Domain.Miscs;

using Newtonsoft.Json;

namespace eDoxa.Games.LeagueOfLegends
{
    [JsonObject]
    public sealed class LeagueOfLegendsAuthentication : Authentication<LeagueOfLegendsAuthenticationFactor>
    {
        [JsonConstructor]
        public LeagueOfLegendsAuthentication(PlayerId playerId, LeagueOfLegendsAuthenticationFactor factor) : base(playerId, factor)
        {
        }
    }
}

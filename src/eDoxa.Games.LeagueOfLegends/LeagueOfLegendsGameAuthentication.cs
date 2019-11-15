// Filename: LeagueOfLegendsAuthentication.cs
// Date Created: 2019-11-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Games.Domain.AggregateModels;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Seedwork.Domain.Miscs;

using Newtonsoft.Json;

namespace eDoxa.Games.LeagueOfLegends
{
    [JsonObject]
    public sealed class LeagueOfLegendsGameAuthentication : GameAuthentication<LeagueOfLegendsGameAuthenticationFactor>
    {
        [JsonConstructor]
        public LeagueOfLegendsGameAuthentication(PlayerId playerId, LeagueOfLegendsGameAuthenticationFactor factor) : base(playerId, factor)
        {
        }
    }
}

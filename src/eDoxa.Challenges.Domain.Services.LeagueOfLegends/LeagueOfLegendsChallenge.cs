// Filename: LeagueOfLegendsChallenge.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Enumerations;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends
{
    public sealed class LeagueOfLegendsChallenge : Challenge
    {
        public LeagueOfLegendsChallenge(ChallengeName name, PublisherInterval interval) : base(Game.LeagueOfLegends, name, new RandomChallengeSetup(interval))
        {
        }
    }
}
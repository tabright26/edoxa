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
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Factories;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.Factories;
using eDoxa.Seedwork.Domain.Common.Enums;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends
{
    public sealed class LeagueOfLegendsChallenge : Challenge
    {
        public LeagueOfLegendsChallenge(ChallengeName name, ChallengeInterval interval) : base(
            Game.LeagueOfLegends,
            name,
            new RandomChallengeSetup(interval),
            ChallengeTimelineFactory.Instance.CreateTimeline(interval)
        )
        {
            // TODO: Refactor in a method.
            Scoring = LeagueOfLegendsChallengeScoringFactory.Instance.CreateScoring(this).Scoring;
        }
    }
}
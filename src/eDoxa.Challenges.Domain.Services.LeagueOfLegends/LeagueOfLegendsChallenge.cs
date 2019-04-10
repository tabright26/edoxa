// Filename: LeagueOfLegendsChallenge.cs
// Date Created: 2019-03-05
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.Factories;
using eDoxa.Seedwork.Domain.Common.Enums;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends
{
    public class LeagueOfLegendsChallenge : Challenge
    {
        public LeagueOfLegendsChallenge(ChallengeName name, ChallengePublisherPeriodicity periodicity) : base(Game.LeagueOfLegends, name, periodicity)
        {
            var factory = LeagueOfLegendsChallengeScoringFactory.Instance;

            var strategy = factory.Create(this);

            Scoring = strategy.Scoring;
        }
    }
}
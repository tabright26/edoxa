// Filename: LeagueOfLegendsMonthlyChallengePublisherStrategy.cs
// Date Created: 2019-03-05
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Strategies
{
    public sealed class LeagueOfLegendsMonthlyChallengePublisherStrategy : IChallengePublisherStrategy
    {
        public IEnumerable<Challenge> Challenges
        {
            get
            {
                yield return new LeagueOfLegendsChallenge("Monthly 1", ChallengeInterval.Monthly);
                yield return new LeagueOfLegendsChallenge("Monthly 2", ChallengeInterval.Monthly);
                yield return new LeagueOfLegendsChallenge("Monthly 3", ChallengeInterval.Monthly);
            }
        }
    }
}
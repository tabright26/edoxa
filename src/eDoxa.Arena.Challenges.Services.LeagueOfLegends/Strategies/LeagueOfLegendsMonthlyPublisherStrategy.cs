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

using eDoxa.Arena.Challenges.Domain;
using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Services.LeagueOfLegends.Strategies
{
    public sealed class LeagueOfLegendsMonthlyPublisherStrategy : IPublisherStrategy
    {
        public IEnumerable<Challenge> Challenges
        {
            get
            {
                yield return new LeagueOfLegendsChallenge(new ChallengeName("Monthly 1"), PublisherInterval.Monthly);
                yield return new LeagueOfLegendsChallenge(new ChallengeName("Monthly 2"), PublisherInterval.Monthly);
                yield return new LeagueOfLegendsChallenge(new ChallengeName("Monthly 3"), PublisherInterval.Monthly);
            }
        }
    }
}
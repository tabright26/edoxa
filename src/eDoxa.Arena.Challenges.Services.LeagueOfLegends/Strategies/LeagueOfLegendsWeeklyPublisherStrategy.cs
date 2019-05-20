// Filename: LeagueOfLegendsChallengePublisherStrategy.cs
// Date Created: 2019-03-04
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
    public class LeagueOfLegendsWeeklyPublisherStrategy : IPublisherStrategy
    {
        public IEnumerable<Challenge> Challenges
        {
            get
            {
                yield return new LeagueOfLegendsChallenge(new ChallengeName("Weekly 1"), PublisherInterval.Weekly);
                yield return new LeagueOfLegendsChallenge(new ChallengeName("Weekly 2"), PublisherInterval.Weekly);
                yield return new LeagueOfLegendsChallenge(new ChallengeName("Weekly 3"), PublisherInterval.Weekly);
            }
        }
    }
}
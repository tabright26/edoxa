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

using eDoxa.Challenges.Domain.Entities;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Strategies
{
    public sealed class LeagueOfLegendsMonthlyPublisherStrategy : IPublisherStrategy
    {
        public IEnumerable<Challenge> Challenges
        {
            get
            {
                yield return new LeagueOfLegendsChallenge("Monthly 1", PublisherInterval.Monthly);
                yield return new LeagueOfLegendsChallenge("Monthly 2", PublisherInterval.Monthly);
                yield return new LeagueOfLegendsChallenge("Monthly 3", PublisherInterval.Monthly);
            }
        }
    }
}
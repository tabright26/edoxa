﻿// Filename: LeagueOfLegendsChallengePublisherStrategy.cs
// Date Created: 2019-03-04
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
    public class LeagueOfLegendsWeeklyPublisherStrategy : IPublisherStrategy
    {
        public IEnumerable<Challenge> Challenges
        {
            get
            {
                yield return new LeagueOfLegendsChallenge("Weekly 1", ChallengeInterval.Weekly);
                yield return new LeagueOfLegendsChallenge("Weekly 2", ChallengeInterval.Weekly);
                yield return new LeagueOfLegendsChallenge("Weekly 3", ChallengeInterval.Weekly);
            }
        }
    }
}
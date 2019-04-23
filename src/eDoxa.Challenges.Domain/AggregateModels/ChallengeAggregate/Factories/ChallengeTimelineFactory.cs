// Filename: ChallengeTimelineFactory.cs
// Date Created: 2019-04-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Strategies;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Factories
{
    public sealed class ChallengeTimelineFactory
    {
        private static readonly Lazy<ChallengeTimelineFactory> Lazy = new Lazy<ChallengeTimelineFactory>(() => new ChallengeTimelineFactory());

        public static ChallengeTimelineFactory Instance => Lazy.Value;

        public IChallengeTimelineStrategy CreateTimelineStrategy(ChallengeInterval interval)
        {
            if (interval == ChallengeInterval.Daily)
            {
                return new DefaultChallengeTimelineDailyStrategy();
            }

            if (interval == ChallengeInterval.Weekly)
            {
                return new DefaultChallengeTimelineWeeklyStrategy();
            }

            if (interval == ChallengeInterval.Monthly)
            {
                return new DefaultChallengeTimelineMonthlyStrategy();
            }

            throw new ArgumentException(nameof(interval));
        }
    }
}
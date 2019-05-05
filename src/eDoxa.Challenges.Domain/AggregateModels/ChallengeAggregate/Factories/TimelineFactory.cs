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
    public sealed class TimelineFactory
    {
        private static readonly Lazy<TimelineFactory> Lazy = new Lazy<TimelineFactory>(() => new TimelineFactory());

        public static TimelineFactory Instance => Lazy.Value;

        public IChallengeTimelineStrategy CreateTimelineStrategy(PublisherInterval interval)
        {
            if (interval == PublisherInterval.Daily)
            {
                return new DefaultChallengeTimelineDailyStrategy();
            }

            if (interval == PublisherInterval.Weekly)
            {
                return new DefaultChallengeTimelineWeeklyStrategy();
            }

            if (interval == PublisherInterval.Monthly)
            {
                return new DefaultChallengeTimelineMonthlyStrategy();
            }

            throw new ArgumentException(nameof(interval));
        }
    }
}
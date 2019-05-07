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

using eDoxa.Challenges.Domain.Entities.Abstractions;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Entities.Default.Strategies;

namespace eDoxa.Challenges.Domain.Entities.Default.Factories
{
    public sealed class DefaultTimelineFactory
    {
        private static readonly Lazy<DefaultTimelineFactory> Lazy = new Lazy<DefaultTimelineFactory>(() => new DefaultTimelineFactory());

        public static DefaultTimelineFactory Instance => Lazy.Value;

        public IChallengeTimelineStrategy CreateTimelineStrategy(PublisherInterval interval)
        {
            if (interval == PublisherInterval.Daily)
            {
                return new DefaultTimelineDailyStrategy();
            }

            if (interval == PublisherInterval.Weekly)
            {
                return new DefaultTimelineWeeklyStrategy();
            }

            if (interval == PublisherInterval.Monthly)
            {
                return new DefaultTimelineMonthlyStrategy();
            }

            throw new ArgumentException(nameof(interval));
        }
    }
}
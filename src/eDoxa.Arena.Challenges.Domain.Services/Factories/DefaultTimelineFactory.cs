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

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.Services.Strategies;

namespace eDoxa.Arena.Challenges.Domain.Services.Factories
{
    public sealed class DefaultTimelineFactory
    {
        private static readonly Lazy<DefaultTimelineFactory> Lazy = new Lazy<DefaultTimelineFactory>(() => new DefaultTimelineFactory());

        public static DefaultTimelineFactory Instance => Lazy.Value;

        public ITimelineStrategy CreateTimelineStrategy(PublisherInterval interval)
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
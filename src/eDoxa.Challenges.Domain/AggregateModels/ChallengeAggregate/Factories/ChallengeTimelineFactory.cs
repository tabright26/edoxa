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

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Factories
{
    public sealed class ChallengeTimelineFactory
    {
        private static readonly Lazy<ChallengeTimelineFactory> Lazy = new Lazy<ChallengeTimelineFactory>(() => new ChallengeTimelineFactory());

        public static ChallengeTimelineFactory Instance => Lazy.Value;

        public ChallengeTimeline CreateTimeline(ChallengeInterval interval)
        {
            if (interval == ChallengeInterval.Daily)
            {
                return new ChallengeTimelineDaily();
            }

            if (interval == ChallengeInterval.Weekly)
            {
                return new ChallengeTimelineWeekly();
            }

            if (interval == ChallengeInterval.Monthly)
            {
                return new ChallengeTimelineMonthly();
            }

            throw new ArgumentException(nameof(interval));
        }
    }
}
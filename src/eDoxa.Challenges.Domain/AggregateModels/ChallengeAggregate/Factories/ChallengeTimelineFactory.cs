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

        public ChallengeTimeline CreateTimeline(ChallengePublisherPeriodicity periodicity)
        {
            switch (periodicity)
            {
                case ChallengePublisherPeriodicity.Daily:

                    return new ChallengeTimelineDaily();

                case ChallengePublisherPeriodicity.Weekly:

                    return new ChallengeTimelineDaily();

                case ChallengePublisherPeriodicity.Monthly:

                    return new ChallengeTimelineDaily();

                default:

                    throw new ArgumentException(nameof(periodicity));
            }
        }
    }
}
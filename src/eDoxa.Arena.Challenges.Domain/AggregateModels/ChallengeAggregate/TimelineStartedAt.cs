// Filename: TimelineStartedAt.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class TimelineStartedAt : TypeObject<TimelineStartedAt, DateTime>
    {
        public TimelineStartedAt(TimelinePublishedAt publishedAt, TimelineRegistrationPeriod registrationPeriod) : base(publishedAt + registrationPeriod)
        {
        }

        public static TimelineEndedAt operator +(TimelineStartedAt startedAt, TimelineExtensionPeriod extensionPeriod)
        {
            return new TimelineEndedAt(startedAt, extensionPeriod);
        }
    }
}

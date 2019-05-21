// Filename: TimelinePublishedAt.cs
// Date Created: 2019-04-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class TimelinePublishedAt : TypeObject<TimelinePublishedAt, DateTime>
    {
        public static readonly DateTime Min = DateTime.UtcNow;
        public static readonly DateTime Max = Min.AddMonths(1);

        public static readonly TimelinePublishedAt MinValue = new TimelinePublishedAt(Min);
        public static readonly TimelinePublishedAt MaxValue = new TimelinePublishedAt(Max);

        public TimelinePublishedAt(DateTime publishedAt, bool validate = true) : base(publishedAt)
        {
            publishedAt = publishedAt.ToUniversalTime();

            if (validate)
            {
                if (publishedAt < Min || publishedAt > Max)
                {
                    throw new ArgumentException(nameof(publishedAt));
                }
            }
        }

        public static TimelineStartedAt operator +(TimelinePublishedAt publishedAt, TimelineRegistrationPeriod registrationPeriod)
        {
            return new TimelineStartedAt(publishedAt, registrationPeriod);
        }
    }
}
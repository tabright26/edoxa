// Filename: TimelineRegistrationPeriod.cs
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
    public sealed class TimelineRegistrationPeriod : TypeObject<TimelineRegistrationPeriod, TimeSpan>
    {
        public static readonly TimeSpan Min = TimeSpan.FromHours(4);
        public static readonly TimeSpan Max = TimeSpan.FromDays(7);
        public static readonly TimeSpan Default = TimeSpan.FromHours(4);

        public static readonly TimelineRegistrationPeriod MinValue = new TimelineRegistrationPeriod(Min);
        public static readonly TimelineRegistrationPeriod MaxValue = new TimelineRegistrationPeriod(Max);
        public static readonly TimelineRegistrationPeriod DefaultValue = new TimelineRegistrationPeriod(Default);

        public TimelineRegistrationPeriod(TimeSpan registrationPeriod, bool validate = true) : base(registrationPeriod)
        {
            if (validate)
            {
                if (registrationPeriod < Min || registrationPeriod > Max)
                {
                    throw new ArgumentException(nameof(registrationPeriod));
                }
            }
        }
    }
}

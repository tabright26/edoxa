// Filename: TimelineExtensionPeriod.cs
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
    public sealed class TimelineExtensionPeriod : TypeObject<TimelineExtensionPeriod, TimeSpan>
    {
        public static readonly TimeSpan Min = TimeSpan.FromHours(8);
        public static readonly TimeSpan Max = TimeSpan.FromDays(28);
        public static readonly TimeSpan Default = TimeSpan.FromDays(8);

        public static readonly TimelineExtensionPeriod MinValue = new TimelineExtensionPeriod(Min);
        public static readonly TimelineExtensionPeriod MaxValue = new TimelineExtensionPeriod(Max);
        public static readonly TimelineExtensionPeriod DefaultValue = new TimelineExtensionPeriod(Default);

        public TimelineExtensionPeriod(TimeSpan extensionPeriod, bool validate = true) : base(extensionPeriod)
        {
            if (validate)
            {
                if (extensionPeriod < Min || extensionPeriod > Max)
                {
                    throw new ArgumentException(nameof(extensionPeriod));
                }
            }
        }
    }
}

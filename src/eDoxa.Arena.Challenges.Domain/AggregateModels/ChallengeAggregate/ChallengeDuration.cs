// Filename: ChallengeDuration.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Common.Attributes;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeDuration : ValueObject
    {
        [AllowValue(true)] public static readonly ChallengeDuration OneDay = new ChallengeDuration(TimeSpan.FromDays(1));
        [AllowValue(true)] public static readonly ChallengeDuration TwoDays = new ChallengeDuration(TimeSpan.FromDays(2));
        [AllowValue(true)] public static readonly ChallengeDuration ThreeDays = new ChallengeDuration(TimeSpan.FromDays(3));
        [AllowValue(true)] public static readonly ChallengeDuration FourDays = new ChallengeDuration(TimeSpan.FromDays(4));
        [AllowValue(true)] public static readonly ChallengeDuration FiveDays = new ChallengeDuration(TimeSpan.FromDays(5));
        [AllowValue(false)] public static readonly ChallengeDuration SixDays = new ChallengeDuration(TimeSpan.FromDays(6));
        [AllowValue(false)] public static readonly ChallengeDuration SevenDays = new ChallengeDuration(TimeSpan.FromDays(7));

        public ChallengeDuration(TimeSpan timeSpan) : this()
        {
            Ticks = timeSpan.Ticks;
        }

        private ChallengeDuration()
        {
            // Required by EF Core.
        }

        public long Ticks { get; private set; }

        public static implicit operator TimeSpan(ChallengeDuration duration)
        {
            return TimeSpan.FromTicks(duration.Ticks);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Ticks;
        }

        public override string ToString()
        {
            return TimeSpan.FromTicks(Ticks).ToString();
        }
    }
}

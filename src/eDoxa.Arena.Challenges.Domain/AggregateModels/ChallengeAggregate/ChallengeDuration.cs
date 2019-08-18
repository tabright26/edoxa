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

using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeDuration : ValueObject
    {
        public static readonly ChallengeDuration OneDay = new ChallengeDuration(TimeSpan.FromDays(1));
        public static readonly ChallengeDuration TwoDays = new ChallengeDuration(TimeSpan.FromDays(2));
        public static readonly ChallengeDuration ThreeDays = new ChallengeDuration(TimeSpan.FromDays(3));
        public static readonly ChallengeDuration FourDays = new ChallengeDuration(TimeSpan.FromDays(4));
        public static readonly ChallengeDuration FiveDays = new ChallengeDuration(TimeSpan.FromDays(5));
        public static readonly ChallengeDuration SixDays = new ChallengeDuration(TimeSpan.FromDays(6));
        public static readonly ChallengeDuration SevenDays = new ChallengeDuration(TimeSpan.FromDays(7));

        public ChallengeDuration(TimeSpan timeSpan)
        {
            Ticks = timeSpan.Ticks;
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

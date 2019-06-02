// Filename: ChallengeDuration.cs
// Date Created: 2019-05-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeDuration : ValueObject
    {
        public static readonly ChallengeDuration OneDay = new ChallengeDuration(1);
        public static readonly ChallengeDuration TwoDays = new ChallengeDuration(2);
        public static readonly ChallengeDuration ThreeDays = new ChallengeDuration(3);
        public static readonly ChallengeDuration FourDays = new ChallengeDuration(4);
        public static readonly ChallengeDuration FiveDays = new ChallengeDuration(5);
        public static readonly ChallengeDuration SixDays = new ChallengeDuration(6);
        public static readonly ChallengeDuration SevenDays = new ChallengeDuration(7);

        public ChallengeDuration(int days) : this()
        {
            Value = TimeSpan.FromDays(days);
        }

        public ChallengeDuration(long ticks) : this()
        {
            Value = TimeSpan.FromTicks(ticks);
        }

        private ChallengeDuration()
        {
            // Required by EF Core.
        }

        public static implicit operator TimeSpan(ChallengeDuration duration)
        {
            return duration.Value;
        }

        public TimeSpan Value { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}

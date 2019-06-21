// Filename: ChallengeState.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Attributes;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class ChallengeState : Enumeration<ChallengeState>
    {
        [AllowValue(true)] public static readonly ChallengeState Inscription = new ChallengeState(1 << 1, nameof(Inscription));
        [AllowValue(true)] public static readonly ChallengeState InProgress = new ChallengeState(1 << 2, nameof(InProgress));
        [AllowValue(true)] public static readonly ChallengeState Ended = new ChallengeState(1 << 3, nameof(Ended));
        [AllowValue(true)] public static readonly ChallengeState Closed = new ChallengeState(1 << 4, nameof(Closed));

        public ChallengeState()
        {
        }

        private ChallengeState(int value, string name) : base(value, name)
        {
        }

        public static ChallengeState From(ChallengeTimeline timeline)
        {
            return Resolve(timeline.Duration, timeline.StartedAt, timeline.ClosedAt);
        }

        public static ChallengeState Resolve(TimeSpan duration, DateTime? startedAt, DateTime? closedAt)
        {
            var endedAt = startedAt + duration;

            return closedAt != null ? Closed : endedAt != null && endedAt <= DateTime.UtcNow ? Ended : startedAt != null ? InProgress : Inscription;
        }
    }
}

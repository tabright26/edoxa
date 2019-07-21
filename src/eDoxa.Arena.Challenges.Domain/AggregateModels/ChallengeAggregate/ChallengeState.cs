// Filename: ChallengeState.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class ChallengeState : Enumeration<ChallengeState>
    {
        public static readonly ChallengeState Inscription = new ChallengeState(1 << 0, nameof(Inscription));
        public static readonly ChallengeState InProgress = new ChallengeState(1 << 1, nameof(InProgress));
        public static readonly ChallengeState Ended = new ChallengeState(1 << 2, nameof(Ended));
        public static readonly ChallengeState Closed = new ChallengeState(1 << 3, nameof(Closed));

        public ChallengeState()
        {
        }

        private ChallengeState(int value, string name) : base(value, name)
        {
        }

        public static ChallengeState From(ChallengeTimeline timeline)
        {
            return timeline.ClosedAt != null ? Closed :
                timeline.EndedAt != null && timeline.EndedAt <= DateTime.UtcNow ? Ended :
                timeline.StartedAt != null ? InProgress : Inscription;
        }
    }
}

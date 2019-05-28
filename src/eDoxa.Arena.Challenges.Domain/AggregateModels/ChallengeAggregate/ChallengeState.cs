// Filename: ChallengeState.cs
// Date Created: 2019-05-20
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
using eDoxa.Seedwork.Domain.TypeConverters;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    [TypeConverter(typeof(EnumerationTypeConverter<ChallengeState>))]
    public sealed class ChallengeState : Enumeration
    {
        public static readonly ChallengeState Inscription = new ChallengeState(1 << 1, nameof(Inscription));
        public static readonly ChallengeState InProgress = new ChallengeState(1 << 2, nameof(InProgress));
        public static readonly ChallengeState Ended = new ChallengeState(1 << 3, nameof(Ended));
        public static readonly ChallengeState Closed = new ChallengeState(1 << 4, nameof(Closed));

        private ChallengeState(int value, string name) : base(value, name)
        {
        }

        public static ChallengeState GetState(ChallengeTimeline timeline)
        {
            return timeline.ClosedAt != null ? Closed :
                timeline.EndedAt != null && timeline.EndedAt <= DateTime.UtcNow ? Ended :
                timeline.StartedAt != null ? InProgress : Inscription;
        }
    }
}

// Filename: ChallengeTimeline.cs
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

using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeTimeline : ValueObject
    {
        public ChallengeTimeline(ChallengeDuration duration, DateTime? startedAt, DateTime? closedAt) : this(duration, startedAt)
        {
            ClosedAt = closedAt;
        }

        public ChallengeTimeline(ChallengeDuration duration, DateTime? startedAt) : this(duration)
        {
            StartedAt = startedAt;
        }

        public ChallengeTimeline(ChallengeDuration duration)
        {
            Duration = duration;
            StartedAt = null;
            ClosedAt = null;
        }

        public ChallengeDuration Duration { get; }

        public DateTime? StartedAt { get; }

        public DateTime? ClosedAt { get; }

        public DateTime? EndedAt => StartedAt + Duration;

        public ChallengeState State => ChallengeState.From(this);

        public static implicit operator ChallengeState(ChallengeTimeline timeline)
        {
            return timeline.State;
        }

        public ChallengeTimeline Start(IDateTimeProvider provider = null)
        {
            provider = provider ?? new UtcNowDateTimeProvider();

            return new ChallengeTimeline(Duration, provider.DateTime);
        }

        public ChallengeTimeline Close(IDateTimeProvider provider = null)
        {
            provider = provider ?? new UtcNowDateTimeProvider();

            return new ChallengeTimeline(Duration, StartedAt, provider.DateTime);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Duration;
            yield return StartedAt;
            yield return EndedAt;
            yield return ClosedAt;
            yield return State;
        }

        public override string ToString()
        {
            return State.ToString();
        }
    }
}

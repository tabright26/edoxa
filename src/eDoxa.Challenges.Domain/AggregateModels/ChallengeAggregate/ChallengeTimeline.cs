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

using eDoxa.Seedwork.Domain;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeTimeline : ValueObject
    {
        public ChallengeTimeline(IDateTimeProvider createdAt, ChallengeDuration duration)
        {
            CreatedAt = createdAt.DateTime;
            Duration = duration;
            StartedAt = null;
            ClosedAt = null;
        }

        private ChallengeTimeline(
            DateTime createdAt,
            ChallengeDuration duration,
            DateTime? startedAt,
            DateTime? closedAt
        ) : this(createdAt, duration, startedAt)
        {
            ClosedAt = closedAt;
        }

        private ChallengeTimeline(DateTime createdAt, ChallengeDuration duration, DateTime? startedAt)
        {
            CreatedAt = createdAt;
            Duration = duration;
            StartedAt = startedAt;
        }

        public DateTime CreatedAt { get; }

        public ChallengeDuration Duration { get; }

        public DateTime? StartedAt { get; }

        public DateTime? ClosedAt { get; }

        public DateTime? EndedAt => StartedAt + Duration;

        public ChallengeState State => ChallengeState.From(this);

        public static implicit operator ChallengeState(ChallengeTimeline timeline)
        {
            return timeline.State;
        }

        public ChallengeTimeline Start(IDateTimeProvider startedAt)
        {
            return new ChallengeTimeline(CreatedAt, Duration, startedAt.DateTime);
        }

        public ChallengeTimeline Close(IDateTimeProvider closedAt)
        {
            return new ChallengeTimeline(CreatedAt, Duration, StartedAt, closedAt.DateTime);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return State;

            yield return CreatedAt;

            yield return Duration;

            if (StartedAt != null)
            {
                yield return StartedAt;
            }

            if (EndedAt != null)
            {
                yield return EndedAt;
            }

            if (ClosedAt != null)
            {
                yield return ClosedAt;
            }
        }

        public override string ToString()
        {
            return State.ToString();
        }
    }
}

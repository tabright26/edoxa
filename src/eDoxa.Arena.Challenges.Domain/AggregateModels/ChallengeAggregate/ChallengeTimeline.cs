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

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class ChallengeTimeline : ValueObject
    {
        public ChallengeTimeline(ChallengeDuration duration) : this()
        {
            Duration = duration;
        }

        protected ChallengeTimeline(ChallengeDuration duration, DateTime? startedAt) : this(duration)
        {
            StartedAt = startedAt;
        }

        protected ChallengeTimeline(ChallengeDuration duration, DateTime? startedAt, DateTime? closedAt) : this(duration, startedAt)
        {
            ClosedAt = closedAt;
        }

        private ChallengeTimeline()
        {
            StartedAt = null;
            ClosedAt = null;
        }

        public ChallengeDuration Duration { get; private set; }

        public DateTime? StartedAt { get; private set; }

        public DateTime? ClosedAt { get; private set; }

        public DateTime? EndedAt => StartedAt + Duration;

        public ChallengeState State =>
            ClosedAt != null ? ChallengeState.Closed :
            EndedAt != null && EndedAt <= DateTime.UtcNow ? ChallengeState.Ended :
            StartedAt != null ? ChallengeState.InProgress : ChallengeState.Inscription;

        public ChallengeTimeline Start()
        {
            return new ChallengeTimeline(Duration, DateTime.UtcNow);
        }

        public ChallengeTimeline Close()
        {
            return new ChallengeTimeline(Duration, StartedAt, DateTime.UtcNow);
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

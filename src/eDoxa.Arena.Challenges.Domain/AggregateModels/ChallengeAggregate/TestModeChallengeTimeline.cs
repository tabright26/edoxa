// Filename: TestModeChallengeTimeline.cs
// Date Created: 2019-06-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class TestModeChallengeTimeline : ChallengeTimeline
    {
        public TestModeChallengeTimeline(ChallengeDuration duration) : base(duration)
        {
        }

        public TestModeChallengeTimeline(ChallengeDuration duration, DateTime startedAt) : base(duration, startedAt)
        {
        }

        public TestModeChallengeTimeline(ChallengeDuration duration, DateTime startedAt, DateTime closedAt) : base(duration, startedAt, closedAt)
        {
        }
    }
}

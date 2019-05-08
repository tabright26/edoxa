// Filename: ChallengeState.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeState : Enumeration<ChallengeState>
    {
        public static readonly ChallengeState Draft = new ChallengeState(1 << 0, nameof(Draft));
        public static readonly ChallengeState Configured = new ChallengeState(1 << 1, nameof(Configured));
        public static readonly ChallengeState Opened = new ChallengeState(1 << 2, nameof(Opened));
        public static readonly ChallengeState InProgress = new ChallengeState(1 << 3, nameof(InProgress));
        public static readonly ChallengeState Ended = new ChallengeState(1 << 4, nameof(Ended));
        public static readonly ChallengeState Closed = new ChallengeState(1 << 5, nameof(Closed));

        private ChallengeState(int value, string displayName) : base(value, displayName)
        {
        }

        public ChallengeState()
        {
        }
    }
}
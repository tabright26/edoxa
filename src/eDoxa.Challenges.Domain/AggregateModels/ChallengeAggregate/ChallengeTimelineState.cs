// Filename: ChallengeTimelineState.cs
// Date Created: 2019-04-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeTimelineState
    {
        private readonly long _value;
        private readonly string _displayName;        

        public ChallengeTimelineState(long value, string displayName)
        {
            _value = value;
            _displayName = displayName;
        }

        public static ChallengeTimelineState None { get; } = new ChallengeTimelineState(0, nameof(None));

        public static ChallengeTimelineState Draft { get; } = new ChallengeTimelineState(1 << 0, nameof(Draft));

        public static ChallengeTimelineState Configured { get; } = new ChallengeTimelineState(1 << 1, nameof(Configured));

        public static ChallengeTimelineState Opened { get; } = new ChallengeTimelineState(1 << 2, nameof(Opened));

        public static ChallengeTimelineState InProgress { get; } = new ChallengeTimelineState(1 << 3, nameof(InProgress));

        public static ChallengeTimelineState Ended { get; } = new ChallengeTimelineState(1 << 4, nameof(Ended));

        public static ChallengeTimelineState Closed { get; } = new ChallengeTimelineState(1 << 5, nameof(Closed));

        public static ChallengeTimelineState All { get; } = new ChallengeTimelineState(~None, nameof(All));

        public static implicit operator long(ChallengeTimelineState state)
        {
            return state._value;
        }

        public override string ToString()
        {
            return _displayName;
        }
    }
}
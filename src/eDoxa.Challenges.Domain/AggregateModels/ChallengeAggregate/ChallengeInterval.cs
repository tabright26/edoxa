// Filename: ChallengeInterval.cs
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
    public sealed class ChallengeInterval
    {
        private readonly string _displayName;
        private readonly long _value;

        private ChallengeInterval(long value, string displayName)
        {
            _value = value;
            _displayName = displayName;
        }

        public static ChallengeInterval Daily { get; } = new ChallengeInterval(1 << 0, nameof(Daily));

        public static ChallengeInterval Weekly { get; } = new ChallengeInterval(1 << 1, nameof(Weekly));

        public static ChallengeInterval Monthly { get; } = new ChallengeInterval(1 << 2, nameof(Monthly));

        public static implicit operator long(ChallengeInterval state)
        {
            return state._value;
        }

        public override string ToString()
        {
            return _displayName;
        }
    }
}
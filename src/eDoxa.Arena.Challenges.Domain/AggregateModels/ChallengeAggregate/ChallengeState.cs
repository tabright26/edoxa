// Filename: ChallengeState.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Seedwork.Common.Attributes;
using eDoxa.Seedwork.Domain.Aggregate;

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
    }
}

// Filename: ChallengeState.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    [Flags]
    public enum ChallengeState1
    {
        None = 0,
        Draft = 1 << 0,
        Configured = 1 << 1,
        Opened = 1 << 2,
        InProgress = 1 << 3,
        Ended = 1 << 4,
        Closed = 1 << 5,
        All = ~None
    }
}
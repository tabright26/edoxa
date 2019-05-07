// Filename: ChallengeType.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate
{
    [Flags]
    public enum ChallengeType
    {
        None = 0,
        Default = 1 << 0,
        All = ~None
    }
}
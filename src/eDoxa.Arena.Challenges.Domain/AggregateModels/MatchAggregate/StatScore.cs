// Filename: StatScore.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Domain.Abstractions;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate
{
    public sealed class StatScore : Score
    {
        public StatScore(Stat stat) : base(Convert.ToDecimal(stat.Value) * Convert.ToDecimal(stat.Weighting))
        {
        }
    }
}

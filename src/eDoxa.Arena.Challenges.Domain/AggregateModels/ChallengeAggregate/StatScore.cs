// Filename: StatScore.cs
// Date Created: 2019-06-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.Abstractions;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class StatScore : Score
    {
        internal StatScore(Stat stat) : base(Resolve(stat.Value, stat.Weighting))
        {
        }

        public static decimal Resolve(double value, float weighting)
        {
            return Convert.ToDecimal(value * weighting);
        }
    }
}

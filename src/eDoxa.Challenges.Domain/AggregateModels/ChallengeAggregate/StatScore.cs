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

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class StatScore : Score
    {
        public StatScore(Stat stat) : base(Convert.ToDecimal(stat.Value * stat.Weighting))
        {
        }
    }
}

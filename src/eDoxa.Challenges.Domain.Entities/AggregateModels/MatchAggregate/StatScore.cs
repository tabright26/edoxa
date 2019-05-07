// Filename: StatScore.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Challenges.Domain.Entities.AggregateModels.MatchAggregate
{
    public class StatScore : Score
    {
        internal StatScore(Stat stat) : base(Convert.ToDecimal(stat.Value) * Convert.ToDecimal(stat.Weighting))
        {
        }
    }
}
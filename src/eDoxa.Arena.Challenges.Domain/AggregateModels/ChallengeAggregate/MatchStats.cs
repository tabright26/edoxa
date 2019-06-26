// Filename: MatchStats.cs
// Date Created: 2019-06-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class MatchStats : Dictionary<StatName, StatValue>, IMatchStats
    {
        public MatchStats(object stats) : base(
            stats.GetType()
                .GetProperties()
                .ToDictionary(propertyInfo => new StatName(propertyInfo), propertyInfo => new StatValue(propertyInfo.GetValue(stats)))
        )
        {
        }

        public MatchStats(IEnumerable<Stat> stats) : base(stats.ToDictionary(stat => stat.Name, stat => stat.Value))
        {
        }
    }
}

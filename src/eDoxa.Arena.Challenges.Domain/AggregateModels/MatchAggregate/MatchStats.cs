﻿// Filename: MatchStats.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.Abstractions;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate
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
    }
}

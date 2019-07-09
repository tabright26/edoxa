// Filename: GameStats.cs
// Date Created: 2019-06-25
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
    public sealed class GameStats : Dictionary<StatName, StatValue>, IGameStats
    {
        public GameStats(object stats) : base(
            stats.GetType()
                .GetProperties()
                .ToDictionary(propertyInfo => new StatName(propertyInfo), propertyInfo => new StatValue(propertyInfo.GetValue(stats)))
        )
        {
        }
    }
}

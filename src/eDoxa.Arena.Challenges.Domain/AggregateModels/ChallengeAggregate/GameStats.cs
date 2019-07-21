// Filename: GameStats.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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

        public GameStats(IEnumerable<Stat> stats) : base(stats.ToDictionary(stat => stat.Name, stat => stat.Value))
        {
        }
    }
}

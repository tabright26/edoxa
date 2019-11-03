// Filename: Scoring.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class Scoring : Dictionary<StatName, StatWeighting>, IScoring
    {
        public Scoring(IEnumerable<Stat> stats) : base(stats.ToDictionary(stat => stat.Name, stat => stat.Weighting))
        {
        }

        public Scoring()
        {
        }

        public IEnumerable<Stat> Map(IGameStats stats)
        {
            for (var index = 0; index < Count; index++)
            {
                var item = this.ElementAt(index);

                var name = item.Key;

                if (!stats.ContainsKey(name))
                {
                    continue;
                }

                var value = stats[name];

                var weighting = item.Value;

                var stat = new Stat(name, value, weighting);

                yield return stat;
            }
        }
    }
}

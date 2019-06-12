// Filename: Scoring.cs
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
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class Scoring : Dictionary<StatName, StatWeighting>, IScoring
    {
        public Scoring(IEnumerable<ScoringItem> scoringItems) : base(
            scoringItems.ToDictionary(scoringItem => scoringItem.Name, scoringItem => scoringItem.Weighting)
        )
        {
        }

        public Scoring(IEnumerable<Stat> stats) : base(stats.ToDictionary(stat => stat.Name, stat => stat.Weighting))
        {
        }
    }
}

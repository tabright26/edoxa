// Filename: MatchScoreAdapter.cs
// Date Created: 2019-04-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Adapters
{
    internal sealed class MatchScoreAdapter : IScoreAdapter
    {
        private readonly IReadOnlyCollection<Stat> _stats;

        public MatchScoreAdapter(Match match)
        {
            _stats = match.Stats;
        }

        public Score Score => new Score(_stats.Sum(stat => stat.Score));
    }
}
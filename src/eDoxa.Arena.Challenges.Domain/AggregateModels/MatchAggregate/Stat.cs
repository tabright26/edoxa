// Filename: Stat.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Domain.Abstractions;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate
{
    public class Stat : Entity<StatId>
    {
        public Stat(MatchId matchId, StatName name, StatValue value, StatWeighting weighting)
        {
            MatchId = matchId;
            Name = name;
            Value = value;
            Weighting = weighting;
        }

        public MatchId MatchId { get; private set; }

        public StatName Name { get; private set; }

        public StatValue Value { get; private set; }

        public StatWeighting Weighting { get; private set; }

        public Score Score => new StatScore(this);
    }
}
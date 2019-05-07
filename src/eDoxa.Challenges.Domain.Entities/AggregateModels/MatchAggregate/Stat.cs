// Filename: Stat.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Challenges.Domain.Entities.AggregateModels.MatchAggregate
{
    public class Stat : Entity<StatId>
    {
        private MatchId _matchId;
        private StatName _name;
        private StatValue _value;
        private StatWeighting _weighting;

        public Stat(MatchId matchId, StatName name, StatValue value, StatWeighting weighting)
        {
            _matchId = matchId;
            _name = name;
            _value = value;
            _weighting = weighting;
        }

        public MatchId MatchId => _matchId;

        public StatName Name => _name;

        public StatValue Value => _value;

        public StatWeighting Weighting => _weighting;

        public Score Score => new StatScore(this);
    }
}
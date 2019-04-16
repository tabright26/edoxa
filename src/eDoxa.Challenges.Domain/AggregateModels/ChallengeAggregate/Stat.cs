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

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class Stat : Entity<StatId>
    {
        private MatchId _matchId;
        private string _name;
        private double _value;
        private float _weighting;

        internal Stat(MatchId matchId, string name, double value, float weighting)
        {
            _matchId = matchId;
            _name = name;
            _value = value;
            _weighting = weighting;
        }

        public MatchId MatchId => _matchId;

        public string Name => _name;

        public double Value => _value;

        public float Weighting => _weighting;

        public Score Score => Score.FromStat(this);
    }
}
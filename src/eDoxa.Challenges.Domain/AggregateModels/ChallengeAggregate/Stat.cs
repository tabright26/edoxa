// Filename: Stat.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

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
            _matchId = matchId ?? throw new ArgumentNullException(nameof(MatchId));
            _name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException(nameof(Name));
            _value = value;
            _weighting = weighting;
        }

        public MatchId MatchId
        {
            get
            {
                return _matchId;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public double Value
        {
            get
            {
                return _value;
            }
        }

        public float Weighting
        {
            get
            {
                return _weighting;
            }
        }

        public Score Score
        {
            get
            {
                return Score.FromStat(this);
            }
        }
    }
}
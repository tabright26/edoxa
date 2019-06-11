// Filename: Match.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate
{
    public class Match : Entity<MatchId>, IAggregateRoot
    {
        private HashSet<Stat> _stats;

        public Match(MatchReference matchReference, IMatchStats stats, IScoring scoring) : this()
        {
            MatchReference = matchReference;

            this.SnapshotStats(stats, scoring);
        }

        public Match()
        {
            Timestamp = DateTime.UtcNow;
            _stats = new HashSet<Stat>();
        }

        public DateTime Timestamp { get; private set; }

        public MatchReference MatchReference { get; private set; }

        public Score TotalScore => new MatchScore(this);

        public IReadOnlyCollection<Stat> Stats => _stats;

        private void SnapshotStats(IMatchStats stats, IScoring scoring)
        {
            for (var index = 0; index < scoring.Count; index++)
            {
                var item = scoring.ElementAt(index);

                var name = item.Key;

                if (!stats.ContainsKey(name))
                {
                    continue;
                }

                var value = stats[name];

                var weighting = item.Value;

                var stat = new Stat(name, value, weighting);

                _stats.Add(stat);
            }
        }
    }
}

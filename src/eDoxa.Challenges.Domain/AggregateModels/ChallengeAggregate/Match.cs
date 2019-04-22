// Filename: Match.cs
// Date Created: 2019-04-14
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

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class Match : Entity<MatchId>, IAggregateRoot
    {
        private LinkedMatch _linkedMatch;
        private Participant _participant;
        private HashSet<Stat> _stats;
        private DateTime _timestamp;

        internal Match(Participant participant, LinkedMatch linkedMatch) : this()
        {
            _linkedMatch = linkedMatch;
            _participant = participant;
        }

        private Match()
        {
            _timestamp = DateTime.UtcNow;
            _stats = new HashSet<Stat>();
        }

        public DateTime Timestamp => _timestamp;

        public LinkedMatch LinkedMatch => _linkedMatch;

        public Score TotalScore => new MatchScore(this);

        public Participant Participant => _participant;

        public IReadOnlyCollection<Stat> Stats => _stats;

        public void SnapshotStats(IChallengeStats stats, IChallengeScoring scoring)
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

                var stat = new Stat(Id, name, value, weighting);

                _stats.Add(stat);
            }
        }
    }
}
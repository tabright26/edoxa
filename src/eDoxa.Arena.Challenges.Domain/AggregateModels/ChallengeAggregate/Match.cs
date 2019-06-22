// Filename: Match.cs
// Date Created: 2019-06-20
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
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public partial class Match : Entity<MatchId>
    {
        private readonly HashSet<Stat> _stats = new HashSet<Stat>();

        public Match(GameMatchId gameMatchId, IDateTimeProvider synchronizedAt)
        {
            GameMatchId = gameMatchId;
            SynchronizedAt = synchronizedAt.DateTime;
        }

        public GameMatchId GameMatchId { get; }

        public DateTime SynchronizedAt { get; }

        public IReadOnlyCollection<Stat> Stats => _stats;

        public Score TotalScore => new MatchScore(this);

        public void SnapshotStats(IScoring scoring, IMatchStats stats)
        {
            // TODO: To refactor.
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

    public partial class Match : IEquatable<Match>
    {
        public bool Equals([CanBeNull] Match match)
        {
            return Id.Equals(match?.Id);
        }

        public sealed override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as Match);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

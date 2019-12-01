// Filename: Match.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed partial class Match : Entity<MatchId>, IMatch
    {
        private readonly HashSet<Stat> _stats;

        public Match(IEnumerable<Stat> stats, GameUuid gameUuid)
        {
            _stats = new HashSet<Stat>(stats);
            GameUuid = gameUuid;
        }

        public GameUuid GameUuid { get; }

        public Score Score => new MatchScore(this);

        public IReadOnlyCollection<Stat> Stats => _stats;
    }

    public sealed partial class Match : IEquatable<IMatch?>
    {
        public bool Equals(IMatch? match)
        {
            return Id.Equals(match?.Id);
        }

        public override bool Equals(object? obj)
        {
            return this.Equals(obj as IMatch);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

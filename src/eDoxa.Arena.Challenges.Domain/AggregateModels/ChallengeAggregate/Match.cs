// Filename: Match.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public abstract partial class Match : Entity<MatchId>, IMatch
    {
        private readonly HashSet<Stat> _stats;

        protected Match(IEnumerable<Stat> stats, GameReference gameReference, IDateTimeProvider synchronizedAt)
        {
            _stats = new HashSet<Stat>(stats);
            SynchronizedAt = synchronizedAt.DateTime;
            GameReference = gameReference;
        }

        public DateTime SynchronizedAt { get; }

        public GameReference GameReference { get; }

        public Score Score => new MatchScore(this);

        public IReadOnlyCollection<Stat> Stats => _stats;
    }

    public abstract partial class Match : IEquatable<IMatch?>
    {
        public bool Equals(IMatch? match)
        {
            return Id.Equals(match?.Id);
        }

        public sealed override bool Equals(object? obj)
        {
            return this.Equals(obj as IMatch);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

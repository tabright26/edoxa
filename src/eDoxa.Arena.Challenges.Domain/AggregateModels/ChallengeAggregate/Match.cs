// Filename: Match.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public abstract partial class Match : Entity<MatchId>, IMatch
    {
        private readonly HashSet<Stat> _stats = new HashSet<Stat>();

        [CanBeNull]
        private readonly GameScore _gameScore;

        protected Match(IEnumerable<Stat> stats, GameReference gameReference, IDateTimeProvider synchronizedAt) : this(gameReference, synchronizedAt)
        {
            _stats = new HashSet<Stat>(stats);
        }

        protected Match(GameScore gameScore, GameReference gameReference, IDateTimeProvider synchronizedAt) : this(gameReference, synchronizedAt)
        {
            _gameScore = gameScore;
        }

        private Match(GameReference gameReference, IDateTimeProvider synchronizedAt)
        {
            SynchronizedAt = synchronizedAt.DateTime;
            GameReference = gameReference;
        }

        public DateTime SynchronizedAt { get; }

        public GameReference GameReference { get; }

        public Score TotalScore => (Score) _gameScore ?? new MatchScore(this);

        public IReadOnlyCollection<Stat> Stats => _stats;
    }

    public abstract partial class Match : IEquatable<IMatch>
    {
        public bool Equals([CanBeNull] IMatch match)
        {
            return Id.Equals(match?.Id);
        }

        public sealed override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as IMatch);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

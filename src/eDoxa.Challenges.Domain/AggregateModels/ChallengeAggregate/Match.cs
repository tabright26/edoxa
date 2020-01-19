// Filename: Match.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed partial class Match : Entity<MatchId>, IMatch
    {
        private readonly HashSet<Stat> _stats;

        public Match(
            GameUuid gameUuid,
            IDateTimeProvider gameStartedAt,
            TimeSpan gameDuration,
            IEnumerable<Stat> stats,
            IDateTimeProvider synchronizedAt
        )
        {
            GameUuid = gameUuid;
            GameStartedAt = gameStartedAt.DateTime;
            GameDuration = gameDuration;
            SynchronizedAt = synchronizedAt.DateTime;
            _stats = new HashSet<Stat>(stats);
        }

        public GameUuid GameUuid { get; }

        public DateTime GameStartedAt { get; }

        public TimeSpan GameDuration { get; }

        public DateTime GameEndedAt => GameStartedAt + GameDuration;

        public DateTime SynchronizedAt { get; }

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

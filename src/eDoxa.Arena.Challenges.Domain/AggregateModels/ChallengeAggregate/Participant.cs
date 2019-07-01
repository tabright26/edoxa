// Filename: Participant.cs
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
using System.Linq;

using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public partial class Participant : Entity<ParticipantId>
    {
        private readonly HashSet<Match> _matches = new HashSet<Match>();

        public Participant(UserId userId, GameAccountId gameAccountId, IDateTimeProvider registeredAt)
        {
            UserId = userId;
            GameAccountId = gameAccountId;
            RegisteredAt = registeredAt.DateTime;
        }

        public UserId UserId { get; }

        public GameAccountId GameAccountId { get; }

        public DateTime RegisteredAt { get; }

        public DateTime? SynchronizedAt => _matches.Select(match => match.SynchronizedAt).Cast<DateTime?>().DefaultIfEmpty().Max();

        public IReadOnlyCollection<Match> Matches => _matches;

        public void Snapshot(Match match)
        {
            _matches.Add(match);
        }

        [CanBeNull]
        public Score AverageScore(BestOf bestOf)
        {
            return Matches.Count >= bestOf ? new ParticipantScore(this, bestOf) : null;
        }

        internal IEnumerable<GameReference> GetUnsynchronizedGameReferences(IEnumerable<GameReference> gameReferences)
        {
            return gameReferences.Where(gameReference => Matches.All(match => match.GameReference != gameReference));
        }
    }

    public partial class Participant : IEquatable<Participant>
    {
        public bool Equals([CanBeNull] Participant participant)
        {
            return Id.Equals(participant?.Id);
        }

        public sealed override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as Participant);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

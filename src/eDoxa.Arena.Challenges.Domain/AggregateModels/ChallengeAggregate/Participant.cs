// Filename: Participant.cs
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
            SynchronizedAt = null;
        }

        public UserId UserId { get; }

        public GameAccountId GameAccountId { get; }

        public DateTime RegisteredAt { get; }

        public DateTime? SynchronizedAt { get; private set; }

        public IReadOnlyCollection<Match> Matches => _matches;

        [CanBeNull]
        public Score AverageScore(BestOf bestOf)
        {
            return HasAverageScore(Matches.Count, bestOf) ? new ParticipantScore(this, bestOf) : null;
        }

        public static bool HasAverageScore(int matchCount, int bestOf)
        {
            return matchCount >= bestOf;
        }

        public bool HasFinalScore(ChallengeTimeline timeline)
        {
            return SynchronizedAt.HasValue && SynchronizedAt.Value >= timeline.EndedAt;
        }

        public void Synchronize(Match match)
        {
            _matches.Add(match);
        }

        public IEnumerable<GameReference> GetUnsynchronizedMatchReferences(IEnumerable<GameReference> gameReferences)
        {
            return gameReferences.Where(gameReference => Matches.All(match => match.GameReference != gameReference));
        }

        public void Synchronize(IDateTimeProvider synchronizedAt)
        {
            SynchronizedAt = synchronizedAt.DateTime;
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

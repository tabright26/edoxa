// Filename: Participant.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public partial class Participant : Entity<ParticipantId>
    {
        private readonly HashSet<IMatch> _matches = new HashSet<IMatch>();

        public Participant(UserId userId, PlayerId playerId, IDateTimeProvider registeredAt)
        {
            UserId = userId;
            PlayerId = playerId;
            RegisteredAt = registeredAt.DateTime;
            SynchronizedAt = null;
        }

        public UserId UserId { get; }

        public PlayerId PlayerId { get; }

        public DateTime RegisteredAt { get; }

        public DateTime? SynchronizedAt { get; private set; }

        public IReadOnlyCollection<IMatch> Matches => _matches;

        public void Snapshot(IEnumerable<IMatch> matches, IDateTimeProvider synchronizedAt)
        {
            foreach (var match in matches)
            {
                this.Snapshot(match);
            }

            this.Synchronize(synchronizedAt);
        }

        private void Snapshot(IMatch match)
        {
            if (!this.MatchExists(match))
            {
                _matches.Add(match);
            }
        }

        private void Synchronize(IDateTimeProvider synchronizedAt)
        {
            SynchronizedAt = synchronizedAt.DateTime;
        }

        private bool MatchExists(IMatch match)
        {
            return _matches.Any(x => x.GameUuid == match.GameUuid);
        }

        public Score? ComputeScore(BestOf bestOf)
        {
            return Matches.Count >= bestOf ? new ParticipantScore(this, bestOf) : null;
        }
    }

    public partial class Participant : IEquatable<Participant?>
    {
        public bool Equals(Participant? participant)
        {
            return Id.Equals(participant?.Id);
        }

        public sealed override bool Equals(object? obj)
        {
            return this.Equals(obj as Participant);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

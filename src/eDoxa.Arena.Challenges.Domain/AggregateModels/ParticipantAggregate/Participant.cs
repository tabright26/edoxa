// Filename: Participant.cs
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
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate
{
    public class Participant : Entity<ParticipantId>, IAggregateRoot
    {
        private HashSet<Match> _matches;

        public Participant(Challenge challenge, UserId userId, UserGameReference userGameReference) : this()
        {
            Challenge = challenge;
            UserId = userId;
            UserGameReference = userGameReference;
        }

        public Participant()
        {
            Timestamp = DateTime.UtcNow;
            LastSync = null;
            _matches = new HashSet<Match>();
        }

        public DateTime Timestamp { get; private set; }

        public DateTime? LastSync { get; private set; }

        public Challenge Challenge { get; private set; }

        public UserId UserId { get; private set; }

        public UserGameReference UserGameReference { get; private set; }

        [CanBeNull]
        public Score AverageScore => Matches.Count >= Challenge.Setup.BestOf ? new ParticipantScore(this) : null;

        public IReadOnlyCollection<Match> Matches
        {
            get => _matches;
            set => _matches = new HashSet<Match>(value);
        }

        public bool HasFinalScore(ChallengeTimeline timeline)
        {
            return LastSync.HasValue && LastSync.Value >= timeline.EndedAt;
        }

        public void SnapshotMatch(MatchReference matchReference, IMatchStats stats)
        {
            _matches.Add(new Match(this, matchReference, stats));
        }

        public IEnumerable<MatchReference> GetUnsynchronizedMatchReferences(IEnumerable<MatchReference> matchReferences)
        {
            return matchReferences.Where(matchReference => Matches.All(match => match.Reference != matchReference));
        }

        internal void Sync()
        {
            LastSync = DateTime.UtcNow;
        }
    }
}

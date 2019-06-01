// Filename: Participant.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Domain.Abstractions;
using eDoxa.Arena.Domain.ValueObjects;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Common;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate
{
    public class Participant : Entity<ParticipantId>, IAggregateRoot
    {
        private HashSet<Match> _matches;

        public Participant(Challenge challenge, UserId userId, ExternalAccount externalAccount) : this()
        {
            Challenge = challenge;
            UserId = userId;
            ExternalAccount = externalAccount;
        }

        private Participant()
        {
            Timestamp = DateTime.UtcNow;
            _matches = new HashSet<Match>();
        }

        public DateTime Timestamp { get; private set; }

        public ExternalAccount ExternalAccount { get; private set; }

        public UserId UserId { get; private set; }

        public Challenge Challenge { get; private set; }

        [CanBeNull]
        public Score AverageScore => Matches.Count >= Challenge.Setup.BestOf ? new ParticipantScore(this) : null;

        public IReadOnlyCollection<Match> Matches => _matches;

        public void SnapshotMatch(IMatchStats stats, IScoring scoring)
        {
            var match = new Match(this, stats.MatchExternalId);

            match.SnapshotStats(stats, scoring);

            _matches.Add(match);
        }
    }
}
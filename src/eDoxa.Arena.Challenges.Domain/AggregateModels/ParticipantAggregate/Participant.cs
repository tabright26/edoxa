﻿// Filename: Participant.cs
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
using eDoxa.Functional;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Entities;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate
{
    public class Participant : Entity<ParticipantId>, IAggregateRoot
    {
        private Challenge _challenge;
        private ParticipantExternalAccount _participantExternalAccount;
        private HashSet<Match> _matches;
        private DateTime _timestamp;
        private UserId _userId;

        public Participant(Challenge challenge, UserId userId, ParticipantExternalAccount participantExternalAccount) : this()
        {
            _challenge = challenge;
            _userId = userId;
            _participantExternalAccount = participantExternalAccount;
        }

        private Participant()
        {
            _timestamp = DateTime.UtcNow;
            _matches = new HashSet<Match>();
        }

        public DateTime Timestamp => _timestamp;

        public ParticipantExternalAccount ParticipantExternalAccount => _participantExternalAccount;

        public UserId UserId => _userId;

        public Challenge Challenge => _challenge;

        public Option<Score> AverageScore => Matches.Count >= Challenge.Setup.BestOf ? new Option<Score>(new ParticipantScore(this)) : new Option<Score>();

        public IReadOnlyCollection<Match> Matches => _matches;

        public void SnapshotMatch(IMatchStats stats, IScoring scoring)
        {
            var match = new Match(this, stats.MatchExternalId);

            match.SnapshotStats(stats, scoring);

            _matches.Add(match);
        }
    }
}
﻿// Filename: Participant.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Adapters;
using eDoxa.Challenges.Domain.ValueObjects;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class Participant : Entity<ParticipantId>
    {
        private Challenge _challenge;
        private LinkedAccount _linkedAccount;
        private HashSet<Match> _matches;
        private DateTime _timestamp;
        private UserId _userId;

        internal Participant(Challenge challenge, UserId userId, LinkedAccount linkedAccount) : this()
        {
            _challenge = challenge;
            _userId = userId;
            _linkedAccount = linkedAccount;
        }

        private Participant()
        {
            _timestamp = DateTime.UtcNow;
            _matches = new HashSet<Match>();
        }

        public DateTime Timestamp => _timestamp;

        public LinkedAccount LinkedAccount => _linkedAccount;

        public Score AverageScore => new ParticipantScoreAdapter(this).Score;

        public UserId UserId => _userId;

        public Challenge Challenge => _challenge;

        public IReadOnlyCollection<Match> Matches => _matches;

        public void SnapshotMatch(IChallengeStats stats, IChallengeScoring scoring)
        {
            var match = new Match(this, stats.LinkedMatch);

            match.SnapshotStats(stats, scoring);

            _matches.Add(match);
        }
    }
}
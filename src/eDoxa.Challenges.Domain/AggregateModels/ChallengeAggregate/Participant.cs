// Filename: Participant.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;
using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class Participant : Entity<ParticipantId>
    {
        private DateTime _timestamp;
        private LinkedAccount _linkedAccount;
        private UserId _userId;
        private Challenge _challenge;
        private HashSet<Match> _matches;

        internal Participant(Challenge challenge, UserId userId, LinkedAccount linkedAccount) : this()
        {
            _challenge = challenge ?? throw new ArgumentNullException(nameof(challenge));
            _userId = userId ?? throw new ArgumentNullException(nameof(userId));
            _linkedAccount = linkedAccount ?? throw new ArgumentNullException(nameof(linkedAccount));
        }

        private Participant()
        {
            _timestamp = DateTime.UtcNow;
            _matches = new HashSet<Match>();
        }

        public DateTime Timestamp
        {
            get
            {
                return _timestamp;
            }
        }

        public LinkedAccount LinkedAccount
        {
            get
            {
                return _linkedAccount;
            }
        }

        [CanBeNull]
        public Score AverageScore
        {
            get
            {
                return Score.FromParticipant(this);
            }
        }

        public UserId UserId
        {
            get
            {
                return _userId;
            }
        }

        public Challenge Challenge
        {
            get
            {
                return _challenge;
            }
        }

        public IReadOnlyCollection<Match> Matches
        {
            get
            {
                return _matches;
            }
        }

        public void SnapshotMatch(IChallengeStats stats, IChallengeScoring scoring)
        {
            var match = new Match(this, stats.LinkedMatch);

            match.SnapshotStats(stats, scoring);

            _matches.Add(match);
        }
    }
}
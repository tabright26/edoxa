// Filename: Challenge.cs
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
using System.ComponentModel;
using System.Linq;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.DomainEvent;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Factories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Common.Enums;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class Challenge : Entity<ChallengeId>, IAggregateRoot
    {
        private Game _game;
        private ChallengeName _name;
        private HashSet<Participant> _participants;
        private IChallengeScoring _scoring;
        private ChallengeSettings _settings;
        private ChallengeTimeline _timeline;

        internal Challenge(Game game, ChallengeName name, ChallengePublisherPeriodicity periodicity) : this(game, name)
        {
            _settings = new ChallengeSettings(periodicity);
            _timeline = new ChallengeTimeline(periodicity);
        }

        internal Challenge(Game game, ChallengeName name, ChallengeSettings settings) : this(game, name)
        {
            _settings = settings;
        }

        internal Challenge(Game game, ChallengeName name) : this()
        {
            Game = game;
            _name = name;
        }

        private Challenge()
        {
            _game = Game.None;
            _name = null;
            _settings = new ChallengeSettings();
            _timeline = new ChallengeTimeline();
            _scoring = null;
            _participants = new HashSet<Participant>();
        }

        public Game Game
        {
            get => _game;
            private set
            {
                if (!Enum.IsDefined(typeof(Game), value))
                {
                    throw new InvalidEnumArgumentException(nameof(Game), (int) value, typeof(Game));
                }

                if (value == Game.None || value == Game.All)
                {
                    throw new ArgumentException(nameof(Game));
                }

                _game = value;
            }
        }

        public ChallengeName Name => _name;

        public ChallengeSettings Settings => _settings;

        public ChallengeTimeline Timeline => _timeline;

        public IChallengeScoring Scoring
        {
            get => _scoring;
            protected set => _scoring = value;
        }

        public ChallengeLiveData LiveData => new ChallengeLiveData(Settings, Participants);

        public IChallengePayout Payout
        {
            get
            {
                var factory = ChallengePayoutFactory.Instance;

                var strategy = factory.CreatePayout(Settings.Type, Settings.PayoutEntries, Settings.PrizePool, Settings.EntryFee);

                return strategy.Payout;
            }
        }

        public IChallengeScoreboard Scoreboard
        {
            get
            {
                var factory = ChallengeScoreboardFactory.Instance;

                var strategy = factory.Create(this);

                return strategy.Scoreboard;
            }
        }

        public IReadOnlyCollection<Participant> Participants => _participants;

        public void Configure(IChallengeScoringStrategy strategy, DateTime publishedAt, TimeSpan registrationPeriod, TimeSpan extensionPeriod)
        {
            _scoring = strategy.Scoring;

            _timeline = Timeline.Configure(publishedAt, registrationPeriod, extensionPeriod);
        }

        public void Configure(IChallengeScoringStrategy strategy, DateTime publishedAt)
        {
            _scoring = strategy.Scoring;

            _timeline = Timeline.Configure(publishedAt);
        }

        public void Publish(IChallengeScoringStrategy strategy, TimeSpan registrationPeriod, TimeSpan extensionPeriod)
        {
            _scoring = strategy.Scoring;

            _timeline = Timeline.Publish(registrationPeriod, extensionPeriod);
        }

        public void Publish(IChallengeScoringStrategy strategy)
        {
            _scoring = strategy.Scoring;

            _timeline = Timeline.Publish();
        }

        public void Close()
        {
            _timeline = Timeline.Close();

            var userPrizes = Payout.Snapshot(Scoreboard);

            var domainEvent = new ChallengeUserPrizesSnapshottedDomainEvent(Id.ToGuid(), userPrizes);

            this.AddDomainEvent(domainEvent);
        }

        public Participant RegisterParticipant(UserId userId, LinkedAccount linkedAccount)
        {
            if (Participants.Any(x => x.UserId == userId))
            {
                throw new ArgumentException("The participant is already registered.", nameof(userId));
            }

            if (LiveData.Entries >= Settings.Entries)
            {
                throw new InvalidOperationException("The maximum number of participants has been reached.");
            }

            if (!Timeline.State.HasFlag(ChallengeState.Opened))
            {
                throw new InvalidOperationException("The participant can only register during the registration period.");
            }

            var participant = new Participant(this, userId, linkedAccount);

            _participants.Add(participant);

            return participant;
        }

        public void SnapshotParticipantMatch(ParticipantId participantId, IChallengeStats stats)
        {
            var participant = Participants.SingleOrDefault(x => x.Id == participantId);

            if (participant == null)
            {
                throw new ArgumentException($"This {nameof(participantId)} ({participantId}) does not exist.", nameof(participantId));
            }

            participant.SnapshotMatch(stats, Scoring);
        }
    }
}
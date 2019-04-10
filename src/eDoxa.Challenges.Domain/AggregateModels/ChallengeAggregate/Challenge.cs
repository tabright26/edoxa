// Filename: Challenge.cs
// Date Created: 2019-03-22
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
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
        private ChallengeSettings _settings;
        private ChallengeTimeline _timeline;
        private IChallengeScoring _scoring;
        private HashSet<Participant> _participants;

        internal Challenge(Game game, ChallengeName name, ChallengePublisherPeriodicity periodicity) : this(game, name)
        {
            Settings = new ChallengeSettings(periodicity);
            Timeline = new ChallengeTimeline(periodicity);
        }

        internal Challenge(Game game, ChallengeName name, ChallengeSettings settings) : this(game, name)
        {
            Settings = settings;
        }

        internal Challenge(Game game, ChallengeName name) : this()
        {
            Game = game;
            Name = name;
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
            get
            {
                return _game;
            }
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

        public ChallengeName Name
        {
            get
            {
                return _name;
            }
            private set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(Name));
                }

                _name = value;
            }
        }

        public ChallengeSettings Settings
        {
            get
            {
                return _settings;
            }
            private set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(Settings));
                }

                _settings = value;
            }
        }

        public ChallengeTimeline Timeline
        {
            get
            {
                return _timeline;
            }
            private set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(Timeline));
                }

                _timeline = value;
            }
        }

        public IChallengeScoring Scoring
        {
            get
            {
                return _scoring;
            }
            protected set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(Scoring));
                }

                _scoring = value;
            }
        }

        public ChallengeLiveData LiveData
        {
            get
            {
                return new ChallengeLiveData(this);
            }
        }

        public IChallengePrizeBreakdown PrizeBreakdown
        {
            get
            {
                var factory = ChallengePrizeBreakdownFactory.Instance;

                var strategy = factory.Create(Settings.Type, Settings.PayoutEntries, Settings.PrizePool);

                return strategy.PrizeBreakdown;
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

        public IReadOnlyCollection<Participant> Participants
        {
            get
            {
                return _participants;
            }
        }

        public void Configure(IChallengeScoringStrategy strategy, DateTime publishedAt, TimeSpan registrationPeriod, TimeSpan extensionPeriod)
        {
            Scoring = strategy.Scoring;

            Timeline = Timeline.Configure(publishedAt, registrationPeriod, extensionPeriod);
        }

        public void Configure(IChallengeScoringStrategy strategy, DateTime publishedAt)
        {
            Scoring = strategy.Scoring;

            Timeline = Timeline.Configure(publishedAt);
        }

        public void Publish(IChallengeScoringStrategy strategy, TimeSpan registrationPeriod, TimeSpan extensionPeriod)
        {
            Scoring = strategy.Scoring;

            Timeline = Timeline.Publish(registrationPeriod, extensionPeriod);
        }

        public void Publish(IChallengeScoringStrategy strategy)
        {
            Scoring = strategy.Scoring;

            Timeline = Timeline.Publish();
        }

        public void Close()
        {
            Timeline = Timeline.Close();

            var userPrizes = PrizeBreakdown.SnapshotUserPrizes(Scoreboard);

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
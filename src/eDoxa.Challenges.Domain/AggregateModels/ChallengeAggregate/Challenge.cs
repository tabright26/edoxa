// Filename: Challenge.cs
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
using System.ComponentModel;
using System.Linq;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.DomainEvent;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Factories;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Specifications;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Common.Enums;
using eDoxa.Specifications.Factories;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class Challenge : Entity<ChallengeId>, IAggregateRoot
    {
        private Game _game;
        private ChallengeName _name;
        private HashSet<Participant> _participants;
        private IChallengeScoring _scoring;
        private ChallengeSetup _setup;
        private ChallengeTimeline _timeline;

        internal Challenge(Game game, ChallengeName name, ChallengePublisherPeriodicity periodicity) : this(game, name)
        {
            _setup = new ChallengeSetup(periodicity);
            _timeline = new ChallengeTimeline(periodicity);
        }

        internal Challenge(Game game, ChallengeName name, ChallengeSetup setup) : this(game, name)
        {
            _setup = setup;
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
            _setup = new ChallengeSetup();
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

        public ChallengeSetup Setup => _setup;

        public ChallengeTimeline Timeline => _timeline;

        public IChallengeScoring Scoring
        {
            get => _scoring;
            protected set => _scoring = value;
        }

        public ChallengeLiveData LiveData => new ChallengeLiveData(Setup, Participants);

        public IChallengePayout Payout
        {
            get
            {
                var factory = ChallengePayoutFactory.Instance;

                var strategy = factory.CreatePayout(Setup.Type, Setup.PayoutEntries, Setup.PrizePool, Setup.EntryFee);

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

        public void RegisterParticipant(UserId userId, LinkedAccount linkedAccount)
        {
            if (!this.CanRegisterParticipant(userId))
            {
                throw new InvalidOperationException();
            }

            _participants.Add(new Participant(this, userId, linkedAccount));
        }

        private bool CanRegisterParticipant(UserId userId)
        {
            var specification = SpecificationFactory.Instance.CreateSpecification<Challenge>()
                .And(new ParticipantAlreadyRegisteredSpecification(userId).Not())
                .And(new ChallengeFullSpecification().Not())
                .And(new ChallengeOpenedSpecification());

            return specification.IsSatisfiedBy(this);
        }

        public void SnapshotParticipantMatch(ParticipantId participantId, IChallengeStats stats)
        {
            if (!this.CanSnapshotParticipantMatch(participantId))
            {
                throw new InvalidOperationException();
            }

            Participants.Single(participant => participant.Id == participantId).SnapshotMatch(stats, Scoring);
        }

        private bool CanSnapshotParticipantMatch(ParticipantId participantId)
        {
            var specification = SpecificationFactory.Instance.CreateSpecification<Challenge>()
                .And(new ParticipantExistsSpecification(participantId));

            return specification.IsSatisfiedBy(this);
        }
    }
}
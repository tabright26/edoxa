﻿// Filename: Challenge.cs
// Date Created: 2019-05-06
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

using eDoxa.Challenges.Domain.Entities.Abstractions;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate.Specifications;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ParticipantAggregate;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ParticipantAggregate.Specifications;
using eDoxa.Functional.Maybe;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Enumerations;
using eDoxa.Specifications.Factories;

namespace eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate
{
    public class Challenge : Entity<ChallengeId>, IAggregateRoot
    {
        private Game _game;
        private ChallengeName _name;
        private HashSet<Participant> _participants;
        private Option<IScoring> _scoring;
        private ChallengeSetup _setup;
        private ChallengeTimeline _timeline;

        public Challenge(Game game, ChallengeName name, ChallengeSetup setup) : this()
        {
            if (!Enum.IsDefined(typeof(Game), game))
            {
                throw new InvalidEnumArgumentException(nameof(Game), (int) game, typeof(Game));
            }

            if (game == Game.None || game == Game.All)
            {
                throw new ArgumentException(nameof(Game));
            }

            _game = game;
            _name = name;
            _setup = setup;
        }

        private Challenge()
        {
            _timeline = new ChallengeTimeline();
            _scoring = new Option<IScoring>();
            _participants = new HashSet<Participant>();
        }

        public Game Game => _game;

        public ChallengeName Name => _name;

        public ChallengeSetup Setup => _setup;

        public ChallengeTimeline Timeline => _timeline;

        //public ChallengeLiveData LiveData => new ChallengeLiveData(Setup, Participants);

        public Option<IScoring> Scoring => _scoring;

        //public IPayout Payout => PayoutFactory.Instance.CreatePayout(Setup.Type, Setup.PayoutEntries, Setup.PrizePool, Setup.EntryFee).Payout;

        //public IScoreboard Scoreboard => ScoreboardFactory.Instance.CreateScoreboard(this).Scoreboard;

        public IReadOnlyCollection<Participant> Participants => _participants;

        public void Configure(IScoringStrategy strategy, DateTime publishedAt, TimeSpan registrationPeriod, TimeSpan extensionPeriod)
        {
            _scoring = new Option<IScoring>(strategy.Scoring);

            _timeline = Timeline.Configure(publishedAt, registrationPeriod, extensionPeriod);
        }

        public void Configure(IScoringStrategy strategy, DateTime publishedAt)
        {
            _scoring = new Option<IScoring>(strategy.Scoring);

            _timeline = Timeline.Configure(publishedAt);
        }

        private bool CanConfigure()
        {
            var specification = SpecificationFactory.Instance.CreateSpecification<Challenge>()
                .And(new ChallengeDraftSpecification());

            return specification.IsSatisfiedBy(this);
        }

        public void Publish(IScoringStrategy strategy, TimeSpan registrationPeriod, TimeSpan extensionPeriod)
        {
            _scoring = new Option<IScoring>(strategy.Scoring);

            _timeline = Timeline.Publish(registrationPeriod, extensionPeriod);
        }

        public void Publish(IScoringStrategy strategy)
        {
            _scoring = new Option<IScoring>(strategy.Scoring);

            _timeline = Timeline.Publish();
        }

        public void Publish(IScoringStrategy scoringStrategy, IChallengeTimelineStrategy challengeTimelineStrategy)
        {
            _scoring = new Option<IScoring>(scoringStrategy.Scoring);

            _timeline = Timeline.Publish(challengeTimelineStrategy.Timeline.RegistrationPeriod.Value, challengeTimelineStrategy.Timeline.ExtensionPeriod.Value);
        }

        private bool CanPublish()
        {
            var specification = SpecificationFactory.Instance.CreateSpecification<Challenge>()
                .And(new ChallengeDraftSpecification());

            return specification.IsSatisfiedBy(this);
        }

        public void Complete()
        {
            if (!this.CanClose())
            {
                throw new InvalidOperationException();
            }

            _timeline = Timeline.Close();

            //this.AddDomainEvent(new PayoutProcessedDomainEvent(Id, Payout.Payoff(Scoreboard)));
        }

        private bool CanClose()
        {
            var specification = SpecificationFactory.Instance.CreateSpecification<Challenge>()
                .And(new ChallengeEndedSpecification());

            return specification.IsSatisfiedBy(this);
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
                .And(new ChallengeIsFullSpecification().Not())
                .And(new ChallengeOpenedSpecification());

            return specification.IsSatisfiedBy(this);
        }

        public void SnapshotParticipantMatch(ParticipantId participantId, IMatchStats stats)
        {
            if (!this.CanSnapshotParticipantMatch(participantId))
            {
                throw new InvalidOperationException();
            }

            Participants.Single(participant => participant.Id == participantId).SnapshotMatch(stats, Scoring.Select(scoring => scoring).Single());
        }

        private bool CanSnapshotParticipantMatch(ParticipantId participantId)
        {
            var specification = SpecificationFactory.Instance.CreateSpecification<Challenge>()
                .And(new ParticipantExistsSpecification(participantId))
                .And(new ChallengeMininumInProgressSpecification());

            return specification.IsSatisfiedBy(this);
        }
    }
}
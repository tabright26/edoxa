// Filename: Challenge.cs
// Date Created: 2019-05-20
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
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Specifications;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate.Specifications;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Enumerations;
using eDoxa.Specifications.Factories;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class Challenge : Entity<ChallengeId>, IAggregateRoot
    {
        private Game _game;
        private ChallengeName _name;
        private ChallengeSetup _setup;
        private ChallengeDuration _duration;
        private ChallengeCreatedAt _createdAt;
        private ChallengeStartedAt _startedAt;
        private ChallengeCompletedAt _completedAt;
        private IScoring _scoring;
        private HashSet<Participant> _participants;

        public Challenge(
            Game game,
            ChallengeName name,
            ChallengeSetup setup,
            ChallengeDuration duration,
            IScoringStrategy strategy
        ) : this()
        {
            _game = game;
            _name = name;
            _setup = setup;
            _duration = duration;
            _scoring = strategy.Scoring;
        }

        private Challenge()
        {
            _createdAt = new ChallengeCreatedAt();
            _startedAt = null;
            _completedAt = null;
            _participants = new HashSet<Participant>();
        }

        public Game Game => _game;

        public ChallengeName Name => _name;

        public ChallengeSetup Setup => _setup;

        public ChallengeDuration Duration => _duration;

        public ChallengeCreatedAt CreatedAt => _createdAt;

        [CanBeNull]
        public ChallengeStartedAt StartedAt => _startedAt;

        [CanBeNull]
        public ChallengeEndedAt EndedAt => _startedAt != null ? _startedAt + _duration : null;

        [CanBeNull]
        public ChallengeCompletedAt CompletedAt => _completedAt;

        public IScoring Scoring => _scoring;

        public IPayout Payout => PayoutFactory.Instance.Create(Setup.PayoutEntries, this.DeterminePayoutPrize());

        public IScoreboard Scoreboard => new Scoreboard(this);

        public IReadOnlyCollection<Participant> Participants => _participants;

        public void Complete()
        {
            if (!this.CanComplete())
            {
                throw new InvalidOperationException();
            }

            _completedAt = new ChallengeCompletedAt();

            //this.AddDomainEvent(new PayoutProcessedDomainEvent(Id, Payout.Payoff(Scoreboard)));
        }

        private bool CanComplete()
        {
            var specification = SpecificationFactory.Instance.CreateSpecification<Challenge>()
                /*       .And(new ChallengeEndedSpecification())*/;

            return specification.IsSatisfiedBy(this);
        }

        public void RegisterParticipant(UserId userId, ParticipantExternalAccount externalAccount)
        {
            if (!this.CanRegisterParticipant(userId))
            {
                throw new InvalidOperationException();
            }

            if (Participants.Count == Setup.Entries - 1)
            {
                _startedAt = new ChallengeStartedAt();
            }

            _participants.Add(new Participant(this, userId, externalAccount));
        }

        private bool CanRegisterParticipant(UserId userId)
        {
            var specification = SpecificationFactory.Instance.CreateSpecification<Challenge>()
                                                    .And(new ParticipantAlreadyRegisteredSpecification(userId).Not())
                                                    .And(new ChallengeIsFullSpecification().Not())
                /*            .And(new ChallengeOpenedSpecification())*/;

            return specification.IsSatisfiedBy(this);
        }

        public void SnapshotParticipantMatch(ParticipantId participantId, IMatchStats stats)
        {
            if (!this.CanSnapshotParticipantMatch(participantId))
            {
                throw new InvalidOperationException();
            }

            Participants.Single(participant => participant.Id == participantId).SnapshotMatch(stats, Scoring);
        }

        private bool CanSnapshotParticipantMatch(ParticipantId participantId)
        {
            var specification = SpecificationFactory.Instance.CreateSpecification<Challenge>().And(new ParticipantExistsSpecification(participantId))
                /*        .And(new ChallengeMininumInProgressSpecification())*/;

            return specification.IsSatisfiedBy(this);
        }

        private Prize DeterminePayoutPrize()
        {
            if (Setup.EquivalentCurrency)
            {
                if (Setup.EntryFee.Currency == Currency.Money)
                {
                    return new MoneyPrize(Setup.EntryFee);
                }

                if (Setup.EntryFee.Currency == Currency.Token)
                {
                    return new TokenPrize(Setup.EntryFee);
                }
            }
            else
            {
                if (Setup.EntryFee.Currency == Currency.Token)
                {
                    return new MoneyPrize(Setup.EntryFee / 1000);
                }

                if (Setup.EntryFee.Currency == Currency.Money)
                {
                    return new TokenPrize(Setup.EntryFee * 1000);
                }
            }

            throw new InvalidOperationException();
        }
    }
}

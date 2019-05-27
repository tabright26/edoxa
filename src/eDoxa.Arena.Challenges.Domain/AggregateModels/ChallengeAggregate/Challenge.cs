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
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.DomainEvents;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Arena.Challenges.Domain.Specifications;
using eDoxa.Arena.Domain;
using eDoxa.Arena.Domain.Abstractions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Entities;
using eDoxa.Seedwork.Domain.Enumerations;
using eDoxa.Specifications.Factories;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class Challenge : Entity<ChallengeId>, IAggregateRoot
    {
        private HashSet<Participant> _participants;

        public Challenge(
            Game game,
            ChallengeName name,
            ChallengeSetup setup,
            ChallengeDuration duration,
            IScoring scoring,
            bool isFake = false
        ) : this()
        {
            Game = game;
            Name = name;
            Setup = setup;
            Duration = duration;
            Scoring = scoring;
            IsFake = isFake;
        }

        private Challenge()
        {
            CreatedAt = new ChallengeCreatedAt();
            StartedAt = null;
            CompletedAt = null;
            _participants = new HashSet<Participant>();
        }

        public Game Game { get; private set; }

        public ChallengeName Name { get; private set; }

        public ChallengeSetup Setup { get; private set; }

        public ChallengeDuration Duration { get; private set; }

        public ChallengeCreatedAt CreatedAt { get; private set; }

        [CanBeNull]
        public ChallengeStartedAt StartedAt { get; private set; }

        [CanBeNull]
        public ChallengeEndedAt EndedAt => StartedAt != null ? StartedAt + Duration : null;

        [CanBeNull]
        public ChallengeCompletedAt CompletedAt { get; private set; }

        public bool IsFake { get; private set; }

        public IScoring Scoring { get; private set; }

        public IPayout Payout => PayoutFactory.Instance.Create(Setup.PayoutEntries, Setup.EntryFee);

        public IScoreboard Scoreboard => new Scoreboard(this);

        public IReadOnlyCollection<Participant> Participants => _participants;

        public void Complete()
        {
            if (!this.CanComplete())
            {
                throw new InvalidOperationException();
            }

            CompletedAt = new ChallengeCompletedAt();

            this.AddDomainEvent(new ChallengePayoutDomainEvent(Id, Payout.GetParticipantPrizes(Scoreboard)));
        }

        private bool CanComplete()
        {
            var specification = SpecificationFactory.Instance.CreateSpecification<Challenge>();

            return specification.IsSatisfiedBy(this);
        }

        public Participant RegisterParticipant(UserId userId, ExternalAccount externalAccount)
        {
            if (!this.CanRegisterParticipant(userId))
            {
                throw new InvalidOperationException();
            }

            if (Participants.Count == Setup.Entries - 1)
            {
                StartedAt = new ChallengeStartedAt();
            }

            var participant = new Participant(this, userId, externalAccount);

            _participants.Add(participant);

            return participant;
        }

        private bool CanRegisterParticipant(UserId userId)
        {
            var specification = SpecificationFactory.Instance.CreateSpecification<Challenge>()
                .And(new UserIsRegisteredSpecification(userId).Not())
                .And(new ChallengeRegisterIsAvailableSpecification().Not());

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
            var specification = SpecificationFactory.Instance.CreateSpecification<Challenge>().And(new ParticipantIsRegisteredSpecification(participantId));

            return specification.IsSatisfiedBy(this);
        }
    }
}

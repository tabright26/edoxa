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

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class Challenge : Entity<ChallengeId>, IAggregateRoot
    {
        private HashSet<Participant> _participants;

        public Challenge(
            Game game,
            ChallengeName name,
            ChallengeSetup setup,
            ChallengeTimeline timeline,
            IScoring scoring,
            bool testMode = false
        ) : this()
        {
            Game = game;
            Name = name;
            Setup = setup;
            Timeline = timeline;
            Scoring = scoring;
            TestMode = testMode;
        }

        private Challenge()
        {
            CreatedAt = DateTime.UtcNow;
            _participants = new HashSet<Participant>();
        }

        public Game Game { get; private set; }

        public ChallengeName Name { get; private set; }

        public ChallengeSetup Setup { get; private set; }

        public ChallengeTimeline Timeline { get; protected set; }

        public DateTime CreatedAt { get; private set; }

        public bool TestMode { get; private set; }

        public IScoring Scoring { get; private set; }

        public IPayout Payout => PayoutFactory.Instance.Create(Setup.PayoutEntries, Setup.EntryFee);

        public ChallengeState State => ChallengeState.GetState(Timeline);

        public IScoreboard Scoreboard => new Scoreboard(this);

        public IReadOnlyCollection<Participant> Participants => _participants;

        public void Close()
        {
            if (!this.CanComplete())
            {
                throw new InvalidOperationException();
            }

            Timeline = Timeline.Close();

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
                Timeline = Timeline.Start();
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

// Filename: Challenge.cs
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
using System.Linq;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Specifications;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate.Specifications;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Enumerations;
using eDoxa.Specifications.Factories;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class Challenge : Entity<ChallengeId>, IAggregateRoot
    {
        private Game _game;
        private ChallengeName _name;
        private ChallengeSetup _setup;
        private IPayout _payout;
        private IScoring _scoring;
        private HashSet<Participant> _participants;
        
        public Challenge(Game game, ChallengeName name, ChallengeSetup setup, IPayout payout, IScoringStrategy strategy) : this()
        {
            _game = game;
            _name = name;
            _setup = setup;
            _payout = payout;
            _scoring = strategy.Scoring;
        }

        private Challenge()
        {
            _participants = new HashSet<Participant>();
        }

        public Game Game => _game;

        public ChallengeName Name => _name;

        public ChallengeSetup Setup => _setup;

        public Scoreboard Scoreboard => new Scoreboard(this);

        public IScoring Scoring => _scoring;

        public IPayout Payout => _payout;

        public IReadOnlyCollection<Participant> Participants => _participants;

        public void Complete()
        {
            if (!this.CanComplete())
            {
                throw new InvalidOperationException();
            }

            //_timeline = Timeline.Close();

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
            var specification = SpecificationFactory.Instance.CreateSpecification<Challenge>()
                .And(new ParticipantExistsSpecification(participantId))
        /*        .And(new ChallengeMininumInProgressSpecification())*/;

            return specification.IsSatisfiedBy(this);
        }
    }
}
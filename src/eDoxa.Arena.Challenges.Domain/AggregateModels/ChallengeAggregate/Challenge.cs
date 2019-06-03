// Filename: Challenge.cs
// Date Created: 2019-06-01
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
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.DomainEvents;
using eDoxa.Arena.Challenges.Domain.Specifications;
using eDoxa.Arena.Domain;
using eDoxa.Arena.Domain.Abstractions;
using eDoxa.Arena.Domain.ValueObjects;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Seedwork.Domain.Common.Enumerations;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Specifications;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class Challenge : Entity<ChallengeId>, IChallenge, IAggregateRoot
    {
        private HashSet<Participant> _participants;
        private HashSet<ChallengeStat> _stats;
        private List<Bucket> _buckets;

        public Challenge(
            Game game,
            ChallengeName name,
            ChallengeSetup setup,
            ChallengeTimeline timeline
        ) : this()
        {
            Game = game;
            Name = name;
            Setup = setup;
            Timeline = timeline;
        }

        private Challenge()
        {
            TestMode = null;
            CreatedAt = DateTime.UtcNow;
            _stats = new HashSet<ChallengeStat>();
            _participants = new HashSet<Participant>();
            _buckets = new List<Bucket>();
        }

        public Game Game { get; private set; }

        public ChallengeName Name { get; private set; }

        public ChallengeSetup Setup { get; private set; }

        public ChallengeTimeline Timeline { get; private set; }

        public TestMode TestMode { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public IReadOnlyCollection<ChallengeStat> Stats => _stats;

        public IReadOnlyCollection<Participant> Participants => _participants;

        public IReadOnlyCollection<Bucket> Buckets => _buckets;

        public void ApplyScoringStrategy(IScoringStrategy strategy)
        {
            strategy.Scoring.ForEach(stat => _stats.Add(new ChallengeStat(stat.Key, stat.Value)));
        }

        public void ApplyPayoutStrategy(IPayoutStrategy strategy)
        {
            strategy.Payout.Buckets.ForEach(bucket => _buckets.Add(bucket));
        }

        public void EnableTestMode(TestMode testMode, ChallengeTimeline timeline)
        {
            TestMode = testMode;

            Timeline = timeline;
        }

        public void DistributePrizes(Scoreboard scoreboard)
        {
            Timeline = Timeline.Close();

            var payout = new Payout(new Buckets(Buckets));

            var participantPrizes = payout.GetParticipantPrizes(scoreboard);

            var domainEvent = new ChallengePayoutDomainEvent(Id, participantPrizes);

            this.AddDomainEvent(domainEvent);
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

            Participants.Single(participant => participant.Id == participantId).SnapshotMatch(stats, new Scoring(Stats));
        }

        private bool CanSnapshotParticipantMatch(ParticipantId participantId)
        {
            var specification = SpecificationFactory.Instance.CreateSpecification<Challenge>().And(new ParticipantIsRegisteredSpecification(participantId));

            return specification.IsSatisfiedBy(this);
        }
    }
}

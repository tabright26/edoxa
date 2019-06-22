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
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.Abstractions.Factories;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Arena.Challenges.Domain.Validators;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public partial class Challenge : Entity<ChallengeId>, IChallenge
    {
        private readonly HashSet<Participant> _participants = new HashSet<Participant>();

        public Challenge(
            ChallengeName name,
            ChallengeGame game,
            ChallengeSetup setup,
            ChallengeDuration duration,
            IDateTimeProvider createdAt = null,
            IScoring scoring = null,
            IPayout payout = null
        )
        {
            Name = name;
            Game = game;
            Timeline = new ChallengeTimeline(duration);
            Setup = setup;
            CreatedAt = createdAt?.DateTime ?? new UtcNowDateTimeProvider().DateTime;
            SynchronizedAt = null;
            Scoring = scoring ?? ScoringFactory.Instance.CreateStrategy(this).Scoring;
            Payout = payout ?? PayoutFactory.Instance.CreateStrategy(this).Payout;
        }

        public DateTime CreatedAt { get; }

        public DateTime? SynchronizedAt { get; private set; }

        public ChallengeName Name { get; }

        public ChallengeGame Game { get; }

        public ChallengeState State => Timeline;

        public ChallengeTimeline Timeline { get; private set; }

        public ChallengeSetup Setup { get; }

        public IScoring Scoring { get; }

        public IPayout Payout { get; }

        public IReadOnlyCollection<Participant> Participants => _participants;

        public IScoreboard Scoreboard => new Scoreboard(this);

        public virtual void Register(Participant participant)
        {
            if (!this.CanRegister(participant))
            {
                throw new InvalidOperationException();
            }

            _participants.Add(participant);
        }

        //public void DistributeParticipantPrizes()
        //{
        //    this.AddDomainEvent(new ChallengePayoutDomainEvent(Id, Payout.GetParticipantPrizes(Scoreboard)));
        //}

        public async Task SynchronizeAsync(
            IGameMatchIdsFactory gameMatchIdsFactory,
            IMatchStatsFactory matchStatsFactory,
            IDateTimeProvider synchronizedAt = null
        )
        {
            foreach (var participant in Participants.Where(participant => !participant.HasFinalScore(Timeline))
                .OrderBy(participant => participant.SynchronizedAt)
                .ToList())
            {
                await this.SynchronizeAsync(gameMatchIdsFactory, matchStatsFactory, participant, synchronizedAt);
            }

            this.Synchronize(synchronizedAt);
        }

        public void Start(IDateTimeProvider startedAt)
        {
            if (!this.CanStart())
            {
                throw new InvalidOperationException();
            }

            Timeline = Timeline.Start(startedAt);
        }

        private bool CanStart()
        {
            return Participants.Count == Setup.Entries;
        }

        public void Close(IDateTimeProvider closedAt)
        {
            if (!this.CanClose())
            {
                throw new InvalidOperationException();
            }

            Timeline = Timeline.Close(closedAt);
        }

        private bool CanClose()
        {
            return true;
        }

        protected bool CanRegister(Participant participant)
        {
            return new RegisterParticipantValidator(participant.UserId).Validate(this).IsValid;
        }

        internal bool SynchronizationMoreThan(TimeSpan timeSpan)
        {
            return SynchronizedAt.HasValue && SynchronizedAt.Value + timeSpan < DateTime.UtcNow;
        }

        private async Task SynchronizeAsync(
            IGameMatchIdsFactory gameMatchIdsFactory,
            IMatchStatsFactory matchStatsFactory,
            Participant participant,
            IDateTimeProvider synchronizedAt = null
        )
        {
            var adapter = await gameMatchIdsFactory.CreateAdapterAsync(Game, participant.GameAccountId, Timeline);

            await this.SynchronizeAsync(adapter.GameMatchIds, matchStatsFactory, participant, synchronizedAt);
        }

        private async Task SynchronizeAsync(
            IEnumerable<GameMatchId> matchReferences,
            IMatchStatsFactory matchStatsFactory,
            Participant participant,
            IDateTimeProvider synchronizedAt = null
        )
        {
            foreach (var matchReference in participant.GetUnsynchronizedMatchReferences(matchReferences))
            {
                await this.SnapshotParticipantMatchAsync(participant, matchReference, matchStatsFactory, synchronizedAt);
            }

            participant.Synchronize(synchronizedAt);
        }

        private async Task SnapshotParticipantMatchAsync(
            Participant participant,
            GameMatchId gameMatchId,
            IMatchStatsFactory factory,
            IDateTimeProvider synchronizedAt = null
        )
        {
            var adapter = await factory.CreateAdapter(Game, participant.GameAccountId, gameMatchId);

            this.SnapshotParticipantMatch(participant, gameMatchId, adapter.MatchStats, synchronizedAt);
        }

        internal void SnapshotParticipantMatch(
            Participant participant,
            GameMatchId gameMatchId,
            IMatchStats matchStats,
            IDateTimeProvider synchronizedAt = null
        )
        {
            var match = new Match(gameMatchId, synchronizedAt);

            match.SnapshotStats(Scoring, matchStats);

            participant.Synchronize(match);
        }

        public void Synchronize(IDateTimeProvider synchronizedAt = null)
        {
            SynchronizedAt = synchronizedAt?.DateTime;
        }
    }

    public partial class Challenge : IEquatable<IChallenge>
    {
        public bool Equals([CanBeNull] IChallenge challenge)
        {
            return Id.Equals(challenge?.Id);
        }

        public sealed override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as IChallenge);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

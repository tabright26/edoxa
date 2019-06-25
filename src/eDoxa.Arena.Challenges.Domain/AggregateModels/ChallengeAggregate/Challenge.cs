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
            ChallengeTimeline timeline,
            IScoring scoring,
            IPayout payout
        )
        {
            Name = name;
            Game = game;
            Setup = setup;
            Timeline = timeline;
            Scoring = scoring;
            Payout = payout;
        }

        public ChallengeName Name { get; }

        public ChallengeGame Game { get; }

        public ChallengeSetup Setup { get; }

        public ChallengeTimeline Timeline { get; private set; }

        public DateTime? SynchronizedAt { get; private set; }

        public IScoring Scoring { get; }

        public IPayout Payout { get; }

        public IReadOnlyCollection<Participant> Participants => _participants;

        public IScoreboard Scoreboard => new Scoreboard(this);

        public void Register(Participant participant)
        {
            if (!this.CanRegister(participant))
            {
                throw new InvalidOperationException();
            }

            _participants.Add(participant);
        }

        //public async void Synchronize(IGameMatchIdsFactory gameMatchIdsFactory, IMatchStatsFactory matchStatsFactory, IDateTimeProvider synchronizedAt)
        //{
        //    foreach (var participant in Participants.Where(participant => !participant.HasFinalScore(Timeline))
        //        .OrderBy(participant => participant.SynchronizedAt)
        //        .ToList())
        //    {
        //        await this.SynchronizeAsync(gameMatchIdsFactory, matchStatsFactory, participant, synchronizedAt);
        //    }

        //    this.Synchronize(synchronizedAt);
        //}

        public void Start(IDateTimeProvider startedAt)
        {
            if (!this.CanStart())
            {
                throw new InvalidOperationException();
            }

            Timeline = Timeline.Start(startedAt);
        }

        public void Close(IDateTimeProvider closedAt)
        {
            if (!this.CanClose())
            {
                throw new InvalidOperationException();
            }

            Timeline = Timeline.Close(closedAt);
        }

        //public void DistributeParticipantPrizes()
        //{
        //    this.AddDomainEvent(new ChallengePayoutDomainEvent(Id, Payout.GetParticipantPrizes(Scoreboard)));
        //}

        public void Synchronize(
            Func<GameAccountId, DateTime, DateTime, IEnumerable<GameReference>> getParticipantGameReferences,
            Func<GameAccountId, GameReference, IMatchStats> getParticipantMatchStats,
            IDateTimeProvider synchronizedAt
        )
        {
        }

        private bool CanStart()
        {
            return Participants.Count == Setup.Entries;
        }

        private bool CanClose()
        {
            return true;
        }

        private bool CanRegister(Participant participant)
        {
            return new RegisterParticipantValidator(participant.UserId).Validate(this).IsValid;
        }

        internal bool SynchronizationMoreThan(TimeSpan timeSpan)
        {
            return SynchronizedAt.HasValue && SynchronizedAt.Value + timeSpan < DateTime.UtcNow;
        }

        //private async Task SynchronizeAsync(
        //    IGameMatchIdsFactory gameMatchIdsFactory,
        //    IMatchStatsFactory matchStatsFactory,
        //    Participant participant,
        //    IDateTimeProvider synchronizedAt
        //)
        //{
        //    var adapter = await gameMatchIdsFactory.CreateAdapterAsync(Game, participant.GameAccountId, Timeline);

        //    await this.SynchronizeAsync(adapter.GetGameReferences, matchStatsFactory, participant, synchronizedAt);
        //}

        //private async Task SynchronizeAsync(
        //    IEnumerable<GameMatchId> matchReferences,
        //    IMatchStatsFactory matchStatsFactory,
        //    Participant participant,
        //    IDateTimeProvider synchronizedAt
        //)
        //{
        //    foreach (var matchReference in participant.GetUnsynchronizedMatchReferences(matchReferences))
        //    {
        //        await this.SnapshotParticipantMatchAsync(participant, matchReference, matchStatsFactory, synchronizedAt);
        //    }

        //    participant.Synchronize(synchronizedAt);
        //}

        //private async Task SnapshotParticipantMatchAsync(
        //    Participant participant,
        //    GameMatchId gameMatchId,
        //    IMatchStatsFactory factory,
        //    IDateTimeProvider synchronizedAt
        //)
        //{
        //    var adapter = await factory.CreateAdapter(Game, participant.GameAccountId, gameMatchId);

        //    this.SnapshotParticipantMatch(participant, gameMatchId, adapter.MatchStats, synchronizedAt);
        //}

        internal void SnapshotParticipantMatch(
            Participant participant,
            GameReference gameReference,
            IMatchStats matchStats,
            IDateTimeProvider synchronizedAt
        )
        {
            var match = new Match(gameReference, synchronizedAt);

            match.SnapshotStats(Scoring, matchStats);

            participant.Synchronize(match);
        }

        public void Synchronize(IDateTimeProvider synchronizedAt)
        {
            SynchronizedAt = synchronizedAt.DateTime;
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

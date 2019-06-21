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
using eDoxa.Arena.Challenges.Domain.Abstractions.Strategies;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Arena.Challenges.Domain.Validators;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public abstract class Challenge : Entity<ChallengeId>, IChallenge, IAggregateRoot
    {
        private readonly HashSet<Participant> _participants = new HashSet<Participant>();

        private IScoring _scoring;
        private IPayout _payout;

        protected Challenge(
            ChallengeGame game,
            ChallengeName name,
            ChallengeSetup setup,
            ChallengeDuration duration,
            IDateTimeProvider provider
        )
        {
            Name = name;
            Game = game;
            Setup = setup;
            Timeline = new ChallengeTimeline(duration);
            CreatedAt = provider.DateTime;
            SynchronizedAt = null;
            this.ApplyScoringStrategy(ScoringFactory.Instance.CreateStrategy(this));
            this.ApplyPayoutStrategy(PayoutFactory.Instance.CreateStrategy(this));
        }

        public DateTime CreatedAt { get; }

        public DateTime? SynchronizedAt { get; private set; }

        public ChallengeName Name { get; }

        public ChallengeGame Game { get; }

        public ChallengeSetup Setup { get; }

        public ChallengeTimeline Timeline { get; private set; }

        public ChallengeState State => Timeline.State;

        public IScoring Scoring => _scoring;

        public IPayout Payout => _payout;

        public IReadOnlyCollection<Participant> Participants => _participants;

        public IScoreboard Scoreboard => new Scoreboard(_participants, Setup.BestOf);

        public async Task SynchronizeAsync(IGameMatchIdsFactory gameMatchIdsFactory, IMatchStatsFactory matchStatsFactory)
        {
            foreach (var participant in Participants.Where(participant => !participant.HasFinalScore(Timeline))
                .OrderBy(participant => participant.SynchronizedAt)
                .ToList())
            {
                await this.SynchronizeAsync(gameMatchIdsFactory, matchStatsFactory, participant);

                participant.Synchronize(new UtcNowDateTimeProvider());
            }

            SynchronizedAt = DateTime.UtcNow;
        }

        //public void DistributeParticipantPrizes()
        //{
        //    this.AddDomainEvent(new ChallengePayoutDomainEvent(Id, Payout.GetParticipantPrizes(Scoreboard)));
        //}

        public Participant Register(Participant participant)
        {
            if (!this.CanRegister(participant))
            {
                throw new InvalidOperationException();
            }

            _participants.Add(participant);

            this.TryStart();

            return participant;
        }

        public void ApplyScoring(IScoring scoring)
        {
            _scoring = scoring;
        }

        public void ApplyPayout(IPayout payout)
        {
            _payout = payout;
        }

        private void ApplyScoringStrategy(IScoringStrategy strategy)
        {
            this.ApplyScoring(strategy.Scoring);
        }

        private void ApplyPayoutStrategy(IPayoutStrategy strategy)
        {
            this.ApplyPayout(strategy.Payout);
        }

        internal bool LastSyncMoreThan(TimeSpan timeSpan)
        {
            return SynchronizedAt.HasValue && SynchronizedAt.Value + timeSpan < DateTime.UtcNow;
        }

        private async Task SynchronizeAsync(IGameMatchIdsFactory gameMatchIdsFactory, IMatchStatsFactory matchStatsFactory, Participant participant)
        {
            var adapter = await gameMatchIdsFactory.CreateAdapterAsync(Game, participant.GameAccountId, Timeline);

            await this.SynchronizeAsync(adapter.GameMatchIds, matchStatsFactory, participant);
        }

        private async Task SynchronizeAsync(IEnumerable<GameMatchId> matchReferences, IMatchStatsFactory matchStatsFactory, Participant participant)
        {
            foreach (var matchReference in participant.GetUnsynchronizedMatchReferences(matchReferences))
            {
                await this.SnapshotParticipantMatchAsync(participant, matchReference, matchStatsFactory);
            }
        }

        private async Task SnapshotParticipantMatchAsync(Participant participant, GameMatchId gameMatchId, IMatchStatsFactory factory)
        {
            var adapter = await factory.CreateAdapter(Game, participant.GameAccountId, gameMatchId);

            this.SnapshotParticipantMatch(participant, gameMatchId, adapter.MatchStats);
        }

        internal void SnapshotParticipantMatch(Participant participant, GameMatchId gameMatchId, IMatchStats matchStats)
        {
            var match = new Match(gameMatchId, new UtcNowDateTimeProvider());

            match.SnapshotStats(Scoring, matchStats);

            participant.Synchronize(match);
        }

        public void TryClose(Action closeChallenge)
        {
            if (Timeline.State.Equals(ChallengeState.Ended))
            {
                Timeline = Timeline.Close();

                closeChallenge();
            }
        }

        private bool CanRegister(Participant participant)
        {
            return new RegisterParticipantValidator(participant.UserId).Validate(this).IsValid;
        }

        internal bool CanStart()
        {
            return Participants.Count == Setup.Entries;
        }

        private void TryStart()
        {
            if (this.CanStart())
            {
                Timeline = Timeline.Start();
            }
        }
    }
}

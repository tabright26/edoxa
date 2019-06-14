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
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.DomainEvents;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Arena.Challenges.Domain.Validators;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Extensions;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class Challenge : Entity<ChallengeId>, IChallenge, IAggregateRoot
    {
        private HashSet<Participant> _participants;
        private HashSet<ScoringItem> _scoringItems;
        private List<Bucket> _buckets;

        public Challenge(
            Game game,
            ChallengeName name,
            ChallengeSetup setup,
            ChallengeDuration duration
        ) : this()
        {
            Game = game;
            Name = name;
            Setup = setup;
            Timeline = new ChallengeTimeline(duration);
            this.ApplyScoringStrategy(ScoringFactory.Instance.CreateStrategy(this));
            this.ApplyPayoutStrategy(PayoutFactory.Instance.CreateStrategy(this));
        }

        private Challenge()
        {
            CreatedAt = DateTime.UtcNow;
            LastSync = null;
            _scoringItems = new HashSet<ScoringItem>();
            _participants = new HashSet<Participant>();
            _buckets = new List<Bucket>();
        }

        public IReadOnlyCollection<ScoringItem> ScoringItems => _scoringItems;

        public IReadOnlyCollection<Bucket> Buckets => _buckets;

        public DateTime CreatedAt { get; private set; }

        public DateTime? LastSync { get; private set; }

        public Game Game { get; private set; }

        public ChallengeName Name { get; private set; }

        public ChallengeSetup Setup { get; private set; }

        public ChallengeTimeline Timeline { get; private set; }

        public ChallengeState State => Timeline.State;

        public IReadOnlyCollection<Participant> Participants
        {
            get => _participants;
            private set => _participants = new HashSet<Participant>(value);
        }

        public IScoring Scoring => new Scoring(ScoringItems);

        public IPayout Payout => new Payout(new Buckets(Buckets));

        public IScoreboard Scoreboard => new Scoreboard(Participants);

        public async Task SynchronizeAsync(IMatchReferencesFactory matchReferencesFactory, IMatchStatsFactory matchStatsFactory)
        {
            foreach (var participant in Participants.Where(participant => !participant.HasFinalScore(Timeline))
                .OrderBy(participant => participant.LastSync)
                .ToList())
            {
                await this.SynchronizeAsync(matchReferencesFactory, matchStatsFactory, participant);

                participant.Sync();
            }

            LastSync = DateTime.UtcNow;
        }

        private void ApplyScoringStrategy(IScoringStrategy strategy)
        {
            strategy.Scoring.ForEach(stat => _scoringItems.Add(new ScoringItem(stat.Key, stat.Value)));
        }

        private void ApplyPayoutStrategy(IPayoutStrategy strategy)
        {
            strategy.Payout.Buckets.ForEach(bucket => _buckets.Add(bucket));
        }

        internal bool LastSyncMoreThan(TimeSpan timeSpan)
        {
            return LastSync.HasValue && LastSync.Value + timeSpan < DateTime.UtcNow;
        }

        private async Task SynchronizeAsync(IMatchReferencesFactory matchReferencesFactory, IMatchStatsFactory matchStatsFactory, Participant participant)
        {
            var adapter = await matchReferencesFactory.CreateAdapterAsync(Game, participant.UserGameReference, Timeline);

            await this.SynchronizeAsync(adapter.MatchReferences, matchStatsFactory, participant);
        }

        private async Task SynchronizeAsync(IEnumerable<MatchReference> matchReferences, IMatchStatsFactory matchStatsFactory, Participant participant)
        {
            foreach (var matchReference in participant.GetUnsynchronizedMatchReferences(matchReferences))
            {
                await this.SnapshotParticipantMatchAsync(participant, matchReference, matchStatsFactory);
            }
        }

        private async Task SnapshotParticipantMatchAsync(Participant participant, MatchReference matchReference, IMatchStatsFactory factory)
        {
            var adapter = await factory.CreateAdapter(Game, participant.UserGameReference, matchReference);

            this.SnapshotParticipantMatch(participant, matchReference, adapter.MatchStats);
        }

        internal void SnapshotParticipantMatch(Participant participant, MatchReference matchReference, IMatchStats matchStats)
        {
            participant.SnapshotMatch(matchReference, matchStats);
        }

        public void TryClose(Action closeChallenge)
        {
            if (Timeline.State.Equals(ChallengeState.Ended))
            {
                Timeline = Timeline.Close();

                closeChallenge();
            }
        }

        public void DistributeParticipantPrizes()
        {
            this.AddDomainEvent(new ChallengePayoutDomainEvent(Id, Payout.GetParticipantPrizes(Scoreboard)));
        }

        public Participant RegisterParticipant(UserId userId, UserGameReference userGameReference)
        {
            if (!this.CanRegisterParticipant(userId))
            {
                throw new InvalidOperationException();
            }

            var participant = new Participant(this, userId, userGameReference);

            _participants.Add(participant);

            this.TryStart();

            return participant;
        }

        private bool CanRegisterParticipant(UserId userId)
        {
            return new RegisterParticipantValidator(userId).Validate(this).IsValid;
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
